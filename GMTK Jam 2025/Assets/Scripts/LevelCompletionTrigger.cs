using UnityEngine;

public class LevelCompletionTrigger : MonoBehaviour
{
    public bool levelFinished = false;
    public GameObject completionDialogue;
    private PlaneMovement plane;
    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject colObj = collision.gameObject;
        if (levelFinished == false && colObj.tag == "Player")
        {
            plane = colObj.GetComponent<PlaneMovement>();
            if (plane.isGrounded && !plane.isDestroyed)
            {
                Debug.Log("Level Finished!");
                levelFinished = true;
                Invoke("InstructorDialogue", 2f);
            }
        }
    }

    void InstructorDialogue()
    {
        plane.StopPlane();
        if (plane.isDestroyed)
            return;

        completionDialogue.SetActive(true);
    }
}
