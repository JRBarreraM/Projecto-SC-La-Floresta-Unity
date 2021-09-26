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

    private bool isCameraEnabled;
    private enum CurentCamera {first, third, free};
    private CurentCamera currentCamera;

    private GameObject cameraPanel;
    private bool isCameraPanelEnabled;

    private void Awake() {
        FPCamera = GameObject.Find("FPCamera");
        TPCamera = GameObject.Find("TPCamera");
        cameraPanel = GameObject.Find("CameraPanel");
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
        MainEventSystem.current.onEnableCurrentCamera += EnableCurrentCamera;
        
        StartCoroutine(InitializeCamera());
    }

    IEnumerator InitializeCamera() {
        yield return new WaitForSeconds(0.001f);
        MainEventSystem.current.ThirdPersonCameraOn();
        currentCamera = CurentCamera.third;
        isCameraEnabled = true;
        cameraPanel.SetActive(false);
        isCameraPanelEnabled = false;
    }

    private void EnableFirstPersonCamera() { FPCamera.SetActive(true); FPController.enabled = true; currentCamera = CurentCamera.first; cameraPanel.SetActive(false); isCameraPanelEnabled = false; }
    private void DisableFirstPersonCamera() { FPCamera.SetActive(false); FPController.enabled = false; }
    private void EnableThirdPersonCamera() { TPCamera.SetActive(true); TPController.enabled = true; currentCamera = CurentCamera.third; cameraPanel.SetActive(false); isCameraPanelEnabled = false; }
    private void DisableThirdPersonCamera() { TPCamera.SetActive(false); TPController.enabled = false; }
    private void EnableFreeCamera() { FPCamera.SetActive(true); FPController.enabled = true; currentCamera = CurentCamera.free; cameraPanel.SetActive(false); isCameraPanelEnabled = false; }
    private void DisableFreeCamera() { FPCamera.SetActive(false); FPController.enabled = false; }
    private void EnableCurrentCamera() {
        if (currentCamera == CurentCamera.first) MainEventSystem.current.FirstPersonCameraOn();
        if (currentCamera == CurentCamera.third) MainEventSystem.current.ThirdPersonCameraOn();
        if (currentCamera == CurentCamera.free) MainEventSystem.current.FreeCameraOn();
    }

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
            if (isCameraEnabled) MainEventSystem.current.DisableCameras();
            else MainEventSystem.current.EnableCurrentCamera();
            isCameraEnabled = !isCameraEnabled;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if (isCameraPanelEnabled) {
                MainEventSystem.current.EnableCurrentCamera();
                cameraPanel.SetActive(false);
            }
            else {
                MainEventSystem.current.DisableCameras();
                cameraPanel.SetActive(true);
            }
            isCameraPanelEnabled = !isCameraPanelEnabled;
        }
    }
}
