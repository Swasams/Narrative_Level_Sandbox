using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    DoorController doorController;

    [Header("Door Status")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] enemies;

    void Start()
    {
        door = GameObject.FindWithTag("Door");
        doorController = door.GetComponent<DoorController>();
    }

    private void Update()
    {
        if (enemies[0] == null && enemies[1] == null && enemies[2] == null)
        {
            print("The Room Is Clear!");
            doorController.isRoomCleared = true;
        }
        else
        {
            doorController.isRoomCleared = false;
        }
    }
}
