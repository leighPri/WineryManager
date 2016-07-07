using UnityEngine;
using System.Collections;

public class WineTemplate {

    public int id;
    public string wineName;
    public int bottlesOnHand;
    public int baseSellValue;

    public static void AddBottles(int wineID) {
        ObjectMaster.winesOnHand[wineID].bottlesOnHand += 100;
    }

    //will be set to 100 bottles in Inspector for testing purposes, make amtToSell someting to pass in later
    public static void SellBottles(int wineID) {
        int amtToSell = 100;
        MoneyCtrl.moneyOnHand += ObjectMaster.winesOnHand[wineID].baseSellValue * amtToSell;
        ObjectMaster.winesOnHand[wineID].bottlesOnHand -= amtToSell;
    }
}
