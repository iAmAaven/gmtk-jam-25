using UnityEngine;
using UnityEngine.UI;

public class PetrolMeter : MonoBehaviour
{
    public Slider petrolSlider;

    public void RefreshPetrolMeter(int petrol, int maxPetrol)
    {
        petrolSlider.maxValue = maxPetrol;
        petrolSlider.value = petrol;
    }
}
