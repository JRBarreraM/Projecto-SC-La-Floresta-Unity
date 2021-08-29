using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private GameObject textDisplay;
    private GameObject interactMessage;

    private void Awake(){
        interactMessage = GameObject.Find("InteractMessage");
        textDisplay = GameObject.Find("ObjectInfo");
        interactMessage.gameObject.SetActive(false);
        textDisplay.gameObject.SetActive(false);
    }

    private void Update(){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            var selection = hit.transform;
            if (selection.CompareTag("selectableTag")){
                // var selectionRenderer = selection.GetComponent<Selectable>();
                interactMessage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.I)){
                    textDisplay.gameObject.SetActive(true);
                }
            }else{
                interactMessage.gameObject.SetActive(false);
            }
        }
    }
}