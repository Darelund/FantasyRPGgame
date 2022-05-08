using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Target to follow
    public Transform target;
    // How quickly camera moves towards the target
    public float smoothing;
    // Bounderies for camera to not go outside the map
    public Vector2 maxPosition;
    public Vector2 minPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Will move towards target if the object with this scripts position is not equal to targets position
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);

            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            // will move from a to b, so from transform.position to the targets position with smoothing(How quickly it moves towards targets position)
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
