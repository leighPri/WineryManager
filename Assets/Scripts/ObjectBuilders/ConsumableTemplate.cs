using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConsumableTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;
    
    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;
    public int parentList = (int)ObjectMaster.listType.Consumable;

    public int outputID; //the ID of the object output by this Consumable

    public ConsumableTemplate(int newID, int newCost, int NewOutputID, string newObjectName, string newDescription, string newBuildingNeeded) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        description = newDescription;
        cost = newCost;
        buildingNeeded = newBuildingNeeded;
    }
    
    //to be run on a building, checks to see if this building is valid for this consumable type
    //public bool CanBePlaced(Building building) {
    //    if (buildingNeeded == building.objectType)
    //        return true;
    //    else
    //        return false;
    //}

    //to be run on a building, checks to see if this building is valid for this consumable type
    //public bool CanBePlaced(Building building) {
    //    if (building.canProcess == (int)ObjectMaster.listType.Consumable)
    //        return true;
    //    else
    //        return false;
    //}

    public void SellBackConsumable() {
        MoneyCtrl.AddMoney(cost);
    }

    //to be run on a building, checks to see if this building is valid for this consumable type
    //public bool CanBePlaced(Building building) {
    //    if (buildingNeeded == building.objectType)
    //        return true;
    //    else
    //        return false;
    //}

    //public Wine WineSelect(int outputIndex) {
    //    if (wineOutputs.Length == 1)
    //        return wineOutputs[0];
    //    else
    //        return wineOutputs[outputIndex];
    //}

    public void ExampleCall() {
        Debug.Log("My name is " + objectName + " and my ID is " + id.ToString());
        Debug.Log("I cost " + cost.ToString());
        Debug.Log("My description is " + description);
        Debug.Log("I need a(n) " + buildingNeeded + " to be processed.");
        Debug.Log("I output " + ObjectMaster.consumableList[outputID].objectName + " when processed.");
    }

}
