using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    DoorController doorController;

    [Header("Door")]
    public GameObject door;
    [SerializeField] private GameObject key;

    void Start()
    {
        doorController = door.GetComponent<DoorController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("Picked Up The Key!");

            doorController.isKeyCollected = true;

            ///SoundManager.Instance.PlaySound2D("Key");

            key.SetActive(false);
        }
    }
}
