using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(ThirdPersonController))]
public class CameraManager : MonoBehaviour
{

    private void Start() {
        MainEventSystem.current.ThirdPersonCameraOn();
    }

    private void Update() {
        if (Input.GetKeyDown("1")) {
            MainEventSystem.current.FirstPersonCameraOn();
        }

        if (Input.GetKeyDown("2")) {
            MainEventSystem.current.ThirdPersonCameraOn();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            MainEventSystem.current.FreeCameraOn();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            MainEventSystem.current.DisableCameras();
        }
    }
}
