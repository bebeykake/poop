using UnityEngine;
using UnityEngine.SceneManagement;

public class titlescreen : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("TCWTR");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}