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
    private Rigidbody2D rb;

    void Start()
    {
        pressed = false;
        originalPos = targetObject.position;
        rb = targetObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distance = (targetPos - targetObject.position).magnitude;
        if ((!reuseable || (reuseable & isColliding)) & distance < 0.3f)
        {
            rb.velocity = Vector2.zero;
        }
        else if (reuseable & !isColliding)
        {
            float distance_to_original = (originalPos - targetObject.position).magnitude;
            if (distance_to_original > 0.3f)
            {
                Vector3 direction = (originalPos - targetPos) * speed;
                UpdateMovement(direction);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        
        // Update the image with the new sprite
        if ((!reuseable & !pressed) || reuseable)
        {
            Vector3 direction = (targetPos - originalPos) * speed;
            isColliding = true;
            UpdateSprite();
            UpdateMovement(direction);
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (reuseable)
        {
            Vector3 direction = (originalPos - targetObject.position) * speed;

            isColliding = false;
            UpdateSprite();
            UpdateMovement(direction);
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

    private void UpdateMovement(Vector3 direction)
    {
        rb.velocity = new Vector2(direction.x, direction.y);
        Debug.Log(direction.y);
    }
}
