using UnityEngine;

public class ControlTutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public float slowMoMultiplier = 4f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlaneMovement planeMovement = collision.gameObject.GetComponent<PlaneMovement>();

            planeMovement.flightSpeed = planeMovement.unchangedFlightSpeed / slowMoMultiplier;
            planeMovement.maxTorqueSpeed = planeMovement.unchangedMaxTorque / slowMoMultiplier;
            planeMovement.gasConsumption = planeMovement.unchangedGasConsumption * slowMoMultiplier;

            tutorialCanvas.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlaneMovement planeMovement = collision.gameObject.GetComponent<PlaneMovement>();
            planeMovement.flightSpeed = planeMovement.unchangedFlightSpeed;
            planeMovement.maxTorqueSpeed = planeMovement.unchangedMaxTorque;
            planeMovement.gasConsumption = planeMovement.unchangedGasConsumption;

            Destroy(tutorialCanvas);
        }
    }
}
