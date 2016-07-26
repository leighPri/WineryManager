using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingMenuControl : MonoBehaviour {

    public static BuildingMenuControl buildingMenuCtrl;

    public static Building displayedBuilding;

    public GameObject buildingName;
    public static Text buildingNameText;
    public GameObject status;
    public static Text statusText;
    public GameObject agingOptions;
    public GameObject getProduct;
    public static Text getProductText;

    public GameObject finishButton;
    public GameObject getProductButton;
    public GameObject demolishButton;
    public GameObject clearButton;

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

        demolishButton.GetComponent<Button>().onClick.AddListener(delegate () { TryToDemolishBuilding(); });
        clearButton.GetComponent<Button>().onClick.AddListener(delegate () { TryToClearBuilding(); });
    }

    //sets the displayed building
    public static void GetBuilding(Building building) {
        displayedBuilding = building;
    }

    void Update() {
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

        //shows timer in the finish button
        if (displayedBuilding.objectType != "aging" || displayedBuilding.hasSelectedOutput) {
            finishButton.gameObject.SetActive(true);
            finishButton.GetComponentInChildren<Text>().text = displayedBuilding.roundedTimeTilComplete.ToString();
        }

        //hides get product button
        getProductButton.gameObject.SetActive(false);

        //displays the name of whatever the building is processing
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
            return ObjectMaster.wineList[ThisUnagedWine().outputID[displayedBuilding.selectedOutput]].wineName;
        else if (displayedBuilding.canProcess == (int)ObjectMaster.listType.Vine)
            return ThisConsumable().objectName;
        else
            return "Done Processing but no product found"; //this should never run
    }
    
    //controls the display of the aging buttons
    public static void DisplayAgingOptions(bool toDisplay) {
        if (toDisplay)
            buildingMenuCtrl.agingOptions.gameObject.SetActive(true);
        else
            buildingMenuCtrl.agingOptions.gameObject.SetActive(false);
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

    //for testing only, take out later
    public void FinishButton() {
            if (!displayedBuilding.timeConsumableTimerComplete) {
                displayedBuilding.timeRemainingTilComplete = 0;
                displayedBuilding.timeConsumableTimerComplete = true;
            }
        if (displayedBuilding.objectType != "aging" || displayedBuilding.hasSelectedOutput) //this conditional is to keep aging barns from being able to "finish" without an option being selected
            displayedBuilding.FinishedProcessing();
        
    }

    //this is a nested call because it's used to set buttons
    public void GetOutput() {
        displayedBuilding.GetOutput();
    }

    public void TryToSetOutput(int output) {
        List<object> tempList = new List<object>();
        object tempObject = output;
        tempList.Add(tempObject);
        string confirmText = "Are you sure you want to select ";
        if (output == 0)
            confirmText += "stainless steel aging?";
        else if (output == 1)
            confirmText += "oak barrel aging?";
        else if (output == 2)
            confirmText += "bottle aging?";
        confirmText += " You cannot change this choice";
        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, displayedBuilding, "SetOutput", tempList);
    }

    public void TryToDemolishBuilding() {
        List<object> tempList = new List<object>();
        object tempObject = displayedBuilding.gameObject;
        tempList.Add(tempObject);

        string confirmText = "Are you sure you want to demolish this building?";
        confirmText += " This cannot be undone.";
        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, displayedBuilding, "DemolishBuilding", tempList);
    }

    public void TryToClearBuilding() {
        List<object> tempList = new List<object>();
        object tempObject = displayedBuilding.gameObject;
        tempList.Add(tempObject);

        string confirmText = "Are you sure you want to empty this building?";
        confirmText += " This cannot be undone.";
        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, displayedBuilding, "ForceEmptyBuliding", tempList);
    }
}
