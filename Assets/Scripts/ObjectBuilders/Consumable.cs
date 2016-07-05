using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Consumable : MonoBehaviour {

    public string objectName; //the name that the player sees
    public string description;
    public int cost;
    
    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;

    //sets possible outcomes of this consumable
    //For the sake of conditionals, multiple possibilities for aging must always be at the same index:
    //0 = stainless steel, 1 = oak chips, 2 = oak barrel
    //for objects with only one outcome, ignore the above note
    public Wine[] wineOutputs;
    public Consumable midpointOutput;

    public void BuyGrape () {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutConsumableInHand(this);
                MoneyCtrl.SubtractMoney(cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + objectName);
    }

    public void SellBackConsumable()
    {
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
