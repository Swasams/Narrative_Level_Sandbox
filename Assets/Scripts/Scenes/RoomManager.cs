using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    BoxCollider2D coll2D;

    [Header("Current Room")]
    public int roomNumber = 1;

    [Header("Next Room")]
    public int index = 2;

    public static RoomManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        coll2D = gameObject.GetComponent<BoxCollider2D>();
    }

    public void ResetInts()
    {
        roomNumber = 1;
        index = 2;
    }

    public void RoomRandomizer()
    {
        LevelManager.Instance.LoadScene(index, "SquareWipe");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Successfully Moved To Next Room!");

            RoomRandomizer();
            roomNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Successfully Increased The Index!");
            index++;
        }
    }
}
