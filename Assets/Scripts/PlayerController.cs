using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 300f;
    private const float fireForce = 5f;
    private const float bulletNb = 10f;
    private const float shootRange = .5f;
    private const float refReload = 1f;
    private const float refReloadSec = 2f;
    private float reloadTime;
    private float reloadTimeSec;

    [SerializeField]
    private GameObject bullet, securityCamera;

    [SerializeField]
    private FovManager fov;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reloadTime = 0f;
        reloadTimeSec = 0f;
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        reloadTime -= Time.deltaTime;
        reloadTimeSec -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && reloadTime < 0f)
        {
            reloadTime = refReload;
            for (int i = 0; i < bulletNb; i++)
            {
                GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector2 dist = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                go.GetComponent<Rigidbody2D>().AddForce(-Vector3.Normalize(Vector3.Normalize(dist) +
                    new Vector3(Random.Range(-shootRange, shootRange), Random.Range(-shootRange, shootRange))) * fireForce, ForceMode2D.Impulse);
            }
        }
        if (Input.GetButtonDown("Fire2") && reloadTimeSec < 0f)
        {
            reloadTimeSec = refReloadSec;
            GameObject go = Instantiate(securityCamera, transform.position, Quaternion.identity);
            Vector2 dist = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            go.GetComponent<Rigidbody2D>().AddForce(-Vector3.Normalize(dist) * fireForce, ForceMode2D.Impulse);
            fov.SetCamera(go.GetComponent<SecurityCamera>());
        }
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
