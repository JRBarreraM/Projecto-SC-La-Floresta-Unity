using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIHandler : MonoBehaviour
{
    public void SetFirstPersonCamera() {
        MainEventSystem.current.FirstPersonCameraOn();
    }
    public void SetThirdPersonCamera() {
        MainEventSystem.current.ThirdPersonCameraOn();
    }
    public void SetFreeCamera() {
        MainEventSystem.current.FreeCameraOn();
    }
}
