using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstScene;
    public void StartGame()
    {
        SceneManager.LoadScene(firstScene);
    }
}
