using UnityEngine;

public class Star : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(-40f, 40f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}
