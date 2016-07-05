using UnityEngine;
using System.Collections;

public class BuildingTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public string objectType;
    public int cost;

    public BuildingTemplate(int newID, int newCost, string newObjectName, string newDescription, string newObjectType) {
        id = newID;
        cost = newCost;
        objectName = newObjectName;
        description = newDescription;
        objectType = newObjectType;
    }

    public void ExampleCall() {
        Debug.Log("My name is " + objectName + " and my ID is " + id.ToString());
        Debug.Log("I cost " + cost.ToString());
        Debug.Log("My description is " + description);
    }
}
