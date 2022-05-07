using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    // Offsets 
    public Vector2 minCameraChange;
    public Vector2 maxCameraChange;
    public Vector3 playerChange;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Reference to CameraMovement script
    private CameraMovement cam; 


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check to see if player is in trigger zone, if thats true then we want to change camera offset
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += minCameraChange;
            cam.maxPosition += maxCameraChange;
            other.transform.position += playerChange;
            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
