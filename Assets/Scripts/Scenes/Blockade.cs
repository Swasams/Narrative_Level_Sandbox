using UnityEngine;
using Cinemachine;

public class Blockade : MonoBehaviour
{
    [Header("Blockade State")]
    [SerializeField] private BoxCollider2D blockade;

    [Header("Keys")]
    public bool isKeyCollected = false;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera playerFollowCam;
    [SerializeField] private CinemachineVirtualCamera hallwayCam;

    void Start()
    {
        blockade = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isKeyCollected)
        {
            blockade.isTrigger = true;
        }
        else
        {
            blockade.isTrigger = false;
        }
    }

    public void ToggleCameras(CinemachineVirtualCamera leftCam, CinemachineVirtualCamera rightCam, Vector2 exitDirection)
    {
        if (leftCam != null && rightCam != null)
        {
            if (exitDirection.x >= 0f)
            {
                leftCam.Priority = 10;
                rightCam.Priority = 0;
            }
            else if (exitDirection.x <= 0f)
            {
                leftCam.Priority = 0;
                rightCam.Priority = 10;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Vector2 exitDirection = (collider.transform.position - blockade.bounds.center).normalized;

            ToggleCameras(playerFollowCam, hallwayCam, exitDirection);
        }
    }
}
