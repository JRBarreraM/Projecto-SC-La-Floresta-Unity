using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIHandler : MonoBehaviour
{
    private void Start() {
        MainEventSystem.current.onFirstPersonCamera += SetFirstPersonCamera;
        MainEventSystem.current.onThirdPersonCamera += SetThirdPersonCamera;
        MainEventSystem.current.onFreeCamera += SetFreeCamera;
    }

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
