using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toSpawn;

    private const float spawnRef = 2f;
    private float timer;

    private void Start()
    {
        timer = spawnRef;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], transform.position, Quaternion.identity);
            timer = spawnRef;
        }
    }
}
