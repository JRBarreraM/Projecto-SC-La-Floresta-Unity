using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject firstPersonObject;
    private GameObject thirdPersonObject;
    private GameObject freeObject;

    private Transform firstPersonCamera;
    private Transform thirdPersonCamera;
    private Transform freeCamera;

    private FirstPersonAIO firstPersonCameraScript;
    private ThirdPersonOrbitCamBasic thirdPersonCameraScript;
    private FreeFlyCamera freeCameraScript;

    private GameObject activeObject;
    private Transform activeCamera;

    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private void Awake () {
        // Referencias de los objetos para cada camara
        firstPersonObject = GameObject.Find("FirstPersonCamera");
        thirdPersonObject = GameObject.Find("ThirdPersonCamera");
        freeObject = GameObject.Find("FreeCamera");
        // Referencias de cada camara
        firstPersonCamera = firstPersonObject.GetComponent<FirstPersonAIO>().playerCamera.transform;
        thirdPersonCamera = thirdPersonObject.GetComponent<BasicBehaviour>().playerCamera;
        freeCamera = freeObject.transform;
        // Referencias de cada script de camara
        firstPersonCameraScript = firstPersonObject.GetComponent<FirstPersonAIO>();
        thirdPersonCameraScript = thirdPersonObject.transform.GetChild(2).GetComponent<ThirdPersonOrbitCamBasic>();
        freeCameraScript = freeObject.GetComponent<FreeFlyCamera>();
    }

    private void Start() {
        // Suscripcion a los eventos de camara
        MainEventSystem.current.onFirstPersonCamera += SetFirstPersonCamera;
        MainEventSystem.current.onThirdPersonCamera += SetThirdPersonCamera;
        MainEventSystem.current.onFreeCamera += SetFreeCamera;
        MainEventSystem.current.onDisableCameras += DisableCameras;
        
        // Se inicializa en la tercera persona
        firstPersonObject.SetActive(false);
        freeObject.SetActive(false);
        activeObject = thirdPersonObject;
        activeCamera = thirdPersonCamera;
    }

    private void SetFirstPersonCamera() {
        ChangeCamera(firstPersonObject, firstPersonCamera, Vector3.zero);
    }
    private void SetThirdPersonCamera() {
        ChangeCamera(thirdPersonObject, thirdPersonCamera, Vector3.zero);
    }
    private void SetFreeCamera() {
        ChangeCamera(freeObject, freeCamera, Vector3.up);
    }
    private void DisableCameras() {
        firstPersonCameraScript.enabled = !firstPersonCameraScript.enabled;
        thirdPersonCameraScript.enabled = !thirdPersonCameraScript.enabled;
        freeCameraScript.enabled = !freeCameraScript.enabled;

        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void Update() {
        if (Input.GetKeyDown("1")) {
            MainEventSystem.current.FirstPersonCamera();
        }

        if (Input.GetKeyDown("2")) {
            MainEventSystem.current.ThirdPersonCamera();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            MainEventSystem.current.FreeCamera();
        }

        if (Input.GetKeyDown("space")) {
            MainEventSystem.current.DisableCameras();
        }
    }

    void ChangeCamera(GameObject selectedObject, Transform selectedCamera, Vector3 displacement) {
        if (selectedObject != activeObject) {
            activeObject.SetActive(false);
            Debug.Log("Before");
            Debug.Log(activeCamera.localRotation);
            selectedObject.transform.position = activeObject.transform.position + displacement;

            selectedCamera.localRotation = Quaternion.Euler(
                selectedCamera.localRotation.x,
                activeCamera.localRotation.y,
                selectedCamera.localRotation.z
            );

            activeObject = selectedObject;
            activeCamera = selectedCamera;
            activeObject.SetActive(true);
            Debug.Log("After");
            Debug.Log(activeCamera.localRotation);
        }
    }
}
