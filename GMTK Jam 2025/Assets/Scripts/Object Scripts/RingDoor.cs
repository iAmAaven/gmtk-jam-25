using UnityEngine;

public class RingDoor : MonoBehaviour
{
    public int ringsNeededToOpen = 1;
    public Animator doorAnim;
    public bool isDoorOpen = false;

    public void CheckIfCanBeOpened(int totalRings)
    {
        if (isDoorOpen == false && totalRings >= ringsNeededToOpen)
        {
            Debug.Log("Trying to open door...");
            doorAnim.SetTrigger("Open");
            isDoorOpen = true;
        }
    }
}
