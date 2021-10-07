// Credits to Sebastian Lague from https://github.com/SebLague/Blender-to-Unity-Character-Creation

using UnityEngine;

public class ThirdPersonController : MonoBehaviour {
	
	public Transform camera;
	public GameObject mesh;

	[Header("Physics")]
	public float gravity = 0.1f;
	private float velocityY;

	[Header("Movement Setting")]
	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
	float turnSmoothVelocity;
	float speedSmoothVelocity;
	float currentSpeed;

	private bool thirdPersonCameraEnabled = false;
	private bool canMove = true;
	private CharacterController controller;

	Animator animator;

	private void Start() {
		animator = GetComponent<Animator> ();
		controller = GetComponent<CharacterController> ();
		MainEventSystem.current.onThirdPersonCamera += EnableThirdPersonCamera;
        MainEventSystem.current.offThirdPersonCamera += DisableThirdPersonCamera;
		MainEventSystem.current.onDisableCameras += DisableMovement;
    }

	// Third person camera management
    private void EnableThirdPersonCamera() {
        if (!thirdPersonCameraEnabled) {
            MainEventSystem.current.FirstPersonCameraOff();
            thirdPersonCameraEnabled = true;
			canMove = true; 
			mesh.SetActive(true);
        }
    }
    private void DisableThirdPersonCamera() {
        if (thirdPersonCameraEnabled) {
            thirdPersonCameraEnabled = false;
        }
    }

	private void DisableMovement() { canMove = false; }

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

			velocityY += Time.deltaTime * gravity;
			Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

			if (canMove) {
				controller.Move (velocity * Time.deltaTime);
				currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;
				//transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);
			}

			if (controller.isGrounded) {
				velocityY = 0;
			}

			float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
			animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}
	}
}
