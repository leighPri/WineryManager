﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingMenuControl : MonoBehaviour {

    public static BuildingMenuControl buildingMenuCtrl;

    public static Building displayedBuilding;
    public static Building previousBuilding;

    //accepts 0, 1, or 2 used to pick an output from the Consumable output array
    public  int selectedOutput;

    public GameObject buildingName;
    public static Text buildingNameText;
    public GameObject status;
    public static Text statusText;
    public GameObject agingOptions;
    public GameObject getProduct;
    public static Text getProductText;

    public GameObject finishButton;
    public GameObject getProductButton;

    void Awake() {
        if (buildingMenuCtrl == null)
            buildingMenuCtrl = this;
        else if (buildingMenuCtrl != this)
            Destroy(gameObject);
        gameObject.SetActive(false); //sets active by default but allows it to be recovered via script
        finishButton.gameObject.SetActive(false);
        getProductButton.gameObject.SetActive(false);
    }

    void Start() {
        if (buildingNameText == null)
            buildingNameText = buildingName.GetComponent<Text>();
        if (statusText == null)
            statusText = status.GetComponent<Text>();
        if (getProductText == null)
            getProductText = getProduct.GetComponent<Text>();
    }

    void Update() {

        //if (displayedBuilding.isProcessing && displayedBuilding.timeRemainingTilComplete <= 0) {
        //    getProductText.text = "";
        //}

        buildingNameText.text = displayedBuilding.objectName; //sets elements to the specifics of the passed building

        if (displayedBuilding.isProcessing) //shows the details of the object in processing
            statusText.text = "Processing: " + DisplayInProcessing();
        else if (displayedBuilding.finishedProcessing) {//shows the details of the finished product
            statusText.text = "Finished processing: " + DisplayFinishedProduct();
            getProductButton.gameObject.SetActive(true);
        }
        else {
            statusText.text = "Empty"; //default display option
            finishButton.gameObject.SetActive(false);
        }
    }

    //used to choose what to display in Update() based on the building type
    string DisplayInProcessing() {

        finishButton.gameObject.SetActive(true);
        getProductButton.gameObject.SetActive(false);
        finishButton.GetComponentInChildren<Text>().text = displayedBuilding.roundedTimeTilComplete.ToString();
        if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable) 
            return ThisConsumable().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint)
            return ThisMidpoint().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged)
            return ThisUnagedWine().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Vine)
            return ThisVine().objectName;
        else
            return "InProcessing but no consumable found"; //this should never run
    }

    //used to choose what to display in Update() based on the building type
    string DisplayFinishedProduct() {
        finishButton.gameObject.SetActive(false);
        getProductButton.gameObject.SetActive(true);
        finishButton.GetComponentInChildren<Text>().text = "Processing Complete";
        if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable)
            return ThisMidpoint().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint)
            return ThisUnagedWine().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged)
            return ObjectMaster.wineList[ThisUnagedWine().outputID[selectedOutput]].wineName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Vine)
            return ThisConsumable().objectName;
        else
            return "Done Processing but no product found"; //this should never run
    }
    
    //controls the display of the aging buttons
    public static void DisplayAgingOptions(bool toDisplay) {
        if (toDisplay)
            buildingMenuCtrl.agingOptions.SetActive(true);
        else
            buildingMenuCtrl.agingOptions.SetActive(false);
    }

    //these are just used to shorten code elsewhere
    public ConsumableTemplate ThisConsumable() {
        return ObjectMaster.consumableList[displayedBuilding.consumableIDInProcessing];
    }

    public MidpointTemplate ThisMidpoint() {
        return ObjectMaster.midpointList[displayedBuilding.consumableIDInProcessing];
    }

    public UnagedTemplate ThisUnagedWine() {
        return ObjectMaster.unagedList[displayedBuilding.consumableIDInProcessing];
    }

    public VineTemplate ThisVine()
    {
        return ObjectMaster.vineList[displayedBuilding.consumableIDInProcessing];
    }

    public void FinishButton() {
        //for testing only, take out later
        if (!displayedBuilding.timeConsumableTimerComplete) {
            displayedBuilding.timeRemainingTilComplete = 0;
            displayedBuilding.timeConsumableTimerComplete = true;
        }
        if (displayedBuilding.objectType != "aging" || displayedBuilding.hasSelectedOutput) //this conditional is to keep aging barns from being able to "finish" without an option being selected
            displayedBuilding.FinishedProcessing();
        
    }

    //places output in hand and hides the building menu
    public void GetOutput() {
        if (displayedBuilding.finishedProcessing) {
            if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable) { //if Wine Press
                InHandCtrl.PutMidpointInHand(ThisConsumable().outputID);

            } else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint) { //if Fermentation Shed
                InHandCtrl.PutUnagedWineInHand(ThisMidpoint().outputID);

            } else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged) { //if Aging Barn
                ObjectMaster.AddBottles(ThisUnagedWine().outputID[selectedOutput]); //adds 100 bottles to the proper wine in the wine list
                Debug.Log("You now have " + ObjectMaster.wineList[ThisUnagedWine().outputID[selectedOutput]].bottlesOnHand + " bottles of " + ObjectMaster.wineList[ThisUnagedWine().outputID[selectedOutput]].wineName + " available to sell.");
                displayedBuilding.EmptyBuilding(); //aging barns clear themselves because there is nowhere else for wines to go except into storage (currently)
            }
            else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Vine) { //if vineyard
                InHandCtrl.PutConsumableInHand(ThisConsumable().outputID);
            }
            
            previousBuilding = displayedBuilding;
            gameObject.SetActive(false); //hides the Building Menu
        }
    }

    public static bool CanClearPrev(Building nextBuild) {
        //if previousBuilding and displayedBuilding are not null
        //if the upcoming building is not the exact same instance as the one currently stored in previousBuilding
        //if previous building is finished processing
        //if the next building is currently processing (i.e., has been passed a consumable but has not processed it)
        if (previousBuilding != null && displayedBuilding != null && previousBuilding != nextBuild && previousBuilding.finishedProcessing && nextBuild.isProcessing)
            return true;
        else
            return false;
    }

    //lets the aging buttons set their desired outputs
    //does not function if there is not something currently being processed
    public void SetOutput(int output) {
        if (displayedBuilding.isProcessing) {
            selectedOutput = output;
            displayedBuilding.hasSelectedOutput = true;
            //hides the whole set once a button is clicked as the user should only be able to select one option
            DisplayAgingOptions(false);
        }
    }

    //sets the displayed building
    public static void GetBuilding(Building building) {
        displayedBuilding = building;
    }
}
