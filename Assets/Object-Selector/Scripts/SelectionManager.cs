using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    private GameObject infoDisplay;
    private GameObject interactMessage;
    private LayerMask IgnoreMe;
    private TextMeshProUGUI text;
    private bool infoDisplayOn; 

    private void Awake(){
        interactMessage = GameObject.Find("InteractMessage");
        text = GameObject.Find("InteractMessage").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        infoDisplay = GameObject.Find("ObjectInfo");
        interactMessage.gameObject.SetActive(false);
        infoDisplay.gameObject.SetActive(false);
        IgnoreMe = LayerMask.GetMask("Ignore Selection");
        infoDisplayOn = false;
    }

    private void Update(){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 7f, ~IgnoreMe)){
            var selection = hit.transform;
            if (selection.CompareTag("selectableTag")){
                interactMessage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.I)){
                    if (!infoDisplayOn){
                        infoDisplay.gameObject.SetActive(true);
                        text.SetText("Right click to uninteract");
                        selection.GetComponent<InteractableObject>().ShowData();
                        infoDisplayOn = true;
                    }
                    else{
                        infoDisplay.gameObject.SetActive(false);
                        text.SetText("Right click to interact");
                        infoDisplayOn = false;
                    }
                }
            }else{
                interactMessage.gameObject.SetActive(false);
                infoDisplay.gameObject.SetActive(false);
                text.SetText("Right click to interact");
                infoDisplayOn = false;
            }
        }else{
            interactMessage.gameObject.SetActive(false);
            infoDisplay.gameObject.SetActive(false);
            text.SetText("Right click to interact");
            infoDisplayOn = false;
        }
    }
}