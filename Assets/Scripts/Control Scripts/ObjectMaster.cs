using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ObjectMaster : MonoBehaviour {

    public static ObjectMaster objectMaster;

    public static List<ConsumableTemplate> consumableList = new List<ConsumableTemplate>();
    public static List<BuildingTemplate> buildingList = new List<BuildingTemplate>();
    public static List<MidpointTemplate> midpointList = new List<MidpointTemplate>();
    public static List<UnagedTemplate> unagedList = new List<UnagedTemplate>();
    public static List<VineTemplate> vineList = new List<VineTemplate>();
    public static List<WineTemplate> wineList = new List<WineTemplate>();

    public enum listType { Building, Consumable, Midpoint, Unaged, Vine };

    void Awake() {
        if (objectMaster == null)
            objectMaster = this;
        else if (objectMaster != this)
            Destroy(gameObject);
    }

    //the int passed in in the inspector should be the object ID from ObjectMaster.buildingList
    public void BuyBuilding(int ID) {
        if (MoneyCtrl.CanAfford(ObjectMaster.buildingList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutBuildingInHand(ID);
                InHandCtrl.justBought = true;
                MoneyCtrl.SubtractMoney(ObjectMaster.buildingList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    }

    public void BuyGrape(int ID) {
        if (MoneyCtrl.CanAfford(consumableList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutConsumableInHand(ID);
                InHandCtrl.justBought = true;
                MoneyCtrl.SubtractMoney(consumableList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } 
    }

    public void BuyVine(int ID) {
        if (MoneyCtrl.CanAfford(vineList[ID].cost))
        {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutVineInHand(ID);
                InHandCtrl.justBought = true;
                MoneyCtrl.SubtractMoney(vineList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        }
    }

    //needs to be static
    public static void AddBottles(int wineID) {
        int amtToAdd = 100;
        wineList[wineID].bottlesOnHand += amtToAdd;
        SaveLoad.Save();
    }
    
    //cannot be static because the buttons don't like it
    public void SellBottles(object[] wineIDObj) {
        int wineID = (int)wineIDObj[0];
        int amtToSell = (int)wineIDObj[1]; /*100; //will be set to 100 bottles in Inspector for testing purposes, make amtToSell someting to pass in later*/

        if (wineList[wineID].bottlesOnHand <= 0){
            Debug.Log("You have no " + wineList[wineID].wineName + " to sell!");
            wineList[wineID].bottlesOnHand = 0; //this is in place to double check that bottles on hand does not go into the negative
        }

        if (wineList[wineID].bottlesOnHand < amtToSell)
            amtToSell = wineList[wineID].bottlesOnHand; //only sells as much wine as possible down to 0 and no further

        MoneyCtrl.moneyOnHand += (wineList[wineID].baseSellValue * amtToSell);
        wineList[wineID].bottlesOnHand -= amtToSell;
        MenuControl.tempSellMenuItem.GetComponent<Text>().text = wineList[wineID].bottlesOnHand.ToString();
        SaveLoad.Save();
    }

    public void TryToSellBottles(int wineID, int amtToSell) {
        string confirmText = "Are you sure you want to sell ";
        confirmText += amtToSell + " bottles of ";
        confirmText += wineList[wineID].wineName + "?";

        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, objectMaster, "SellBottles", ConfirmationPanel.confirmPanel.WrapInts(wineID, amtToSell));
    }
}
