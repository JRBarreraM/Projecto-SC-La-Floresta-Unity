using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject firstPersonObject;
    private GameObject thirdPersonObject;
    private GameObject freeObject;
    private GameObject activeObject;
    private Transform firstPersonCamera;
    private Transform thirdPersonCamera;
    private Transform freeCamera;
    private Transform activeCamera;

    void Awake ()
    {
        firstPersonObject = GameObject.Find("FirstPersonCamera");
        thirdPersonObject = GameObject.Find("ThirdPersonCamera");
        freeObject = GameObject.Find("FreeCamera");

        firstPersonCamera = firstPersonObject.GetComponent<FirstPersonAIO>().playerCamera.transform;
        thirdPersonCamera = thirdPersonObject.GetComponent<BasicBehaviour>().playerCamera;
        freeCamera = freeObject.transform;
    }

    void Start()
    {
        firstPersonObject.SetActive(false);
        freeObject.SetActive(false);

        activeObject = thirdPersonObject;
        activeCamera = thirdPersonCamera;
    }

    void Update()
    {
        if (Input.GetKeyDown("1")) {
            ChangeCamera(firstPersonObject, firstPersonCamera, Vector3.zero);
        }

        if (Input.GetKeyDown("2")) {
            ChangeCamera(thirdPersonObject, thirdPersonCamera, Vector3.zero);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            ChangeCamera(freeObject, freeCamera, Vector3.up);
        }
    }

    void ChangeCamera(GameObject selectedObject, Transform selectedCamera, Vector3 displacement) {
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
