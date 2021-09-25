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
    private List<Filter> filters;
    private List<InteractableObject> interactableObjects;
    private List<GameObject> UIFilters = new List<GameObject>();
    private int numOfActiveFilters = 1;
    private GameObject UIContent;

    private void Awake() {
        interactableObjects = new List<InteractableObject>(GameObject.FindObjectsOfType<InteractableObject>());
        UIContent = transform.Find("Content").gameObject;
        int children = transform.Find("Content/Filters").gameObject.transform.childCount;
        Debug.Log(children);
        for (int i = 0; i < children; ++i)
            UIFilters.Add(transform.Find("Content/Filters").gameObject.transform.GetChild(i).gameObject);
        for (int i = 1; i < children; ++i)
            UIFilters[i].SetActive(false);
        UIContent.SetActive(false);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if(UIContent.activeSelf)
                UIContent.SetActive(false);
            else
                UIContent.SetActive(true);
        }
    }

    public void AddFilter(){
        Debug.Log(UIFilters.Count);
        Debug.Log(numOfActiveFilters);
        if (numOfActiveFilters < UIFilters.Count){
            UIFilters[numOfActiveFilters].SetActive(true);
            numOfActiveFilters++;
        }
    }

    public void RemoveFilter(){
        Debug.Log(UIFilters.Count);
        Debug.Log(numOfActiveFilters);
        if (2 <= numOfActiveFilters){
            UIFilters[numOfActiveFilters-1].SetActive(false);
            numOfActiveFilters--;
        }
    }

    public void ResetAppliedFilters(){
        FilterController.DeactivateFilteredObjects(interactableObjects);
    }

    public void SendData(){
        filters = new List<Filter>();
        foreach (Transform child in transform.Find("Content/Filters").gameObject.transform){
            attribute = child.transform.Find("Attribute/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            operation = child.transform.Find("Operator/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
            value = child.transform.Find("Value").gameObject.GetComponent<TMP_InputField>().text.Trim();
            // Aqui Hacer Validaciones del Value
            if(Int32.TryParse(value, out int temp)){
                value = temp.ToString();
            }
            string clean_value = value;
            filters.Add(new Filter(attribute, operation, clean_value));
        }

        List<InteractableObject> filteredObjects = FilterController.ProccessFilters(interactableObjects, filters);
        FilterController.ActivateFilteredObjects(filteredObjects);
    }
}
