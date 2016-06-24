using UnityEngine;
using System.Collections;

public class WineMaster : MonoBehaviour {

    public static WineMaster wineMaster;

    public Wine[] winesOnHand;

    void Awake() {
        if(wineMaster == null) {
            wineMaster = this;

        } else if(wineMaster != this) {
            Destroy(gameObject);
        }
    }

    public void AddBottles(int wineID) {
        winesOnHand[wineID].bottlesOnHand += 100;
    }

    //will be set to 100 bottles in Inspector for testing purposes, make amtToSell someting to pass in later
    public void SellBottles(int wineID) {
        int amtToSell = 100;
        MoneyCtrl.moneyOnHand += winesOnHand[wineID].baseSellValue * amtToSell;
        winesOnHand[wineID].bottlesOnHand -= amtToSell;
    }
}

