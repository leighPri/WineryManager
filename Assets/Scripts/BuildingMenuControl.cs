using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingMenuControl : MonoBehaviour {

    public static BuildingMenuControl buildingMenuCtrl;

    public static Building displayedBuilding;

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
        if (displayedBuilding.objectType != "aging") {
            InHandCtrl.PutConsumableInHand(displayedBuilding.consumableInProcessing.midpointOutput);
        } else {
            int wineID = displayedBuilding.consumableInProcessing.WineSelect(selectedOutput).id;
            WineMaster.AddBottles(wineID);
            Debug.Log("You now have " + WineMaster.winesOnHand[wineID].bottlesOnHand + " bottles of " + WineMaster.winesOnHand[wineID].wineName + " available to sell.");
        }
        displayedBuilding.EmptyBuilding();
        gameObject.SetActive(false);
    }

    //lets the aging buttons set their desired outputs
    public void SetOutput(int output) {
        selectedOutput = output;
    }

    public static void GetBuilding(Building building)  {
        displayedBuilding = building;
    }
}
