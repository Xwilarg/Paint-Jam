using UnityEngine;

public class Star : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector2.up * Time.deltaTime);
    }
}
