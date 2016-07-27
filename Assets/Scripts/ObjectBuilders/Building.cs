﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    public static Building staticBuilding;

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //accepts 0, 1, or 2 used to pick an output from the Consumable output array
    public int selectedOutput;

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

    public PopUpControl popUp;

    //These values are used for timing
    public float timeConsumableIsPlaced;  //marks time consumable is placed on building
    public float tempTimerValue = 5f;  //this is a placeholder and should be filled with the JSON value of how long a consumable takes to process
    public float timeRemainingTilComplete;
    public bool timeConsumableTimerComplete = false;
    public int roundedTimeTilComplete;

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
        } else if (objectType == "vineyard") {
            canProcess = (int)ObjectMaster.listType.Vine;
        }
    }
    
    void Update() {
        //enables and disables colliders based on whether or not panels are active (only on MainGame)
        if ((SceneManager.GetActiveScene().buildIndex == 2 && UIControl.panelIsActive) || (InHandCtrl.isInHand && InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building))
            GetComponent<BoxCollider2D>().enabled = false;
        else
            GetComponent<BoxCollider2D>().enabled = true;

        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = spriteArray[id]; //enables visual sprite if an instance of this object has been populated
        if (isProcessing && !timeConsumableTimerComplete)
                TimerCheck();

        //hides and shows pop up based on whether or not something is available to retrieve
        if (!isProcessing && finishedProcessing) {
            popUp.gameObject.SetActive(true);
            popUp.DisplayProcessedItem(GetProcessingIcon());
            if (InHandCtrl.isInHand && BuildingCtrl.prevBuild == this)
                popUp.gameObject.SetActive(false);
        } else
            popUp.gameObject.SetActive(false);
    }

    public int GetProcessingIcon() {
        if (canProcess == (int)ObjectMaster.listType.Vine)
            return 0;
        else if (canProcess == (int)ObjectMaster.listType.Consumable)
            return 1;
        else if (canProcess == (int)ObjectMaster.listType.Midpoint)
            return 2;
        else if (canProcess == (int)ObjectMaster.listType.Unaged)
            return 3;
        else
            return 0; //this should never trigger

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

    public void SetParams(BuildingTemplate buildTempl) {
        id = buildTempl.id;
        objectName = buildTempl.objectName;
        description = buildTempl.description;
        cost = buildTempl.cost;
        objectType = buildTempl.objectType;

        //status variables
        isProcessing = buildTempl.isProcessing;
        finishedProcessing = buildTempl.finishedProcessing;
        hasSelectedOutput = buildTempl.hasSelectedOutput;
        consumableIDInProcessing = buildTempl.consumableIDInProcessing;

        myPos = new Vector3(buildTempl.myPos[0], buildTempl.myPos[1], buildTempl.myPos[2]);
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
        //fills only if the object in hand is compatible and if this building is not currently processing anything and if it's not holding a finished product waiting to be picked up
        if (canProcess == InHandCtrl.typeOfObject && isProcessing == false && finishedProcessing == false) {
            isProcessing = true;
            finishedProcessing = false;
            consumableIDInProcessing = InHandCtrl.objectInHand;
            timeConsumableIsPlaced = Time.time;

            InHandCtrl.ClearHand();
            //clears previously-used building if applicable (see BuildingCtrl.CanClearPrev() comments for breakdown of conditionals)
            if (BuildingCtrl.CanClearPrev(this))
                BuildingCtrl.prevBuild.EmptyBuilding();
            SaveLoad.Save();
        }
    }
    public void EmptyBuilding() {
        finishedProcessing = false;
        isProcessing = false; //This is redundant on purpose, isProcessing should be set to false by FinishedProcessing()
        hasSelectedOutput = false;
        timeConsumableTimerComplete = false;

        if(canProcess == (int)ObjectMaster.listType.Vine) {
            isProcessing = true;
            timeConsumableIsPlaced = Time.time;
        }
    }

    public void ForceEmptyBuliding(GameObject justToMakeItCompile) {
        finishedProcessing = false;
        isProcessing = false;
        hasSelectedOutput = false;
        timeConsumableTimerComplete = false;
    }

    public void FinishedProcessing() {
        if (isProcessing) {
            finishedProcessing = true;
            isProcessing = false;
        }
    }

    //lets the aging buttons set their desired outputs
    //does not function if there is not something currently being processed
    public void SetOutput(object output) {
        if (isProcessing) {
            selectedOutput = (int)output;
            //sets time upon selection
            timeConsumableIsPlaced = Time.time;
            hasSelectedOutput = true;
            //hides the whole set once a button is clicked as the user should only be able to select one option
            if (BuildingMenuControl.buildingMenuCtrl.gameObject.activeSelf)
                BuildingMenuControl.DisplayAgingOptions(false);
        }
    }

    //places output in hand
    public void GetOutput() {
        if (finishedProcessing) {
            if (canProcess == (int)ObjectMaster.listType.Consumable) { //if Wine Press
                InHandCtrl.PutMidpointInHand(ObjectMaster.consumableList[consumableIDInProcessing].outputID);
            } else if (canProcess == (int)ObjectMaster.listType.Midpoint) { //if Fermentation Shed
                InHandCtrl.PutUnagedWineInHand(ObjectMaster.midpointList[consumableIDInProcessing].outputID);

            } else if (canProcess == (int)ObjectMaster.listType.Unaged) { //if Aging Barn
                ObjectMaster.AddBottles(ObjectMaster.unagedList[consumableIDInProcessing].outputID[selectedOutput]); //adds 100 bottles to the proper wine in the wine list
                Debug.Log("You now have " + ObjectMaster.wineList[ObjectMaster.unagedList[consumableIDInProcessing].outputID[selectedOutput]].bottlesOnHand + " bottles of " + ObjectMaster.wineList[ObjectMaster.unagedList[consumableIDInProcessing].outputID[selectedOutput]].wineName + " available to sell.");
                EmptyBuilding(); //aging barns clear themselves because there is nowhere else for wines to go except into storage (currently)
            } else if (canProcess == (int)ObjectMaster.listType.Vine) { //if vineyard
                InHandCtrl.PutConsumableInHand(ObjectMaster.consumableList[consumableIDInProcessing].outputID);
            }

            BuildingCtrl.prevBuild = this;
            HideBuildingMenu(); //hides the Building Menu if it's active
        }
    }

    //Fills building and only displays building menu when it's an aging barn (because you have to select an option)
    //If it is not doing the fill action, displays building menu anyway on click
    void OnMouseUpAsButton() {
        if (!isProcessing && !finishedProcessing && InHandCtrl.isInHand) {
            FillBuilding();
            if (canProcess == (int)ObjectMaster.listType.Unaged)
                ShowBuildingMenu();
        } else
            ShowBuildingMenu();
    }

    public void ShowBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
        UIControl.panelIsActive = true;
        if (objectType == "aging" && !hasSelectedOutput)
            BuildingMenuControl.DisplayAgingOptions(true);
        else
            BuildingMenuControl.DisplayAgingOptions(false);
        //passes this building to the menu so that it can be populated 
        BuildingMenuControl.GetBuilding(this);
    }

    public void HideBuildingMenu() {
        //only works if the menu is enabled
        if (BuildingMenuControl.buildingMenuCtrl.gameObject.activeSelf) {
            BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(false);
            UIControl.panelIsActive = false;
        }
    }

    public void DemolishBuilding(object toDestroy) {
        HideBuildingMenu();
        Destroy((GameObject)toDestroy);
    }

    public void TimerCheck() {
        //gets how much time has elapsed since consumable placed
        float timeCheck = Time.time - timeConsumableIsPlaced;

        timeRemainingTilComplete = tempTimerValue - timeCheck;

        roundedTimeTilComplete = (int)timeRemainingTilComplete;

        if (timeRemainingTilComplete <= 0) {
            timeConsumableTimerComplete = true;
            finishedProcessing = true;
            isProcessing = false;
        }
    }
}
