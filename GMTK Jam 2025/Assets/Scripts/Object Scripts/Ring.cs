using UnityEngine;

public class Ring : MonoBehaviour
{
    public int direction;
    public bool isRingCompleted = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something hit the ring:" + collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && isRingCompleted == false)
        {
            Debug.Log("Player went thorugh the ring");
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            if ((direction < 0 && playerRB.linearVelocityX < 0)
            || (direction > 0 && playerRB.linearVelocityX > 0))
            {
                Debug.Log("Player went the right way!");
                RingCompleted();
            }
            else
                Debug.Log("Player went the wrong way!");
        }
    }

    void RingCompleted()
    {
        isRingCompleted = true;
        FindFirstObjectByType<RingManager>().NewRingCompleted();
    }
}