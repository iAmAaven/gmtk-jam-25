using UnityEngine;

public class Petrol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObj = collision.gameObject;
        if (colObj.tag == "Player")
        {
            colObj.GetComponent<PlaneMovement>().RefuelTank();
            Destroy(gameObject);
        }
    }
}
