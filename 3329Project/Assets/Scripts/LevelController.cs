using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Respawn_Block;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
