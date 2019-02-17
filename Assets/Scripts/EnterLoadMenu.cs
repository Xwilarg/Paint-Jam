using UnityEngine;

public class EnterLoadMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
