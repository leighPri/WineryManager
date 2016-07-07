﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMaster : MonoBehaviour {

    public static ObjectMaster objectMaster;

    public static List<ConsumableTemplate> consumableList = new List<ConsumableTemplate>();
    public static List<BuildingTemplate> buildingList = new List<BuildingTemplate>();
    public static List<MidpointTemplate> midpointList = new List<MidpointTemplate>();
    public static List<UnagedTemplate> unagedList = new List<UnagedTemplate>();
    public static List<WineTemplate> wineList = new List<WineTemplate>();

    public enum listType { Building, Consumable, Midpoint, Unaged };

    public static void AddBottles(int wineID) {
        wineList[wineID].bottlesOnHand += 100;
    }

    //will be set to 100 bottles in Inspector for testing purposes, make amtToSell someting to pass in later
    //cannot be static because the buttons don't like it
    public void SellBottles(int wineID) {
        int amtToSell = 100;
        if (wineList[wineID].bottlesOnHand <= 0){
            Debug.Log("You have no " + wineList[wineID].wineName + " to sell!");
            wineList[wineID].bottlesOnHand = 0; //this is in place to double check that bottles on hand does not go into the negative
        }
        if (wineList[wineID].bottlesOnHand < amtToSell) {
            amtToSell = wineList[wineID].bottlesOnHand; //only sells as much wine as possible down to 0 and no further
        }
            MoneyCtrl.moneyOnHand += (wineList[wineID].baseSellValue * amtToSell);
            wineList[wineID].bottlesOnHand -= amtToSell;
    }
}
