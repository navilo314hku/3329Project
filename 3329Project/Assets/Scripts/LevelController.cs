using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Respawn_Block;
    private bool gameover; 
    public GameObject get_respawn_block()
    {
        return Respawn_Block;
    }
    public void printShit()
    {
        Debug.Log("print shit from levelController");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Respawn_Block == null)
        {
            throw new System.Exception("No respawn_block is found");
        }
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for 1 second
        StartCoroutine(WaitForOneSecond());
    }

    public bool get_gameover()
    {
        return gameover; 
    }

    public void set_gameover(bool status)
    {
        gameover = status;
    }

    IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
        set_gameover(false);
    }
}
