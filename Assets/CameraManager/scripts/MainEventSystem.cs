using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventSystem : MonoBehaviour
{
    public static MainEventSystem current;

    private void Awake() {
        current = this;
    }

    public event Action onFullMap;
    public void FullMap() {
        if (onFullMap != null) {
            onFullMap();
        }
    }

    public event Action onMiniMap;
    public void MiniMap() {
        if (onMiniMap != null) {
            onMiniMap();
        }
    }

    // First Person Camera Events
    public event Action onFirstPersonCamera;
    public event Action offFirstPersonCamera;
    public void FirstPersonCameraOn() {
        if (onFirstPersonCamera != null) {
            onFirstPersonCamera();
        }
    }
    public void FirstPersonCameraOff() {
        if (offFirstPersonCamera != null) {
            offFirstPersonCamera();
        }
    }

    // Third Person Camera Events
    public event Action onThirdPersonCamera;
    public event Action offThirdPersonCamera;
    public void ThirdPersonCameraOn() {
        if (onThirdPersonCamera != null) {
            onThirdPersonCamera();
        }
    }
    public void ThirdPersonCameraOff() {
        if (offThirdPersonCamera != null) {
            offThirdPersonCamera();
        }
    }

    // Free Person Camera Events
    public event Action onFreeCamera;
    public event Action offFreeCamera;
    public void FreeCameraOn() {
        if (onFreeCamera != null) {
            onFreeCamera();
        }
    }
    public void FreeCameraOff() {
        if (offFreeCamera != null) {
            offFreeCamera();
        }
    }

    public event Action onEnableCurrentCamera;
    public void EnableCurrentCamera() {
        if (onEnableCurrentCamera != null) {
            onEnableCurrentCamera();
        }
    }

    public event Action onDisableCameras;
    public void DisableCameras() {
        if (onDisableCameras != null) {
            onDisableCameras();
        }
    }
}
