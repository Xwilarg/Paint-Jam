using UnityEngine;

public class PlayAndDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}
