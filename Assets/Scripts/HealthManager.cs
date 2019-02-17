using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class HealthManager : MonoBehaviour
{
    private int health;

    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image[] healthRemaining;

    private int index;
    private float timer;
    private AudioSource source;

    private void Start()
    {
        index = healthRemaining.Length - 1;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "You survived " + (int)(timer) + " seconds";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            healthRemaining[index].enabled = false;
            index--;
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            Destroy(collision.gameObject);
        }
    }
}