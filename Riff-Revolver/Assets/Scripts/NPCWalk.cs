using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour
{
    public string direction;
    public int distance;
    private Vector2 originalPosition, targetPosition;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        if (direction == "right") {
            targetPosition = new Vector2(originalPosition.x + distance, originalPosition.y);
        } else {
            targetPosition = new Vector2(originalPosition.x - distance, originalPosition.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != targetPosition.x) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        } else {
            transform.position = originalPosition;
        }
    }
}
