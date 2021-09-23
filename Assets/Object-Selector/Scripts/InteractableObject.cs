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
    [SerializeField]
    private FilterArrow iconPrefab;
    private FilterArrow icon;

    private void Awake(){
        text = GameObject.Find("ObjectInfo").transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        icon = Instantiate(iconPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.transform.lossyScale.y + 1.5f, this.transform.position.z), Quaternion.identity, transform);
        icon.Hide();
    }

    public void ShowFilter(){
        icon.Show();
        GetComponent<MiniMapWorldObject>().Show();
    }

    public void HideFilter(){
        icon.Hide();
        GetComponent<MiniMapWorldObject>().Hide();
    }

    public void ShowData(){
        dataShow = "";
        dataShow += "Height: " + height.ToString() + "\n";
        dataShow += "Age: " + age.ToString() + "\n";
        dataShow += "Specie: " + specie;
        text.SetText(dataShow);
    }
}