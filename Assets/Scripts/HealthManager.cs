using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int health;

    private void Start()
    {
        health = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
