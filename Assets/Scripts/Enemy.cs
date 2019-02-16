using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    private const float speed = 100f;
    private const float distNext = .1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource source;
    private BoxCollider2D bc;

    [SerializeField]
    private Sprite[] up, down, left, right;

    [SerializeField]
    private AudioClip[] spawnClip, deathClip;

    public bool IsEnable { set; private get; }

    private NextNode nextNode;

    public void Die()
    {
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
        Camera.main.GetComponent<Shake>().ShakeCamera();
        bc.enabled = false;
        sr.sprite = null;
        source.clip = deathClip[Random.Range(0, deathClip.Length)];
        source.volume = 1f;
        source.Play();
        Destroy(gameObject, 2f);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
        IsEnable = false;
        var allNodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject node = null;
        float dist = 0f;
        foreach (GameObject nn in allNodes)
        {
            float cd = Vector2.Distance(nn.transform.position, transform.position);
            if (node == null || cd < dist)
            {
                node = nn;
                dist = cd;
            }
        }
        nextNode = node.GetComponent<NextNode>();
        source.clip = spawnClip[Random.Range(0, spawnClip.Length)];
        source.Play();
    }

    private void Update()
    {
        int x = 0, y = 0;
        if (transform.position.x - nextNode.transform.position.x < -distNext)
            x = 1;
        else if (transform.position.x - nextNode.transform.position.x > distNext)
            x = -1;
        if (transform.position.y - nextNode.transform.position.y < -distNext)
            y = 1;
        else if (transform.position.y - nextNode.transform.position.y > distNext)
            y = -1;
        if (x == 0 && y == 0)
            nextNode = nextNode.GetNextNode();
        else
            rb.velocity = new Vector2(x, y) * Time.deltaTime * speed;
        if (IsEnable)
        {
            if (y == 1)
                sr.sprite = up[Random.Range(0, up.Length)];
            else if (y == -1)
                sr.sprite = down[Random.Range(0, down.Length)];
            else if (x == -1)
                sr.sprite = left[Random.Range(0, left.Length)];
            else if (x == 1)
                sr.sprite = right[Random.Range(0, right.Length)];
        }
        sr.enabled = IsEnable;
        if (IsEnable)
            IsEnable = false;
    }
}
