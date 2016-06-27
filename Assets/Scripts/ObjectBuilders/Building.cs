﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Building : MonoBehaviour {

    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string objectType;

    public bool isProcessing;
    public bool finishedProcessing;
    public Consumable consumableInProcessing;

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

    public void SellBackBuilding()
    {
        MoneyCtrl.AddMoney(cost);
    }

    public void FillBuilding() {
            if (InHandCtrl.consumableInHand.CanBePlaced(this)) {
                isProcessing = true;
                finishedProcessing = false;
                consumableInProcessing = InHandCtrl.consumableInHand;
                InHandCtrl.ClearHand();
            //clears previously-used building if applicable (see BuildingMenuControl.CanClearPrev() comments for breakdown of conditionals)
            if (BuildingMenuControl.CanClearPrev(this))
                BuildingMenuControl.previousBuilding.EmptyBuilding();
            } else {
                Debug.Log("Cannot place " + InHandCtrl.consumableInHand.objectName + " in " + objectName + ", object requires a(n) " + InHandCtrl.consumableInHand.buildingNeeded + " to be processed.");
            }
    }

    public void EmptyBuilding() {
        finishedProcessing = false;
    }

    public void FinishedProcessing() {
        finishedProcessing = true;
        isProcessing = false;
    }

    void OnMouseUpAsButton() {
        if (!isProcessing && InHandCtrl.isInHand) {
            FillBuilding();
        } else {
            BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
            if (objectType == "aging")
                BuildingMenuControl.DisplayAgingOptions(true);
            else
                BuildingMenuControl.DisplayAgingOptions(false);
            //passes this building to the menu so that it can be populated 
            BuildingMenuControl.GetBuilding(this);
        }
    }

    public void DestroyObject() {
        DestroyObject(this);
    }
}
