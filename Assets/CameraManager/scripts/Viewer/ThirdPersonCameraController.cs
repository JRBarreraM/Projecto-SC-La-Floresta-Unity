// Credits to Sebastian Lague from https://github.com/SebLague/Blender-to-Unity-Character-Creation

using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour {

	[Header("Camera Setting")]
	public float mouseSensitivity = 10;
	public Transform target;
	public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2 (-40, 85);
	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	private bool thirdPersonCameraEnabled = false;

	void Start() {
		MainEventSystem.current.onThirdPersonCamera += EnableThirdPersonCamera;
        MainEventSystem.current.offThirdPersonCamera += DisableThirdPersonCamera;
        MainEventSystem.current.onDisableCameras += DisableCamera;
	}

	// Third person camera management
	private void EnableThirdPersonCamera() {
        if (!thirdPersonCameraEnabled) {
            thirdPersonCameraEnabled = true;
            CursorVisibility (false);
            CursonMode(false);
        }
    }
    private void DisableThirdPersonCamera() {
        if (thirdPersonCameraEnabled) {
            thirdPersonCameraEnabled = false;
        }
    }

	// Cursor management
    private void DisableCamera() {
		thirdPersonCameraEnabled = false;
        CursorVisibility (true);
        CursonMode(false);
    }

	private void LateUpdate () {
		if (thirdPersonCameraEnabled) {
			yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
			pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

			currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
			transform.eulerAngles = currentRotation;

			transform.position = target.position - transform.forward * dstFromTarget;
		}
	}

	private void CursorVisibility(bool visibility) {
        Cursor.visible = visibility;
    }

    private void CursonMode(bool mode) {
        if (mode) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

}
