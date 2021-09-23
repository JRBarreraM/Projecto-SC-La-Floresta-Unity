using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    
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

    [Header("Camera Setting")]
    public bool controllerMode = false;
    public float camRotationSpeed = 5f;
    public float cameraMinimunY = -60f;
    public float cameraMaximunY = 75f;
    float camRotationY;
    float bodyRotationX;

    Vector3 xDir;
    Vector3 yDir;

    private bool firstPersonCameraEnabled = false;
    private bool freeCameraEnabled = false;

    private void Start() {
        // Suscripcion a los eventos de camara
        MainEventSystem.current.onFirstPersonCamera += EnableFirstPersonCamera;
        MainEventSystem.current.offFirstPersonCamera += DisableFirstPersonCamera;
        MainEventSystem.current.onFreeCamera += EnableFreeCamera;
        MainEventSystem.current.offFreeCamera += DisableFreeCamera;
        MainEventSystem.current.onDisableCameras += DisableCamera;
    }

    // First person camera management
    private void EnableFirstPersonCamera() {
        if (!firstPersonCameraEnabled) {
            MainEventSystem.current.ThirdPersonCameraOff();
            firstPersonCameraEnabled = true;
            CursorVisibility (false);
            CursonMode(true);
            mesh.SetActive(false);
        }
    }
    private void DisableFirstPersonCamera() {
        if (firstPersonCameraEnabled) {
            firstPersonCameraEnabled = false;
        }
    }

    // Free camera management
    private void EnableFreeCamera() {
        if (!freeCameraEnabled) {
            MainEventSystem.current.ThirdPersonCameraOff();
            freeCameraEnabled = true;
            CursorVisibility (false);
            CursonMode(true);
            mesh.SetActive(false);
        }
    }
    private void DisableFreeCamera() {
        if (freeCameraEnabled) {
            freeCameraEnabled = false;
        }
    }

    // Cursor management
    private void DisableCamera() {
        firstPersonCameraEnabled = false;
        freeCameraEnabled = false;
        CursorVisibility (true);
        CursonMode(false);
    }

    private void Update() {
        if (firstPersonCameraEnabled || freeCameraEnabled) {
            LookRotation();
            Movement();
        }
    }

    private void LookRotation() {
        bodyRotationX += Input.GetAxis("Mouse X") * camRotationSpeed;
        camRotationY += Input.GetAxis("Mouse Y") * camRotationSpeed;
        camRotationY = Mathf.Clamp(camRotationY, cameraMinimunY, cameraMaximunY);

        Quaternion camTargetRotation = Quaternion.Euler(-camRotationY, 0, 0);
        Quaternion bodyTargetRotation = Quaternion.Euler(0, bodyRotationX, 0);

        camera.localRotation = Quaternion.Lerp(camera.localRotation, camTargetRotation, Time.deltaTime * camRotationSpeed);
        transform.rotation = Quaternion.Lerp(bodyTargetRotation, transform.rotation, Time.deltaTime);
    }

    private void Movement() {
        xDir = camera.right;
        xDir.y = 0;
        xDir.Normalize();

        yDir = camera.forward;
        yDir.y = 0;
        yDir.Normalize();

        Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		Vector3 inputDir = input.normalized;

        if (Input.GetKey(KeyCode.Q)) input.y = 1;
        if (Input.GetKey(KeyCode.E)) input.y = -1;
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E)) input.y = 0;

        bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 upMovement = Vector3.zero;

        if (freeCameraEnabled){
            upMovement = Vector3.up * input.y * walkSpeed;
        }

        Vector3 movementDir = yDir * input.z * currentSpeed + xDir * input.x * currentSpeed + upMovement;

		transform.Translate (movementDir * Time.deltaTime, Space.World);
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
