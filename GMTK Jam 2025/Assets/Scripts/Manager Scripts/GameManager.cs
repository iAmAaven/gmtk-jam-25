using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CoinManager coinManager;
    public MotivationManager motivationManager;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
