using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    private string type;
    [SerializeField]
    private string sname;
    [SerializeField]
    private string species;
    [SerializeField]
    private int diameter_min;
    [SerializeField]
    private int diameter_max;
    [SerializeField]
    private int tree_top_min;
    [SerializeField]
    private int tree_top_max;
    [SerializeField]
    private TextMeshProUGUI text;
    private string dataShow;
    [SerializeField]
    private FilterArrow iconPrefab;
    private FilterArrow icon;

    public string Type { get { return type; } set {} }
    public string Sname { get { return sname; } set {} }
    public string Species { get { return species; } set {} }
    public int Diameter_min { get { return diameter_min; } set {} }
    public int Diameter_max { get { return diameter_max; } set {} }
    public int Tree_top_min { get { return tree_top_min; } set {} }
    public int Tree_top_max { get { return tree_top_max; } set {} }

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
        dataShow += "Type: " + type + "\n";
        dataShow += "Sname: " + sname + "\n";
        dataShow += "Species: " + species + "\n";
        dataShow += "Diameter min: " + diameter_min.ToString() + "\n";
        dataShow += "Diameter max: " + diameter_max.ToString() + "\n";
        dataShow += "Tree top min: " + tree_top_min.ToString() + "\n";
        dataShow += "Tree top max: " + tree_top_max.ToString() + "\n";
        text.SetText(dataShow);
    }
}