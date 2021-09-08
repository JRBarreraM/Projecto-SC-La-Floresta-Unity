using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(ThirdPersonController))]
public class CameraManager : MonoBehaviour
{
    private GameObject FPCamera;
    private GameObject TPCamera;
    private FirstPersonController FPController;
    private ThirdPersonController TPController;

    private void Awake() {
        FPCamera = GameObject.Find("FPCamera");
        TPCamera = GameObject.Find("TPCamera");
        FPController = GetComponent<FirstPersonController>();
        TPController = GetComponent<ThirdPersonController>();
    }

    private void Start() {
        MainEventSystem.current.onFirstPersonCamera += EnableFirstPersonCamera;
        MainEventSystem.current.offFirstPersonCamera += DisableFirstPersonCamera;
        MainEventSystem.current.onThirdPersonCamera += EnableThirdPersonCamera;
        MainEventSystem.current.offThirdPersonCamera += DisableThirdPersonCamera;
        MainEventSystem.current.onFreeCamera += EnableFreeCamera;
        MainEventSystem.current.offFreeCamera += DisableFreeCamera;
        
        MainEventSystem.current.ThirdPersonCameraOn();
    }

    private void EnableFirstPersonCamera() { FPCamera.SetActive(true); FPController.enabled = true; }
    private void DisableFirstPersonCamera() { FPCamera.SetActive(false); FPController.enabled = false; }
    private void EnableThirdPersonCamera() { TPCamera.SetActive(true); TPController.enabled = true; }
    private void DisableThirdPersonCamera() { TPCamera.SetActive(false); TPController.enabled = false; }
    private void EnableFreeCamera() { FPCamera.SetActive(true); FPController.enabled = true; }
    private void DisableFreeCamera() { FPCamera.SetActive(false); FPController.enabled = false; }

    private void Update() {
        if (Input.GetKeyDown("1")) {
            MainEventSystem.current.FreeCameraOff();
            MainEventSystem.current.FirstPersonCameraOn();
        }

        if (Input.GetKeyDown("2")) {
            MainEventSystem.current.ThirdPersonCameraOn();
            MainEventSystem.current.FreeCameraOff();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            MainEventSystem.current.FreeCameraOn();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            MainEventSystem.current.DisableCameras();
        }
    }
}
