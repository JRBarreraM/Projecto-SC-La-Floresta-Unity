using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterArrow : MonoBehaviour
{
    private Camera FPCamera;
    private Camera TPCamera;
    private bool OnTPCamera;
    // Start is called before the first frame update
    private void Awake() {
        FPCamera = GameObject.Find("FPCamera").GetComponent<Camera>();
        TPCamera = GameObject.Find("TPCamera").GetComponent<Camera>();
    }

    private void Start() {
        MainEventSystem.current.onFirstPersonCamera += EnableFirstPersonCamera;
        MainEventSystem.current.onThirdPersonCamera += EnableThirdPersonCamera;
        MainEventSystem.current.onFreeCamera += EnableFreeCamera;
    }

    private void EnableThirdPersonCamera() { OnTPCamera = true; }
    private void EnableFirstPersonCamera() { OnTPCamera = false; }
    private void EnableFreeCamera() { OnTPCamera = false; }

    public void Show(){
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide(){
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTPCamera){
            Vector3 targetVector = this.transform.position - TPCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(targetVector, TPCamera.transform.rotation * Vector3.up);
        }else{
            Vector3 targetVector = this.transform.position - TPCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(targetVector, TPCamera.transform.rotation * Vector3.up);
        }
    }
}