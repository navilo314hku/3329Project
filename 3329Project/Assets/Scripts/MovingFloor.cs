using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float speed;
    public Vector3 targetPos;

    private Vector3 originalPos;
    private LevelController level_controller;
    private Transform _originalParent;
    private Vector3 direction; 

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
        level_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        _originalParent = transform.parent;
        direction = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (targetPos - transform.localPosition).magnitude;
        float distance_to_original = (originalPos - transform.localPosition).magnitude;
        if (distance < 0.3f)
        {
            direction = originalPos;
        }

        if (distance_to_original < 0.3f)
        {
            direction = targetPos;
        }

        UpdateMovement(direction);
        if (level_controller.get_gameover())
        {
            transform.localPosition = originalPos;
        }
    }

    private void UpdateMovement(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
}
