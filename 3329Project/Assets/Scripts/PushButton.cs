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
    private Sprite original;
    private bool pressed;
    private bool isColliding;
    void Start()
    {
        pressed = false;
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.tag == "Player")
        {
            // Update the image with the new sprite
            if ((!reuseable & !pressed) || reuseable)
            {
                isColliding = true;
                UpdateSprite();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        Debug.Log("leaving");
        if (target.collider.tag == "Player" & reuseable)
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
            original = spriteRenderer.sprite;
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            spriteRenderer.sprite = original;
        }
    }
}
