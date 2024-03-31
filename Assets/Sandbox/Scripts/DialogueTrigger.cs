using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // Reference to the Fungus Flowchart containing the dialogue sequence
    public string dialogueBlockName; // Name of the Fungus block to trigger

    public bool isOneShot; // Whether the dialogue should only be triggered once
    public bool hasTriggered = false; // Whether the dialogue has been triggered

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                flowchart.ExecuteBlock(dialogueBlockName);
            }

            if (isOneShot)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockName);
                hasTriggered = true;
            }
        }
    }
}
