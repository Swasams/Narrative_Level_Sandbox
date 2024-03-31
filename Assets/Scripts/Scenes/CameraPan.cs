using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private GameObject panTarget;

    [Header("Points")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera playerFollowCam;
    [SerializeField] private CinemachineVirtualCamera panningCam;

    [Header("Settings")]
    [SerializeField] private bool canPan;

    void Start()
    {
        trigger = gameObject.GetComponent<BoxCollider2D>();
    }

    public void PanDown()
    {
        if (!canPan || panTarget == null)
        {
            return;
        }

        panTarget.transform.position = endPoint.position;
    }

    public void PanUp()
    {
        if (!canPan || panTarget == null)
        {
            return;
        }

        panTarget.transform.position = startPoint.position;
    }

    public void SwitchCameras()
    {
        if (playerFollowCam != null && panningCam != null)
        {
            panningCam.Priority = 0;
            playerFollowCam.Priority = 10;
        }
    }

    public void ToggleCameras(CinemachineVirtualCamera followCam, CinemachineVirtualCamera panningCam, Vector2 exitDirection)
    {
        if (followCam != null && panningCam != null)
        {
            if (exitDirection.y >= 0f)
            {
                followCam.Priority = 10;
                panningCam.Priority = 0;
            }
            else if (exitDirection.y <= 0f)
            {
                followCam.Priority = 0;
                panningCam.Priority = 10;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Vector2 exitDirection = (collider.transform.position - trigger.bounds.center).normalized;

            ToggleCameras(playerFollowCam, panningCam, exitDirection);
        }
    }
}
