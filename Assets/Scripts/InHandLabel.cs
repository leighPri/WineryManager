﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InHandLabel : MonoBehaviour {

    Text inHandText;
	
    void Awake() {
        inHandText = GetComponentInChildren<Text>();
    }

    void Start() {
        if (!InHandCtrl.isInHand)
            inHandText.text = "";
    }
    
    void Update () {
        inHandText.text = "";

        if (InHandCtrl.isInHand)
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building)
                inHandText.text = "In Hand: " + ObjectMaster.buildingList[InHandCtrl.objectInHand].objectName; 
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Consumable)
                inHandText.text = "In Hand: " + ObjectMaster.consumableList[InHandCtrl.objectInHand].objectName;
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Midpoint)
                inHandText.text = "In Hand: " + ObjectMaster.midpointList[InHandCtrl.objectInHand].objectName;
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Unaged)
                inHandText.text = "In Hand: " + ObjectMaster.unagedList[InHandCtrl.objectInHand].objectName;
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Vine)
                inHandText.text = "In Hand: " + ObjectMaster.vineList[InHandCtrl.objectInHand].objectName;
    }
}
