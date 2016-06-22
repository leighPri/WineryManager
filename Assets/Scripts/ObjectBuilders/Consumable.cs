using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Consumable : MonoBehaviour {

    public string objectName; //the name that the player sees
    public string description;
    public int cost; //maybe also use for sell price

    //valid inputs are: "grape" "mush" and "unaged"
    //these will be used elsewhere for conditionals
    public string objectType;
    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string buildingNeeded;

    //sets possible outcomes of this consumable
    //For the sake of conditionals, multiple possibilities for aging must always be at the same index:
    //0 = stainless steel, 1 = oak chips, 2 = oak barrel
    //for objects with only one outcome, ignore the above note
    public Consumable[] outputs;
    public Consumable finalOutput;

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

    //to be run on a building, checks to see if this building is valid for this consumable type
    public bool CanBePlaced(Building building) {
        if (buildingNeeded == building.objectType)
            return true;
        else
            return false;
    }

    public void OutputSelect(int outputIndex) {
        if (outputs.Length == 1) {
            finalOutput = outputs[0];
        } else {
            finalOutput = outputs[outputIndex];
        }
    }

    void OnMouseUpAsButton() {
        Debug.Log(objectName);
    }

    public void DestroyObject() {
        DestroyObject(this);
    }
}
