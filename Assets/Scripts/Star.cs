using UnityEngine;

public class Star : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(-100f, 100f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}
