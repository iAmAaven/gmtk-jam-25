using UnityEngine;

public class RingManager : MonoBehaviour
{
    public int ringsCompletedInlevel = 0;
    public RingDoor[] allRingDoors;

    public void NewRingCompleted()
    {
        ringsCompletedInlevel++;
        foreach (RingDoor ringDoor in allRingDoors)
        {
            ringDoor.CheckIfCanBeOpened(ringsCompletedInlevel);
        }
    }
}
