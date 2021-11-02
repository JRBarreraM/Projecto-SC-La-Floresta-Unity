using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FilterController
{
    public static List<InteractableObject> ProccessFilters(List<InteractableObject> objects, List<Filter> filters) {
        List<InteractableObject> interactableObjects = objects;

        filters.ForEach(filter => {
            List<InteractableObject> filteredObjects = new List<InteractableObject>();
            interactableObjects.ForEach(obj => {
                switch (filter.name) {
                    case "diameter_min":
                        int diameter_min_value = 0;
                        try {
                            diameter_min_value = Convert.ToInt32(filter.val);
                        }
                        catch(Exception e) {
                            Debug.Log(e);
                        }

                        switch (filter.op) {
                            case "<":
                                if (obj.Diameter_min < diameter_min_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Diameter_min > diameter_min_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Diameter_min >= diameter_min_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Diameter_min <= diameter_min_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Diameter_min == diameter_min_value) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Diameter_min != diameter_min_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "diameter_max":
                        int diameter_max_value = 0;
                        try {
                            diameter_max_value = Convert.ToInt32(filter.val);
                        }
                        catch(Exception e) {
                            Debug.Log(e);
                        }

                        switch (filter.op) {
                            case "<":
                                if (obj.Diameter_max < diameter_max_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Diameter_max > diameter_max_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Diameter_max >= diameter_max_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Diameter_max <= diameter_max_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Diameter_max == diameter_max_value) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Diameter_max != diameter_max_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "tree_top_min":
                        int tree_top_min_value = 0;
                        try {
                            tree_top_min_value = Convert.ToInt32(filter.val);
                        }
                        catch(Exception e) {
                            Debug.Log(e);
                        }

                        switch (filter.op) {
                            case "<":
                                if (obj.Tree_top_min < tree_top_min_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Tree_top_min > tree_top_min_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Tree_top_min >= tree_top_min_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Tree_top_min <= tree_top_min_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Tree_top_min == tree_top_min_value) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Tree_top_min != tree_top_min_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "tree_top_max":
                        int tree_top_max_value = 0;
                        try {
                            tree_top_max_value = Convert.ToInt32(filter.val);
                        }
                        catch(Exception e) {
                            Debug.Log(e);
                        }

                        switch (filter.op) {
                            case "<":
                                if (obj.Tree_top_max < tree_top_max_value) filteredObjects.Add(obj);
                                break;
                            case ">":
                                if (obj.Tree_top_max > tree_top_max_value) filteredObjects.Add(obj);
                                break;
                            case ">=":
                                if (obj.Tree_top_max >= tree_top_max_value) filteredObjects.Add(obj);
                                break;
                            case "<=":
                                if (obj.Tree_top_max <= tree_top_max_value) filteredObjects.Add(obj);
                                break;
                            case "=":
                                if (obj.Tree_top_max == tree_top_max_value) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Tree_top_max != tree_top_max_value) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "type":
                        switch (filter.op) {
                            case "=":
                                if (obj.Type == filter.val) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Type != filter.val) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "sname":
                        switch (filter.op) {
                            case "=":
                                if (obj.Sname == filter.val) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Sname != filter.val) filteredObjects.Add(obj);
                                break;
                        }
                        break;

                    case "species":
                        switch (filter.op) {
                            case "=":
                                if (obj.Species == filter.val) filteredObjects.Add(obj);
                                break;
                            case "!=":
                                if (obj.Species != filter.val) filteredObjects.Add(obj);
                                break;
                        }
                        break;
                }
            });
            interactableObjects = filteredObjects;
        });
        
        return interactableObjects;
    }

    public static void ActivateFilteredObjects(List<InteractableObject> objects) {
        objects.ForEach( obj => {
            obj.ShowFilter();
        });
    }

    public static void DeactivateFilteredObjects(List<InteractableObject> objects) {
        objects.ForEach( obj => {
            obj.HideFilter();
        });
    }
}
