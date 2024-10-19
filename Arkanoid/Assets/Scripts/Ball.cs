using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 3f;  

    private float ballRadius;
    private Vector2 direction;
    private Vector2 screenBounds;
    
    void Awake()
    {
        direction = new Vector2(1, 1).normalized;
        ballRadius = GetComponent<SpriteRenderer>().bounds.extents.x;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        MoveBall();
        UpdateDirection();
    }

    void MoveBall()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void UpdateDirection()
    {
        Vector2 position = transform.position;

        if (Mathf.Abs(position.x) >= screenBounds.x - ballRadius)
            direction.x = -direction.x;

        if (Mathf.Abs(position.y) >= screenBounds.y - ballRadius)
            direction.y = -direction.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, collisionNormal).normalized;
    }
}