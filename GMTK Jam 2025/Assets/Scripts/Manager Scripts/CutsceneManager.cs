using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string sceneToChangeTo;
    public float cutsceneDuration = 16f;

    void Start()
    {
        Invoke("SceneChange", cutsceneDuration);
    }
    void SceneChange()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
