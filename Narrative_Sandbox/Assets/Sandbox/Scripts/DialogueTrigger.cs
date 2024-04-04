using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // Reference to the Fungus Flowchart containing the dialogue sequence
    public string dialogueBlockName; // Name of the Fungus block to trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the Fungus dialogue sequence
            flowchart.ExecuteBlock(dialogueBlockName);
        }
    }
}
