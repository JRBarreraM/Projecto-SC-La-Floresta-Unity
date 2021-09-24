using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilterPanel : MonoBehaviour
{
    private string attribute;
    private string operation;
    private string value;
    private List<Filter> filters = new List<Filter>();
    private InteractableObject[] interactableObjects;

    private void Awake() {
        interactableObjects = GameObject.FindObjectsOfType<InteractableObject>();
    }

    public void SendData(){
        foreach (Transform child in transform.Find("Filters").gameObject.transform){
            attribute = child.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            operation = child.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            value = child.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();

            string clean_value = value.ToString();
            filters.Add(new Filter(attribute, operation, clean_value));
        }

        List<InteractableObject> filteredObjects = FilterController.ProccessFilters(new List<InteractableObject>(interactableObjects), filters);
        FilterController.ActivateFilteredObjects(filteredObjects);
    }
}
