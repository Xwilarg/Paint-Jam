using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 300f;
    private const float fireForce = 5f;

    [SerializeField]
    private GameObject bullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(-(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)) * fireForce, ForceMode2D.Impulse);
        }
    }
}
