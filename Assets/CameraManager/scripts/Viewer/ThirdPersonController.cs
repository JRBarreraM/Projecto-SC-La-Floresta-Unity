// Credits to Sebastian Lague from https://github.com/SebLague/Blender-to-Unity-Character-Creation

using UnityEngine;

public class ThirdPersonController : MonoBehaviour {
	
	public Transform camera;
	public GameObject mesh;

	[Header("Movement Setting")]
	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
	float turnSmoothVelocity;
	float speedSmoothVelocity;
	float currentSpeed;

	private bool thirdPersonCameraEnabled = false;

	Animator animator;

	private void Start() {
		animator = GetComponent<Animator> ();
		MainEventSystem.current.onThirdPersonCamera += EnableThirdPersonCamera;
        MainEventSystem.current.offThirdPersonCamera += DisableThirdPersonCamera;
    }

	// Third person camera management
    private void EnableThirdPersonCamera() {
        if (!thirdPersonCameraEnabled) {
            MainEventSystem.current.FirstPersonCameraOff();
            thirdPersonCameraEnabled = true;
			mesh.SetActive(true);
        }
    }
    private void DisableThirdPersonCamera() {
        if (thirdPersonCameraEnabled) {
            thirdPersonCameraEnabled = false;
        }
    }

	private void Update () {
		if (thirdPersonCameraEnabled) {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 inputDir = input.normalized;

			if (inputDir != Vector2.zero) {
				float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			}

			bool running = Input.GetKey (KeyCode.LeftShift);
			float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
			currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

			transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);

			float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
			animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}
	}
}
