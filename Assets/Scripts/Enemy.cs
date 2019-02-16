using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    private const float speed = 100f;
    private const float distNext = .1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField]
    private Sprite[] up, down, left, right;

    public bool IsEnable { set; private get; }

    private NextNode nextNode;

    public void Die()
    {
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
        Camera.main.GetComponent<Shake>().ShakeCamera();
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
