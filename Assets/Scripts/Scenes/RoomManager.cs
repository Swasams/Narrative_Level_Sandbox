using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [Header("Current Room")]
    public int roomNumber = 1;

    [Header("Next Room")]
    public int index = 2;

    public static RoomManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        roomNumber = SceneManager.GetActiveScene().buildIndex;
        index = roomNumber + 1;
    }

    public void ResetInts()
    {
        roomNumber = 1;
        index = 2;
    }

    public void RoomSwitcher()
    {
        LevelManager.Instance.LoadScene(index, "SquareWipe");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Successfully Moved To Next Room!");

            RoomSwitcher();
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
