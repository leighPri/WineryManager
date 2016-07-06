using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UnagedTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public string objectType = "Unaged";

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;

    public int[] outputID; //the ID of the object output by this Consumable

    public UnagedTemplate(int newID, int[] NewOutputID, string newObjectName, string newDescription, string newBuildingNeeded) {
        id = newID;
        objectName = newObjectName;
        outputID = new int[NewOutputID.Length];
        for (int i = 0; i < NewOutputID.Length; i++) {
            outputID[i] = NewOutputID[i];
        }
        description = newDescription;
        buildingNeeded = newBuildingNeeded;
    }

    //to be run on a building, checks to see if this building is valid for this consumable type
    public bool CanBePlaced(Building building) {
        if (buildingNeeded == building.objectType)
            return true;
        else
            return false;
    }

    public int WineSelect(int outputIndex) {
        if (outputID.Length == 1)
            return outputID[0];
        else
            return outputID[outputIndex];
    }

    public void ExampleCall() {
        Debug.Log("My name is " + objectName + " and my ID is " + id.ToString());
        Debug.Log("My description is " + description);
        Debug.Log("I need a(n) " + buildingNeeded + " to be processed.");
    }

}
