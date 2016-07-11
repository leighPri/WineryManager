﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string objectType;
    public int parentList = (int)ObjectMaster.listType.Building;

    public bool isProcessing;
    public bool finishedProcessing;
    public bool hasSelectedOutput;
    
    public int canProcess; //use the ObjectMaster enum list
    public int consumableIDInProcessing;
    public Vector3 myPos;

    public Sprite[] spriteArray;
    SpriteRenderer spriteRenderer;

    void Start() {
        SetEnum();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetEnum() {
        if (objectType == "press") {
            canProcess = (int)ObjectMaster.listType.Consumable;
        } else if (objectType == "ferment") {
            canProcess = (int)ObjectMaster.listType.Midpoint;
        } else if (objectType == "aging") {
            canProcess = (int)ObjectMaster.listType.Unaged;
        }
    }
    
    void Update() {
        if (spriteRenderer.sprite == null) {
            spriteRenderer.sprite = spriteArray[id]; //enables visual sprite if an instance of this object has been populated
        }
    }

    //used to set an instance of a Building to the details from the relevant template on the BuildingTemplate list
    public void SetParamsByID(int templateID) {
        id = ObjectMaster.buildingList[templateID].id;
        objectName = ObjectMaster.buildingList[templateID].objectName;
        description = ObjectMaster.buildingList[templateID].description;
        cost = ObjectMaster.buildingList[templateID].cost;
        objectType = ObjectMaster.buildingList[templateID].objectType;

        myPos = gameObject.transform.position;
    }

    public void SetParamsByID(int templateID, BuildingTemplate[] buildList) {
        id = buildList[templateID].id;
        objectName = buildList[templateID].objectName;
        description = buildList[templateID].description;
        cost = buildList[templateID].cost;
        objectType = buildList[templateID].objectType;

        //status variables
        isProcessing = buildList[templateID].isProcessing;
        finishedProcessing = buildList[templateID].finishedProcessing;
        hasSelectedOutput = buildList[templateID].hasSelectedOutput;
        consumableIDInProcessing = buildList[templateID].consumableIDInProcessing;

        myPos = buildList[templateID].myPos;
    }

    //the int passed in in the inspector should be the object ID from ObjectMaster.buildingList
    public void BuyBuilding (int ID) {
        if (MoneyCtrl.CanAfford(ObjectMaster.buildingList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutBuildingInHand(ID);
                MoneyCtrl.SubtractMoney(ObjectMaster.buildingList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    }

    public void FillBuilding() {
        if (canProcess == InHandCtrl.typeOfObject) {
            isProcessing = true;
            finishedProcessing = false;
            consumableIDInProcessing = InHandCtrl.objectInHand;
            InHandCtrl.ClearHand();
            //clears previously-used building if applicable (see BuildingMenuControl.CanClearPrev() comments for breakdown of conditionals)
            if (BuildingMenuControl.CanClearPrev(this))
                BuildingMenuControl.previousBuilding.EmptyBuilding();
        }
        SaveLoad.Save();
    }

    public void EmptyBuilding() {
        finishedProcessing = false;
        isProcessing = false; //This is redundant on purpose, isProcessing should be set to false by FinishedProcessing()
        hasSelectedOutput = false;
    }

    public void FinishedProcessing() {
        if (isProcessing) {
            finishedProcessing = true;
            isProcessing = false;
        }
    }

    void OnMouseUpAsButton() {
        if (!isProcessing && InHandCtrl.isInHand)
            FillBuilding();
        ShowBuildingMenu();
    }

    void ShowBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
        if (objectType == "aging" && !hasSelectedOutput)
            BuildingMenuControl.DisplayAgingOptions(true);
        else
            BuildingMenuControl.DisplayAgingOptions(false);
        //passes this building to the menu so that it can be populated 
        BuildingMenuControl.GetBuilding(this);
        BuildingHolder.HideBuildingHolder(true); //hides buildingholder so the buildings don't interfere with clicks
    }
}
