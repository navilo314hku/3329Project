using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    //Player related
    public Vector2 velocity;
    public Vector2 runVelocity;
    public float jumpforce = 10f;
    public int player_id; //Indicate the character is Player 1 or Player 2
    public LayerMask wallMask;
    public bool havekey =false;
    public int current_level = 1;
    Rigidbody2D rb;

    private int jump_count;
    private bool walk,run, walk_left, walk_right, jump;
    // Start is called before the first frame update
    private LevelController level_controller;


    public void respawn()
    {
        //gameover restart
        level_controller.set_gameover(true);

        //get the respawn block game object
        GameObject respawn_block = level_controller.get_respawn_block();
        transform.position = new Vector3(respawn_block.transform.position.x, respawn_block.transform.position.y+5, respawn_block.transform.position.z);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        level_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent <LevelController>() ;
        jump_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        UpdatePlayerPosition();
        //RayCastCheck();
        CheckDrop();
    }

    void UpdatePlayerPosition()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;
        if (walk)
        {
            if (walk_left)
            {
                if (run)
                {
                    pos.x -= runVelocity.x * Time.deltaTime;
                }
                else { pos.x -= velocity.x * Time.deltaTime; }
                
                scale.x = -1;
            }

            if (walk_right)
            {
                if (run)
                {
                    pos.x += runVelocity.x * Time.deltaTime;
                }
                else { pos.x += velocity.x * Time.deltaTime; }
                scale.x = 1;
            }
            //pos = CheckWallRays(pos, scale.x);//return 
        }

        transform.localPosition = pos;
        transform.localScale = scale;
        if (jump && jump_count<2)
        {
            Debug.Log("jumpping");
            jump_count += 1;
            rb.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
        }
    }
    void CheckPlayerInput()
    {
        if (player_id == 1) //Player 1
        {
            bool input_left = Input.GetKey(KeyCode.LeftArrow);
            bool input_right = Input.GetKey(KeyCode.RightArrow);
            bool input_jump = Input.GetKeyDown(KeyCode.UpArrow);
            bool input_run = Input.GetKey(KeyCode.M);
            run = input_run;
            walk = input_left || input_right;
            walk_left = input_left && !input_right;
            walk_right = !input_left && input_right;
            jump = input_jump;
        }
        else if (player_id == 2)
        {   //TODO: Keys To be editted 
            bool input_left = Input.GetKey(KeyCode.A);
            bool input_right = Input.GetKey(KeyCode.D);
            bool input_jump = Input.GetKeyDown(KeyCode.W);
            bool input_run = Input.GetKey(KeyCode.V);
            run = input_run;
            walk = input_left || input_right;
            walk_left = input_left && !input_right;
            walk_right = !input_left && input_right;
            jump = input_jump;
        }
        else
        {
            Debug.Log("Missing player id");
        }
    }

    void CheckDrop()
    {
        if (transform.position.y < -100)
        {
            respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            Debug.Log("Got Key");
            havekey = true;
            Destroy(other.gameObject);

        }
        if (other.tag == "Spikes") {
            Debug.Log("Ouch!!! Spikes");
            respawn();
        }
        if (other.tag == "Exit" & havekey)
        {
            Debug.Log("Level Completed");
            current_level++;
            SceneManager.LoadScene(current_level-1);
            //GoToNextLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump_count = 0;
        if (collision.gameObject.tag == "MovingBlock")
        {
            Debug.Log("contacted");
            transform.SetParent(collision.collider.transform, true);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ouch!!! Enemy");
            respawn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingBlock")
        {
            transform.SetParent(null);
        }
    }


    //Vector3 CheckWallRays(Vector3 pos, float direction)
    //{
    //    Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
    //    Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
    //    Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);
    //    RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
    //    RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
    //    RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
    //    if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
    //    {
    //        pos.x -= velocity.x * Time.deltaTime * direction;
    //    }
    //    return pos;

    //}
    //Vector3 CheckUpDownRays(Vector3 pos)
}
/*void CheckPlayerInput()
{
    bool input_left = Input.GetKey(KeyCode.LeftArrow);
    bool input_right = Input.GetKey(KeyCode.RightArrow);
    bool input_space = Input.GetKeyDown(KeyCode.Space);
    bool input_z = Input.GetKey(KeyCode.Z);
    run = input_z;
    walk = input_left || input_right;
    walk_left = input_left && !input_right;
    walk_right = !input_left && input_right;
    jump = input_space;
    //Debug.Log(input_space);
}
*/