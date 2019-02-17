using UnityEngine;

public class PlayAndDestroy : MonoBehaviour
{
    [SerializeField]
    private float timer = 2f;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
