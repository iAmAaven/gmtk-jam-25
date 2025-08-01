using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.coinManager.AddCoin();
            Destroy(gameObject);
        }
    }
}
