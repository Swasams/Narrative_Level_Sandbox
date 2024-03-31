using UnityEngine;
using Fungus;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart; // Reference to the Fungus Flowchart containing the dialogue sequence
    public string[] dialogueBlockNames; // Name of the Fungus block to trigger

    public bool isOneShot; // Whether the dialogue should only be triggered once
    public bool isRepeatable; // Whether the dialogue can be triggered multiple times
    public bool hasTriggered = false; // Whether the dialogue has been triggered
    public int conversationCount = 1; // Number for what conversation to trigger

    public UnityEvent onTrigger; // Event to trigger when the dialogue is started
    public UnityEvent onEnd; // Event to trigger when the dialogue ends

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOneShot)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[0]);

                if (onTrigger != null)
                {
                    onTrigger.Invoke();
                }

                gameObject.SetActive(false);
            }

            int blockCount = dialogueBlockNames.Length;

            if (!isOneShot && conversationCount <= blockCount)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[conversationCount - 1]);
                conversationCount++;

                if (onTrigger != null)
                {
                    onTrigger.Invoke();
                }
            }
            else if (!isOneShot && conversationCount > blockCount)
            {
                hasTriggered = true;

                if (onEnd != null)
                {
                    onEnd.Invoke();
                }

                gameObject.SetActive(false);
            }

            if (isRepeatable && !isOneShot && conversationCount <= blockCount)
            {
                // Start the Fungus dialogue sequence
                flowchart.ExecuteBlock(dialogueBlockNames[conversationCount - 1]);

                if (onTrigger != null)
                {
                    onTrigger.Invoke();
                }

                conversationCount++;
            }
            else if (isRepeatable && !isOneShot && conversationCount > blockCount)
            {
                conversationCount = 1;
            }
        }
    }
}
