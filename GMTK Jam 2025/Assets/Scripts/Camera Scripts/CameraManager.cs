using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject freeCamera, mainCamera;
    public bool isLookingAround = false;
    private PlaneMovement planeMovement;
    public bool enableCameraMovement = false;

    void Start()
    {
        planeMovement = FindFirstObjectByType<PlaneMovement>();
    }

    void Update()
    {
        if ((!planeMovement.movementIsPrevented || enableCameraMovement) && planeMovement.isGrounded
            && Input.GetKeyDown(KeyCode.C))
        {
            isLookingAround = !isLookingAround;
            freeCamera.SetActive(isLookingAround);
            mainCamera.SetActive(!isLookingAround);
        }
    }
}
