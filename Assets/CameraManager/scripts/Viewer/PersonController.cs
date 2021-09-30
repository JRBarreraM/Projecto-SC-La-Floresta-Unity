using UnityEngine;

public class PersonController : MonoBehaviour
{
    public float gravityForce = 0.1f;
    private bool groundCollition = false;
    private bool isFreeCamera = false;

    private void Start()
    {
        MainEventSystem.current.onFreeCamera += EnableFreeCamera;
        MainEventSystem.current.offFreeCamera += DisableFreeCamera;
    }

    private void EnableFreeCamera() { isFreeCamera = true; }
    private void DisableFreeCamera() { isFreeCamera = false; }

    private void FixedUpdate()
    {
        if (!isFreeCamera && !groundCollition) {
            Vector3 movementDir = Vector3.down * gravityForce;
		    transform.Translate (movementDir * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Ground") { groundCollition = true; }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Ground") { groundCollition = false; }
    }
}
