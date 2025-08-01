using UnityEngine;

public class Lift : MonoBehaviour
{
    private PlaneMovement planeMovement;

    void Start()
    {
        planeMovement = FindFirstObjectByType<PlaneMovement>();
        planeMovement.movementIsPrevented = true;
    }
}
