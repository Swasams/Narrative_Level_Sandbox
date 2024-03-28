using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DoorController doorController;
    [SerializeField] private Blockade blockade;

    [Header("Door")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject counter;
    [SerializeField] private GameObject key;

    void Start()
    {
        if (counter != null)
            blockade = counter.GetComponent<Blockade>();

        if (door != null)
            doorController = door.GetComponent<DoorController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("Picked Up The Key!");

            if (blockade != null)
                blockade.isKeyCollected = true;

            if (doorController != null)
                doorController.isKeyCollected = true;

            key.SetActive(false);
        }
    }
}
