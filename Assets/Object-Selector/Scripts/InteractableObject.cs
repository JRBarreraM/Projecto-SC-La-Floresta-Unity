using System.Text;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
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
    private float _height;
    [SerializeField]
    private float _diameter;
    [SerializeField]
    private float _topDiameterNS;
    [SerializeField]
    private float _topDiameterEW;

    [SerializeField]
    private FilterArrow iconPrefab;
    
    private TextMeshProUGUI text;
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
        icon = Instantiate(iconPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.transform.lossyScale.y + 15.0f, this.transform.position.z), Quaternion.identity, transform);
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
        StringBuilder dataShow = new StringBuilder();
        dataShow.Append($"Type: {Type} \n");
        dataShow.Append($"Specie: {Species} \n");
        dataShow.Append($"Common Name: {Sname} \n");
        dataShow.Append($"Diameter min: {Diameter_min} \n");
        dataShow.Append($"Diameter max: {Diameter_max} \n");
        dataShow.Append($"Tree top min: {Tree_top_min} \n");
        dataShow.Append($"Tree top max: {Tree_top_max} \n");
        text.SetText(dataShow);
    }
}