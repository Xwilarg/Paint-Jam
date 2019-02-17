using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text achievementName, achievementContent;

    private const float refTimer = 5f;
    private float timer;

    private void Update()
    {
        if (timer > -1f)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                panel.SetActive(false);
                timer = -2f;
            }
        }
    }

    public void Enable(string title, string content)
    {
        panel.SetActive(true);
        timer = refTimer;
        achievementName.text = title;
        achievementContent.text = content;
    }
}
