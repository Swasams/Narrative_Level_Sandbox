using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    KeyCollector key;
    
    [Header("Door State")]
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject roomManager;
    [SerializeField] private BoxCollider2D managerColl2D;

    [Header("Keys")]
    public bool isKeyCollected = false;

    [Header("Room Clear")]
    public bool isRoomCleared = false;

    private AudioSource doorOpened;

    void Start()
    {
        roomManager = GameObject.FindWithTag("Teleporter");
        managerColl2D = roomManager.GetComponent<BoxCollider2D>();
        managerColl2D.enabled = true;

        doorClosed = GameObject.FindWithTag("Closed");
        doorClosed.SetActive(true);

        doorOpen = GameObject.FindWithTag("Opened");
        doorOpen.SetActive(true);

        key = GetComponent<KeyCollector>();
    }

    void Update()
    {
        if (isKeyCollected || isRoomCleared)
        {
            doorClosed.SetActive(false);
            doorOpen.SetActive(true);
            managerColl2D.enabled = true;

            //SoundManager.Instance.PlaySound2D("Door");
        }
        else
        {
            doorClosed.SetActive(true);
            doorOpen.SetActive(false);
            managerColl2D.enabled = false;
        }
    }
}
