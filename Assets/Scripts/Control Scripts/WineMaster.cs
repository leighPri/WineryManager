﻿using UnityEngine;
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
}
