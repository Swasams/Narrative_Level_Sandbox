using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the follower follows the player
    public Vector3 offset; // Offset from the player

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the target position for the follower
            Vector3 targetPosition = playerTransform.position + offset;

            // Move the follower towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
        }
    }
}
