using TMPro;
using UnityEngine;

public class MotivationManager : MonoBehaviour
{
    public int currentMotivation = 3;
    public TextMeshProUGUI motivationText;

    void Start()
    {
        motivationText.text = "x" + currentMotivation;
    }

    public void AddMotivation()
    {
        currentMotivation++;
        motivationText.text = "x" + currentMotivation;
    }

    public void LoseMotivation()
    {
        currentMotivation--;
        if (currentMotivation < 0)
            Debug.Log("GAME OVER! MOTIVATION LOST!");
        else
            motivationText.text = "x" + currentMotivation;
    }
}
