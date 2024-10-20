using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private float ballRadius;
    private Vector2 direction;
    private Vector2 screenBounds;

    private void Awake()
    {
        direction = new Vector2(1, 1).normalized;
        ballRadius = Mathf.Max(GetComponent<SpriteRenderer>().bounds.extents.x, GetComponent<SpriteRenderer>().bounds.extents.y);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update()
    {
        MoveBall();
        UpdateDirection();
    }

    private void MoveBall()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void UpdateDirection()
    {
        Vector2 position = transform.position;

        position.x = CheckBounds(position.x, ballRadius, screenBounds.x, ref direction.x);
        position.y = CheckBounds(position.y, ballRadius, screenBounds.y, ref direction.y);

        transform.position = position;
    }

    private float CheckBounds(float pos, float radius, float bound, ref float dir)
    {
        if (pos + radius > bound || pos - radius < -bound)
        {
            dir = -dir;
            pos = Mathf.Clamp(pos, -bound + radius, bound - radius);
        }
        return pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, collisionNormal).normalized;
    }
}
