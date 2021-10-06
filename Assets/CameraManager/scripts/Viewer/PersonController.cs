using UnityEngine;
using TMPro;

public class PersonController : MonoBehaviour
{
    public float gravityForce = 0.1f;
    private bool groundCollition = false;
    private bool isFreeCamera = false;

    private GameObject infoDisplay;
    private GameObject interactMessage;
    private LayerMask IgnoreMe;
    private TextMeshProUGUI text;
    private bool infoDisplayOn; 
    private bool canSetMap;
    private bool canInteract;

    private void Awake(){
        interactMessage = GameObject.Find("InteractMessage");
        text = GameObject.Find("InteractMessage").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        infoDisplay = GameObject.Find("ObjectInfo");
        IgnoreMe = LayerMask.GetMask("Ignore Selection");
    }

    private void Start()
    {
        infoDisplayOn = false;
        canSetMap = true;
        canInteract = true;

        MainEventSystem.current.onFreeCamera += EnableFreeCamera;
        MainEventSystem.current.offFreeCamera += DisableFreeCamera;
    }

    private void EnableFreeCamera() { isFreeCamera = true; }
    private void DisableFreeCamera() { isFreeCamera = false; }

    private void FixedUpdate()
    {
        if (!isFreeCamera && !groundCollition) {
            Vector3 movementDir = Vector3.down * gravityForce;
		    transform.Translate (movementDir * Time.deltaTime, Space.World);
        }

        Debug.Log(groundCollition);
    }

    private void OnCollisionEnter(Collision collision){
        /* if (collision.gameObject.tag == "Ground") { groundCollition = true; } */
        if (collision.gameObject.tag == "selectableTag") {
            MainEventSystem.current.BeginInteraction();

            interactMessage.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I) && canSetMap){
                if (!infoDisplayOn){
                    infoDisplay.gameObject.SetActive(true);
                    text.SetText("Press I to uninteract");
                    collision.gameObject.GetComponent<InteractableObject>().ShowData();
                    infoDisplayOn = true;
                }
                else{
                    infoDisplay.gameObject.SetActive(false);
                    text.SetText("Press I to interact");
                    infoDisplayOn = false;
                }
            }
        } else {
            interactMessage.gameObject.SetActive(false);
            infoDisplay.gameObject.SetActive(false);
            text.SetText("Press I to interact");
            infoDisplayOn = false;
        }
    }

    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag == "Ground") { groundCollition = true; }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Ground") { groundCollition = false; }
        if (collision.gameObject.tag == "selectableTag") {
            MainEventSystem.current.LeaveInteraction();

            interactMessage.gameObject.SetActive(false);
            infoDisplay.gameObject.SetActive(false);
            text.SetText("Press I to interact");
            infoDisplayOn = false;
        }
    }
}
