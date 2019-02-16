using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    private const float speed = 50f;
    private Rigidbody2D rb;

    public void Die()
    {
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
        Camera.main.GetComponent<Shake>().ShakeCamera();
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }
}
