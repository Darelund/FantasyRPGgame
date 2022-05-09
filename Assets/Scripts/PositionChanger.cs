using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Transform player;
    public Camera cameraCamera;

    public float offsetX = -9;
    public float offsetY = -4;
    private void Awake()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.transform.position = spawnPosition;
            
            cameraCamera.transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, -10);
        }
    }
}
