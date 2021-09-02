using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string dataShow;
    [SerializeField]
    private int height;
    [SerializeField]
    private int age;
    [SerializeField]
    private string specie;

    private void Awake(){
        text = GameObject.Find("ObjectInfo").transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void ShowData(){
        dataShow = "";
        dataShow += "Height: " + height.ToString() + "\n";
        dataShow += "Age: " + age.ToString() + "\n";
        dataShow += "Specie: " + specie;
        text.SetText(dataShow);
    }
}