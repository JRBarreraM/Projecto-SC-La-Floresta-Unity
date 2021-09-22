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
        Filter filter1 = new Filter("height", ">", "25");
        Filter filter2 = new Filter("specie", "=", "Caobo");
        Filter filter3 = new Filter("age", "<", "30");

        List<Filter> myFilters = new List<Filter>() { filter1, filter2, filter3 };

        List<InteractableObject> filteredObjects =  FilterController.ProccessFilters(interactableObjects, myFilters);
    }
}
