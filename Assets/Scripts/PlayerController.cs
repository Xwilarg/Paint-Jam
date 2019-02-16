using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 300f;
    private const float fireForce = 5f;
    private const float bulletNb = 10;
    private const float shootRange = .5f;

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
            for (int i = 0; i < bulletNb; i++)
            {
                GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector2 dist = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                go.GetComponent<Rigidbody2D>().AddForce(-Vector3.Normalize(Vector3.Normalize(dist) +
                    new Vector3(Random.Range(-shootRange, shootRange), Random.Range(-shootRange, shootRange))) * fireForce, ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
