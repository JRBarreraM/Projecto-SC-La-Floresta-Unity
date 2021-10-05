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
        UIContent = transform.Find("FilterContent").gameObject;
        int children = transform.Find("FilterContent/Filters").gameObject.transform.childCount;
        for (int i = 0; i < children; ++i)
            UIFilters.Add(transform.Find("FilterContent/Filters").gameObject.transform.GetChild(i).gameObject);
        for (int i = 1; i < children; ++i)
            UIFilters[i].SetActive(false);
        UIContent.SetActive(false);
    }

    /* private void Start(){
        TMP_InputField value = transform.Find("Content/Filters/Filter/Value").gameObject.transform.GetComponent<TMP_InputField>();
        value.text = "15";
        SendData();
    } */

    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if(UIContent.activeSelf){
                UIContent.SetActive(false);
                MainEventSystem.current.EnableCurrentCamera();
            }
            else {
                UIContent.SetActive(true);
                MainEventSystem.current.DisableCameras();
            }
        }
    }

    public void AddFilter(){
        if (numOfActiveFilters < UIFilters.Count){
            UIFilters[numOfActiveFilters].SetActive(true);
            numOfActiveFilters++;
        }
    }

    public void RemoveFilter(){
        if (2 <= numOfActiveFilters){
            UIFilters[numOfActiveFilters-1].SetActive(false);
            numOfActiveFilters--;
        }
    }

    public void ResetAppliedFilters(){
        FilterController.DeactivateFilteredObjects(interactableObjects);
    }

    private string inputValidator(string attribute, string operation, string value){
        if (value == "")
            return "No value detected";
        int temp;
        string error = "";
        switch (attribute)
        {
            case "height":
                if(Int32.TryParse(value, out temp)){
                    if (temp < 0){
                        error = "Input must be positive integer";
                    }
                }else{
                    error = "Input must be numeric for attribute";
                }
                break;
            case "age":
                if(Int32.TryParse(value, out temp)){
                    if (temp < 0){
                        error = "Input must be positive integer";
                    }
                }else{
                    error = "Input must be numeric for attribute";
                }
                break;
            case "specie":
                switch (operation)
                {
                    case ">":
                        error = "Invalid operator for attribute";
                        break;
                    case "<":
                        error = "Invalid operator for attribute";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return error;
    }

    public void SendData(){
        filters = new List<Filter>();
        bool errorFound = false;
        foreach (Transform child in transform.Find("FilterContent/Filters").gameObject.transform){
            if (child.gameObject.activeSelf){
                attribute = child.Find("Attribute/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
                operation = child.Find("Operator/Label").gameObject.GetComponent<TextMeshProUGUI>().text.ToString().Trim();
                value = child.Find("Value").gameObject.GetComponent<TMP_InputField>().text.Trim();
                TextMeshProUGUI errorMsg = child.Find("ErrorMsg").gameObject.GetComponent<TextMeshProUGUI>();
                errorMsg.text = (inputValidator(attribute,operation,value));
                if (errorMsg.text != ""){
                    errorFound = true;
                    continue;
                }
                if(Int32.TryParse(value, out int temp)){
                    value = temp.ToString();
                }
                string clean_value = value;
                filters.Add(new Filter(attribute, operation, clean_value));
            }
        }
        if (errorFound)
            return;
        List<InteractableObject> filteredObjects = FilterController.ProccessFilters(interactableObjects, filters);
        ResetAppliedFilters();
        FilterController.ActivateFilteredObjects(filteredObjects);
    }
}
