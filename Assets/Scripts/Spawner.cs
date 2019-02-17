using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toSpawn;

    private float spawnRef = 8f;
    [SerializeField]
    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], transform.position, Quaternion.identity);
            if (spawnRef > 5f)
                spawnRef -= .5f;
            timer = spawnRef;
        }
    }
}
