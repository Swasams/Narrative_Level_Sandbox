using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door State")]
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject roomManager;
    [SerializeField] private BoxCollider2D managerColl2D;

    [Header("Keys")]
    public bool isKeyCollected = false;

    [Header("Room Clear")]
    public bool isRoomCleared = false;

    void Start()
    {
        roomManager = GameObject.FindWithTag("Teleporter");
        managerColl2D = roomManager.GetComponent<BoxCollider2D>();
        managerColl2D.enabled = true;

        doorClosed = GameObject.FindWithTag("Closed");
        doorClosed.SetActive(true);

        doorOpen = GameObject.FindWithTag("Opened");
        doorOpen.SetActive(true);
    }

    void Update()
    {
        if (isKeyCollected || isRoomCleared)
        {
            doorClosed.SetActive(false);
            doorOpen.SetActive(true);
            managerColl2D.enabled = true;
        }
        else
        {
            doorClosed.SetActive(true);
            doorOpen.SetActive(false);
            managerColl2D.enabled = false;
        }
    }
}
