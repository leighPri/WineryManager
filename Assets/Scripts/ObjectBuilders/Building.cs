using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Building : MonoBehaviour {

    public string objectName;
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string objectType;

    public static bool isProcessing;
    public static bool finishedProcessing;
    public static Consumable consumableInProcessing;
    public static Consumable outputConsumable;

    public void BuyBuilding () {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutBuildingInHand(this);
                MoneyCtrl.SubtractMoney(cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + objectName);
    }

    public static void fillPress(Consumable consumable) {
        isProcessing = true;
        finishedProcessing = false;
        consumableInProcessing = consumable;
    }

    public static void emptyBuilding() {
        isProcessing = false;
    }

    public static void FinishedProcessing() {
        finishedProcessing = true;
    }

    void OnMouseUpAsButton() {
        if (isProcessing) {
            if (finishedProcessing) {
                //places output prefab in outputConsumable
                Debug.Log("All done!");
            } else if (objectName == "Wine Press") {
                Debug.Log("Currently proccessing " + consumableInProcessing);
            }
        }
        if (InHandCtrl.isInHand) {
            //if this building is valid for the consumable that's trying to be placed on it
            if (InHandCtrl.consumableInHand.CanBePlaced(this)) {
                fillPress(InHandCtrl.consumableInHand);
            } else {
                Debug.Log("Cannot place " + InHandCtrl.consumableInHand.objectName + " in " + this.objectName + ", object requires a(n) " + InHandCtrl.consumableInHand.buildingNeeded + " to be processed.");
            }
        }
    }

    public void DestroyBuilding() {
        DestroyObject(this);
    }
}
