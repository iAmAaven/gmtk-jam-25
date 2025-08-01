using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public string sceneToChangeTo;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlaneMovement>().StopPlane();
            collision.gameObject.GetComponent<PlaneMovement>().movementIsPrevented = true;
            SceneManager.LoadScene(sceneToChangeTo);
        }
    }
}
