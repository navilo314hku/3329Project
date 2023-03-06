using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCow : MonoBehaviour
{
    public float speed;
    public bool initial_left;
    
    public bool spawnBySpawner;
    private Vector3 direction;
    private int scale;
    private LevelController level_controller;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        level_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        originalPos = transform.position;
        GetComponent<Animator>().SetInteger("State", 1);
        if (initial_left)
        {
            scale = 1;
        }
        else
        {
            scale = -1;
        }
        direction = new Vector3(-1000000 * scale, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(scale * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (level_controller.get_gameover())
        {
            if (spawnBySpawner)
            {
                //Destroy itself
                Destroy(gameObject);
            }
            else
            {
                //return original position
                Debug.Log("called");
                transform.position = originalPos;
            }
        }
        if (transform.position.y < -100 && spawnBySpawner) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision along x axis and the collision object not other enemy
        if (Mathf.Abs(collision.contacts[0].normal.x) > 0.5f)
        {
            Debug.Log("hitted something");
            direction = new Vector3(-direction.x, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
