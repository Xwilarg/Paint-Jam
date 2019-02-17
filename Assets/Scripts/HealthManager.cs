using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthManager : MonoBehaviour
{
    private int health;

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource source;

    private void Start()
    {
        health = 5;
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            health--;
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            Destroy(collision.gameObject);
        }
    }
}
