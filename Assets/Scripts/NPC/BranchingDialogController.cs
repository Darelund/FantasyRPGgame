using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class BranchingDialogController : MonoBehaviour
{
    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private GameObject choiceHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas()
    {
        branchingCanvas.SetActive(true);
        SetStory();
    }

    public void SetStory()
    {
        if(dialogValue.value)
        {
            myStory = new Story(dialogValue.value.text);
        }
        else
        {
            Debug.Log("Something went wrong with the story setup");
        }
    }

    public void RefreshView()
    {


    }

    void MakeNewDialog(string newDialog)
    {
        DialogObject newDialogobject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();

    }
}
