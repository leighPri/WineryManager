using UnityEngine;
using System.Collections;

public class WineMaster : MonoBehaviour {

    public static WineMaster wineMaster;

    public static Wine[] winesOnHand;

    void Awake() {
        if(wineMaster == null) {
            wineMaster = this;

        } else if(wineMaster != this) {
            Destroy(gameObject);
        }
    }

    public static void AddBottles(int wineID) {
        winesOnHand[wineID].bottlesOnHand += 100;
    }
}

