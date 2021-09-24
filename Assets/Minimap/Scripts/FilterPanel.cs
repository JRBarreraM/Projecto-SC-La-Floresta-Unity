using System;
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
            attribute = child.transform.Find("Attribute/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            operation = child.transform.Find("Operator/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            value = child.transform.Find("Value").gameObject.GetComponent<TMP_InputField>().text.Trim();
            if(Int32.TryParse(value, out int temp)){
                value = temp.ToString();
            }
            string clean_value = value;
            filters.Add(new Filter(attribute, operation, clean_value));
        }

        List<InteractableObject> filteredObjects = FilterController.ProccessFilters(new List<InteractableObject>(interactableObjects), filters);
        FilterController.ActivateFilteredObjects(filteredObjects);
    }
}
