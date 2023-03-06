using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PushButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool reuseable;
    public Sprite newSprite;
    public Transform targetObject;
    public float speed;
    public Vector3 targetPos;

    private Sprite original;
    private bool pressed;
    private bool isColliding;
    private Vector3 originalPos;
    //private Rigidbody2D rb;
    private LevelController level_controller;

    void Start()
    {
        pressed = false;
        originalPos = targetObject.position;
        //rb = targetObject.GetComponent<Rigidbody2D>();
        original = spriteRenderer.sprite;
        level_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
    }

    void Update()
    {
        if (isColliding)
        {
            UpdateMovement(targetPos);
        }
        else
        {
            UpdateMovement(originalPos);
        }

        if (level_controller.get_gameover())
        {
            spriteRenderer.sprite = original;
            targetObject.position = originalPos;
            //rb.velocity = Vector2.zero;
            pressed = false;
            isColliding = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        // Update the image with the new sprite
        if ((!reuseable & !pressed) || reuseable)
        {
            isColliding = true;
            UpdateSprite();
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (reuseable)
        {
            isColliding = false;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if (isColliding)
        {
            pressed = true;
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            spriteRenderer.sprite = original;
        }
    }

    private void UpdateMovement(Vector3 direction)
    {
        targetObject.position = Vector3.MoveTowards(targetObject.position, direction, speed * Time.deltaTime);
    }
}
