using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float timer_score;
    private AudioSource source;

    private void Start()
    {
        timer_score = 0f;
        index = healthRemaining.Length - 1;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer_score += Time.deltaTime;
        timerText.text = "You survived " + (int)(timer_score) + " seconds";
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>().text = "Game Over\nYou survived " + (int)(timer_score) + " seconds.\n\nPress enter to go back to the main menu.";
        timer_score = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (index == 0)
            {
                SceneManager.sceneLoaded += OnSceneLoad;
                SceneManager.LoadScene("GameEnd");
                return;
            }
            healthRemaining[index].enabled = false;
            index--;
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            Destroy(collision.gameObject);
        }
    }
}