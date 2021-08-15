using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIHandler : MonoBehaviour
{

    public void SetFirstPersonCamera() {
        Debug.Log("Primera");
        MainEventSystem.current.FirstPersonCamera();
    }
    public void SetThirdPersonCamera() {
        Debug.Log("Tercera");
        MainEventSystem.current.ThirdPersonCamera();
    }
    public void SetFreeCamera() {
        Debug.Log("Libre");
        MainEventSystem.current.FreeCamera();
    }
}
