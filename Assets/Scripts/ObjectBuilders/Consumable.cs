using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Consumable : MonoBehaviour {

    //the int passed in in the inspector should be the object ID from ObjectMaster.consumableList
    public void BuyGrape(int ID) {
        if (MoneyCtrl.CanAfford(ObjectMaster.consumableList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutConsumableInHand(ID); //replace the above with this eventually
                MoneyCtrl.SubtractMoney(ObjectMaster.consumableList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    }

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;

    public int outputID; //the ID of the object output by this Consumable

    //sets possible outcomes of this consumable
    //For the sake of conditionals, multiple possibilities for aging must always be at the same index:
    //0 = stainless steel, 1 = oak chips, 2 = oak barrel
    //for objects with only one outcome, ignore the above note
    public Wine[] wineOutputs;
    public Consumable midpointOutput;

    //used to set an instance of a Consumable to the details from the relevant template on the BuildingTemplate list
    public void SetParamsByID(int templateID) {
        id = ObjectMaster.consumableList[templateID].id;
        objectName = ObjectMaster.consumableList[templateID].objectName;
        description = ObjectMaster.consumableList[templateID].description;
        cost = ObjectMaster.consumableList[templateID].cost;
        buildingNeeded = ObjectMaster.consumableList[templateID].buildingNeeded;
    }

    ////the int passed in in the inspector should be the object ID from ObjectMaster.consumableList
    //public void BuyGrape(int ID) {
    //    if (MoneyCtrl.CanAfford(ObjectMaster.consumableList[ID].cost)) {
    //        if (!InHandCtrl.isInHand) {
    //            InHandCtrl.PutConsumableInHand(this);
    //            //InHandCtrl.PutConsumableInHand(ID); //replace the above with this eventually
    //            MoneyCtrl.SubtractMoney(ObjectMaster.consumableList[ID].cost);
    //            SceneManager.LoadScene("MainGame");
    //        }
    //    } else
    //        Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    //}

    public void SellBackConsumable() {
        MoneyCtrl.AddMoney(cost);
    }

    //to be run on a building, checks to see if this building is valid for this consumable type
    public bool CanBePlaced(Building building) {
        if (buildingNeeded == building.objectType)
            return true;
        else
            return false;
    }

    public Wine WineSelect(int outputIndex) {
        if (wineOutputs.Length == 1)
            return wineOutputs[0];
        else
            return wineOutputs[outputIndex];
    }

    void OnMouseUpAsButton() {
        Debug.Log(objectName);
    }

    public void DestroyObject() {
        DestroyObject(this);
    }
}
