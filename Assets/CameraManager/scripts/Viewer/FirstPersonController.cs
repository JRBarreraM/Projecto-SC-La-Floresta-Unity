using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    public Transform camera;

    public float camRotationSpeed = 5f;
    public float cameraMinimunY = -60f;
    public float cameraMaximunY = 75f;
    public float rotationSmoothSpeed = 10f;
    float camRotationY;

    public float walkSpeed = 2;
	public float runSpeed = 6;
    public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
	float turnSmoothVelocity;
	float speedSmoothVelocity;
    float currentSpeed;
    float bodyRotationX;

    Vector3 xDir;
    Vector3 yDir;

    private void Update() {
        LookRotation();
        Movement();
    }

    private void LookRotation() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

        bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 movementDir = yDir * input.z * currentSpeed + xDir * input.x * currentSpeed;

		transform.Translate (movementDir * Time.deltaTime, Space.World);
    }
}
