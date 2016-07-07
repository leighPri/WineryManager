using UnityEngine;
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
        //sets elements to the specifics of the passed building
        buildingNameText.text = displayedBuilding.objectName;

        if (displayedBuilding.isProcessing) {
            statusText.text = "Processing: " + DisplayInProcessing();
        } else if (displayedBuilding.finishedProcessing) {
            statusText.text = "Finished processing: " + DisplayFinishedProduct();
        } else
            statusText.text = "Empty"; //default display option
    }

    string DisplayInProcessing() {
        if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable) 
            return ThisConsumable().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint)
            return ThisMidpoint().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged)
            return ThisUnagedWine().objectName;
        else
            return "InProcessing but no consumable found"; //this should never run
    }

    string DisplayFinishedProduct() {
        if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable)
            return ThisMidpoint().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint)
            return ThisUnagedWine().objectName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged)
            return ObjectMaster.wineList[ThisUnagedWine().outputID[selectedOutput]].wineName;
        else
            return "Done Processing but no product found"; //this should never run
    }

    public static void DisplayAgingOptions(bool toDisplay) {
        if (toDisplay)
            buildingMenuCtrl.agingOptions.SetActive(true);
        else
            buildingMenuCtrl.agingOptions.SetActive(false);
    }

    //these are used just to shorten code elsewhere
    public ConsumableTemplate ThisConsumable() {
        return ObjectMaster.consumableList[displayedBuilding.consumableIDInProcessing];
    }

    public MidpointTemplate ThisMidpoint() {
        return ObjectMaster.midpointList[displayedBuilding.consumableIDInProcessing];
    }

    public UnagedTemplate ThisUnagedWine() {
        return ObjectMaster.unagedList[displayedBuilding.consumableIDInProcessing];
    }

    public void FinishButton() {
        //this conditional is to keep aging barns from being able to "finish" without something being selected
        if (displayedBuilding.objectType != "aging" || displayedBuilding.hasSelectedOutput)
            displayedBuilding.FinishedProcessing();
    }

    //places output in hand and hides the building menu
    public void GetOutput() {
        if (displayedBuilding.finishedProcessing) {
            if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Consumable) {
                InHandCtrl.PutMidpointInHand(ThisConsumable().outputID);
            } else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Midpoint) {
                InHandCtrl.PutUnagedWineInHand(ThisMidpoint().outputID);
            } else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Unaged) {
                int wineID = ThisUnagedWine().outputID[selectedOutput];
                ObjectMaster.AddBottles(wineID);
                //WineMaster.AddBottles(wineID);
                Debug.Log("You now have " + ObjectMaster.wineList[wineID].bottlesOnHand + " bottles of " + ObjectMaster.wineList[wineID].wineName + " available to sell.");
                displayedBuilding.EmptyBuilding(); //aging barns clear themselves because there is nowhere else for wines to go except into storage (currently)
            }
            //if (displayedBuilding.objectType != "aging") {
            //    InHandCtrl.PutConsumableInHand(displayedBuilding.consumableInProcessing.midpointOutput);
            //} else {
            //    int wineID = displayedBuilding.consumableInProcessing.WineSelect(selectedOutput).id;
            //    WineMaster.wineMaster.AddBottles(wineID);
            //    Debug.Log("You now have " + WineMaster.wineMaster.winesOnHand[wineID].bottlesOnHand + " bottles of " + WineMaster.wineMaster.winesOnHand[wineID].wineName + " available to sell.");
            //    displayedBuilding.EmptyBuilding(); //aging barns clear themselves because there is nowhere else for wines to go except into storage (currently)
            //}
            previousBuilding = displayedBuilding;
            gameObject.SetActive(false);
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

    public static void GetBuilding(Building building) {
        displayedBuilding = building;
    }
}
