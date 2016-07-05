using UnityEngine;
using System.Collections;

public class ConsumableTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;
    
    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;

    public int outputID; //the ID of the object output by this Consumable

    public ConsumableTemplate(int newID, int newCost, int NewOutputID, string newObjectName, string newDescription, string newBuildingNeeded) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        description = newDescription;
        cost = newCost;
        buildingNeeded = newBuildingNeeded;
    }

    //sets possible outcomes of this consumable
    //For the sake of conditionals, multiple possibilities for aging must always be at the same index:
    //0 = stainless steel, 1 = oak chips, 2 = oak barrel
    //for objects with only one outcome, ignore the above note
    //public Wine[] wineOutputs;
    //public Consumable midpointOutput;

    public void ExampleCall() {
        Debug.Log("My name is " + objectName + " and my ID is " + id.ToString());
        Debug.Log("I cost " + cost.ToString());
        Debug.Log("My description is " + description);
        Debug.Log("I need a(n) " + buildingNeeded + " to be processed.");
        Debug.Log("I output " + ObjectMaster.consumableList[outputID].objectName + " when processed.");
    }

}
