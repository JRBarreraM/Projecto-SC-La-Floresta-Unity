using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTester : MonoBehaviour
{
    private InteractableObject[] interactableObjects;

    private void Awake() {
        interactableObjects = GameObject.FindObjectsOfType<InteractableObject>();
    }

    private void Start() {
        Filter filter1 = new Filter("height", ">", "10");
        Filter filter2 = new Filter("specie", "=", "Mijao");
        Filter filter3 = new Filter("age", "<", "30");

        List<Filter> myFilters = new List<Filter>() { filter1, filter2, filter3 };

        List<InteractableObject> filteredObjects =  FilterController.ProccessFilters(new List<InteractableObject>(interactableObjects), myFilters);

        filteredObjects.ForEach( obj => {
            Debug.Log(obj.Height + " -- " + obj.Age + "--" + obj.Specie);
        });
    }
}
