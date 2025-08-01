using UnityEngine;

public class InstructorEnablesMovement : MonoBehaviour
{
    public bool cameraMovementEnabled = false;
    public bool planeMovementEnabled = false;
    public bool finishDoorOpens = false;
    public Animator finishDoorAnim;

    void Start()
    {
        FindFirstObjectByType<CameraManager>().enableCameraMovement = cameraMovementEnabled;
        FindFirstObjectByType<PlaneMovement>().movementIsPrevented = !planeMovementEnabled;
        if (finishDoorOpens)
            finishDoorAnim.SetTrigger("Open");
    }
}
