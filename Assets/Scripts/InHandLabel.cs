﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InHandLabel : MonoBehaviour {

    Text inHandText;
	
    void Awake() {
        inHandText = GetComponent<Text>();
    }

    void Start() {
        if (!InHandCtrl.isInHand)
            inHandText.text = "In Hand: ";
    }
    
    void Update () {
        inHandText.text = "In Hand: ";
        //GetComponent<SpriteRenderer>().sprite = null;

        if (InHandCtrl.isInHand)
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building)
                //GetComponent<SpriteRenderer>().sprite = InHandCtrl.inHandCtrl.buildingInHand.spriteArray[InHandCtrl.objectInHand];
                inHandText.text = "In Hand: " + ObjectMaster.buildingList[InHandCtrl.objectInHand].objectName; 
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Consumable)
                inHandText.text = "In Hand: " + ObjectMaster.consumableList[InHandCtrl.objectInHand].objectName;
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Midpoint)
                inHandText.text = "In Hand: " + ObjectMaster.midpointList[InHandCtrl.objectInHand].objectName;
            else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Unaged)
                inHandText.text = "In Hand: " + ObjectMaster.unagedList[InHandCtrl.objectInHand].objectName;
    }
}
