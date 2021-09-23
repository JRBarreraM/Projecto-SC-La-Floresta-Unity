using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FilterController
{
    public static List<InteractableObject> ProccessFilters(List<InteractableObject> Objects, List<Filter> filters) {
        List<InteractableObject> interactableObjects = Objects;

        filters.ForEach(filter => {
            List<InteractableObject> filteredObjects = new List<InteractableObject>();
            interactableObjects.ForEach(obj => {
                switch (filter.name) {
                    case "height":
                        int height_value = Convert.ToInt32(filter.val);
                        switch (filter.op) {
                            case "<":
                                if (obj.Height < height_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Height > height_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Height >= height_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Height <= height_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Height == height_value) filteredObjects.Add(obj);
                                break;
                            case "~=":
                                if (obj.Height != height_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "age":
                        int age_value = Convert.ToInt32(filter.val);
                        switch (filter.op) {
                            case "<":
                                if (obj.Age < age_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Age > age_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Age >= age_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Age <= age_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Age == age_value) filteredObjects.Add(obj);
                                break;
                            case "~=":
                                if (obj.Age != age_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "specie":
                        switch (filter.op) {
                            case "=":
                                if (obj.Specie == filter.val) filteredObjects.Add(obj);
                                break;
                            case "~=":
                                if (obj.Specie != filter.val) filteredObjects.Add(obj);
                                break;
                        }
                        break;
                }
            });
            interactableObjects = filteredObjects;
        });
        
        return interactableObjects;
    }
}
