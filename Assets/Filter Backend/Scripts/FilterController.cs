using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FilterController
{
    public static List<InteractableObject> ProccessFilters(InteractableObject[] interactableObjects, List<Filter> filters) {
        List<InteractableObject> filteredObjects;
        InteractableObject[] currentObjects = interactableObjects;

        filters.ForEach(filter => {
            for (int i = 0; i < interactableObjects.Length; i++) {
                switch (filter.name) {
                    case "height":
                        switch (filter.op) {
                            case "<":
                                if (interactableObjects[i].height < filter.value) filteredObjects.Add(interactableObjects[i]);
                            case ">":
                                if (interactableObjects[i].height > filter.value) filteredObjects.Add(interactableObjects[i]);
                            case ">=":
                                if (interactableObjects[i].height >= filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "<=":
                                if (interactableObjects[i].height <= filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "=":
                                if (interactableObjects[i].height == filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "~=":
                                if (interactableObjects[i].height != filter.value) filteredObjects.Add(interactableObjects[i]);
                        }

                    case "age":
                        switch (filter.op) {
                            case "<":
                                if (interactableObjects[i].age < filter.value) filteredObjects.Add(interactableObjects[i]);
                            case ">":
                                if (interactableObjects[i].age > filter.value) filteredObjects.Add(interactableObjects[i]);
                            case ">=":
                                if (interactableObjects[i].age >= filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "<=":
                                if (interactableObjects[i].age <= filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "=":
                                if (interactableObjects[i].age == filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "~=":
                                if (interactableObjects[i].age != filter.value) filteredObjects.Add(interactableObjects[i]);
                        }

                    case "specie":
                        switch (filter.op) {
                            case "=":
                                if (interactableObjects[i].specie == filter.value) filteredObjects.Add(interactableObjects[i]);
                            case "~=":
                                if (interactableObjects[i].specie != filter.value) filteredObjects.Add(interactableObjects[i]);
                        }
                }
            }

            filteredObjects
        });
    }
}
