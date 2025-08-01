using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int currentCoins;
    public int coinsNeededForMotivation = 10;
    public TextMeshProUGUI coinText;

    public void AddCoin()
    {
        currentCoins++;
        if (currentCoins >= coinsNeededForMotivation)
        {
            currentCoins = 0;
            GetComponent<GameManager>().motivationManager.AddMotivation();
        }
        coinText.text = "x" + currentCoins;
    }
}
