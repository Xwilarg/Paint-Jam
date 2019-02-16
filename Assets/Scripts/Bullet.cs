using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
            other.collider.GetComponent<Enemy>().Die();
        Destroy(gameObject);
    }
}
