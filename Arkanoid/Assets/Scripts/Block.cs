using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int score = 1;
    [SerializeField] private GameObject particlePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(score);
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
