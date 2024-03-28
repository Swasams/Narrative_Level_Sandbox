using UnityEngine;

public class Blockade : MonoBehaviour
{
    [Header("Blockade State")]
    [SerializeField] private BoxCollider2D blockade;

    [Header("Keys")]
    public bool isKeyCollected = false;

    void Start()
    {
        blockade = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isKeyCollected)
        {
            blockade.enabled = false;
        }
        else
        {
            blockade.enabled = true;
        }
    }
}
