using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendData(){
        // transform.Find("Filters").gameObject;
        
        foreach (Transform child in transform.Find("Filters").gameObject.transform){

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
