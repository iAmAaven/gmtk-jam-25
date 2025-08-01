using UnityEngine;

public class PlaneTriggerMechanics : MonoBehaviour
{
    private PlaneMovement planeMovement;

    void Start()
    {
        planeMovement = GetComponentInParent<PlaneMovement>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        string colTag = coll.gameObject.tag;
        if (colTag != "Player" && colTag != "PickUp" && colTag != "Tutorial" && colTag != "Wall")
        {
            if (!planeMovement.isDestroyed)
                planeMovement.DestroyPlane();
            Debug.Log("Trigger");
        }
    }
}
