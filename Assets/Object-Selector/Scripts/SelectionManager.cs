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
    [SerializeField]
    private Camera cam;
    private bool canSetMap;
    private bool canInteract;

    private void Awake(){
        interactMessage = GameObject.Find("InteractMessage");
        text = GameObject.Find("InteractMessage").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        infoDisplay = GameObject.Find("ObjectInfo");
        IgnoreMe = LayerMask.GetMask("Ignore Selection");
        infoDisplayOn = false;
    }

    private void Start(){
        interactMessage.gameObject.SetActive(false);
        infoDisplay.gameObject.SetActive(false);

        canSetMap = true;
        canInteract = true;

        MainEventSystem.current.onEnableCurrentCamera += EnableCurrentCamera;
        MainEventSystem.current.onDisableCameras += HandleDisableCameras;
        MainEventSystem.current.onBeginInteraction += HandleBeginInteraction;
        MainEventSystem.current.onLeaveInteraction += HandleLeaveInteraction;
    }

    private void EnableCurrentCamera() { canSetMap = true; }
    private void HandleDisableCameras() { canSetMap = false; }
    private void HandleBeginInteraction() { canInteract = false; }
    private void HandleLeaveInteraction() { canInteract = true; }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.GetChild(0).transform.position - transform.position;
        Gizmos.DrawRay(cam.transform.position, direction.normalized * 15);
    }

    private void Update(){
        if (canInteract) {
            RaycastHit hit;
            Vector3 direction = transform.GetChild(0).transform.position - transform.position;
            if (Physics.Raycast(cam.transform.position,direction.normalized, out hit, 15f, ~IgnoreMe)){
                var selection = hit.transform;
                if (selection.CompareTag("selectableTag")){
                    interactMessage.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.I) && canSetMap){
                        if (!infoDisplayOn){
                            infoDisplay.gameObject.SetActive(true);
                            text.SetText("Press I to uninteract");
                            selection.GetComponent<InteractableObject>().ShowData();
                            infoDisplayOn = true;
                        }
                        else{
                            infoDisplay.gameObject.SetActive(false);
                            text.SetText("Press I to interact");
                            infoDisplayOn = false;
                        }
                    }
                }else{
                    interactMessage.gameObject.SetActive(false);
                    infoDisplay.gameObject.SetActive(false);
                    text.SetText("Press I to interact");
                    infoDisplayOn = false;
                }
            }else{
                interactMessage.gameObject.SetActive(false);
                infoDisplay.gameObject.SetActive(false);
                text.SetText("Press I to interact");
                infoDisplayOn = false;
            }
        }
    }
}