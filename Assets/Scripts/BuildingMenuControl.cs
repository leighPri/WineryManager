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
    Text buildingNameText;
    public GameObject status;
    Text statusText;
    public GameObject agingOptions;

    void Awake() {
        if (buildingMenuCtrl == null) {
            //DontDestroyOnLoad(gameObject);
            buildingMenuCtrl = this;
        } else if (buildingMenuCtrl != this)
            Destroy(gameObject);
        //sets active by default but allows it to be recovered via script
        gameObject.SetActive(false);
    }

    void Start() {
        buildingNameText = buildingName.GetComponent<Text>();
        statusText = status.GetComponent<Text>();
    }

    void Update() {
        //displays option buttons only if the building is an aging barn AND only if the held item has more than one possible output
        if (displayedBuilding.objectType != "aging") {
            agingOptions.SetActive(false);
        } else {
            agingOptions.SetActive(true);
        }

        //default display option
        statusText.text = "Empty";

        //sets elements to the specifics of the passed building
        buildingNameText.text = displayedBuilding.objectName;

        if (displayedBuilding.isProcessing) {
            statusText.text = "Processing: " + displayedBuilding.consumableInProcessing.objectName;
        } else if (displayedBuilding.finishedProcessing) {
            if (displayedBuilding.objectType == "aging") {
                //selects final output and displays the name of it
                statusText.text = "Finished processing: " + displayedBuilding.consumableInProcessing.WineSelect(selectedOutput).wineName;
            } else {
                statusText.text = "Finished processing: " + displayedBuilding.consumableInProcessing.midpointOutput.objectName;
            }
        }
    }

    public void FinishButton() {
        displayedBuilding.FinishedProcessing();
    }

    //places output in hand and hides the building menu
    public void GetOutput() {
        if (displayedBuilding.finishedProcessing) {
            if (displayedBuilding.objectType != "aging") {
                InHandCtrl.PutConsumableInHand(displayedBuilding.consumableInProcessing.midpointOutput);
            } else {
                int wineID = displayedBuilding.consumableInProcessing.WineSelect(selectedOutput).id;
                WineMaster.wineMaster.AddBottles(wineID);
                Debug.Log("You now have " + WineMaster.wineMaster.winesOnHand[wineID].bottlesOnHand + " bottles of " + WineMaster.wineMaster.winesOnHand[wineID].wineName + " available to sell.");
                displayedBuilding.EmptyBuilding(); //aging barns clear themselves because there is nowhere else for wines to go except into storage (currently)
            }
            previousBuilding = displayedBuilding;
            //displayedBuilding.EmptyBuilding();
            gameObject.SetActive(false);
        }
    }

    public static bool CanClearPrev(Building nextBuild) {
        //if previousBuilding and displayedBuilding are not null
        //if the upcoming building is not the exact same instance as the one currently stored in previousBuilding
        //if previous building is finished processing
        //if the next building is currently processing (i.e., has been passed a consumable but has not processed it)
        //if the two consumables are the same (end product for one, starting product for the other)
        if (previousBuilding != null && displayedBuilding != null && previousBuilding != nextBuild && previousBuilding.finishedProcessing && nextBuild.isProcessing && previousBuilding.consumableInProcessing.midpointOutput == nextBuild.consumableInProcessing)
            return true;
        else
            return false;
    }

    //lets the aging buttons set their desired outputs
    public void SetOutput(int output) {
        selectedOutput = output;
    }

    public static void GetBuilding(Building building) {
        displayedBuilding = building;
    }
}
