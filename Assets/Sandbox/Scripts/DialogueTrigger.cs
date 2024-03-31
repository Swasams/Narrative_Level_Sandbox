using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // Reference to the Fungus Flowchart containing the dialogue sequence
    public string[] dialogueBlockNames; // Name of the Fungus block to trigger

    public bool isOneShot; // Whether the dialogue should only be triggered once
    public bool isRepeatable; // Whether the dialogue can be triggered multiple times
    public bool hasTriggered = false; // Whether the dialogue has been triggered
    public int conversationCount = 1; // Number for what conversation to trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOneShot)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[0]);
                gameObject.SetActive(false);
            }

            int blockCount = dialogueBlockNames.Length;

            if (!isOneShot && conversationCount <= blockCount)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[conversationCount - 1]);
                conversationCount++;
            }
            else if (!isOneShot && conversationCount > blockCount)
            {
                hasTriggered = true;
                gameObject.SetActive(false);
            }

            if (isRepeatable && !isOneShot && conversationCount <= blockCount)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[conversationCount - 1]);
                conversationCount++;
            }
            else if (isRepeatable && !isOneShot && conversationCount > blockCount)
            {
                conversationCount = 1;
            }
        }
    }
}
