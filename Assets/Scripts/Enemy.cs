using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;

    public void Die()
    {
        Instantiate(blood, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
        Camera.main.GetComponent<Shake>().ShakeCamera();
        Destroy(gameObject);
    }
}
