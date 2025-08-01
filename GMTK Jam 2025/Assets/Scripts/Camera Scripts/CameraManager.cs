using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject freeCamera, mainCamera;
    public bool isLookingAround = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLookingAround = !isLookingAround;
            freeCamera.SetActive(isLookingAround);
            mainCamera.SetActive(!isLookingAround);
        }
    }
}
