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

    public event Action onFirstPersonCamera;
    public void FirstPersonCamera() {
        if (onFirstPersonCamera != null) {
            onFirstPersonCamera();
        }
    }

    public event Action onThirdPersonCamera;
    public void ThirdPersonCamera() {
        if (onThirdPersonCamera != null) {
            onThirdPersonCamera();
        }
    }

    public event Action onFreeCamera;
    public void FreeCamera() {
        if (onFreeCamera != null) {
            onFreeCamera();
        }
    }

    public event Action onDisableCameras;
    public void DisableCameras() {
        if (onDisableCameras != null) {
            onDisableCameras();
        }
    }
}
