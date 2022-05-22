using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : Interactable
{
    // Reference to the intermediate dialog value
    [SerializeField] private TextAssetValue dialogValue;
    // Reference to the NPC's dialog
    [SerializeField] private TextAsset myDialog;
    // Notification to send to the canvases to activate and check dialog
    [SerializeField] private SignalObserver branchingDialogNotification;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                dialogValue.value = myDialog;
                branchingDialogNotification.Raise();
            }
        }
    }
}
