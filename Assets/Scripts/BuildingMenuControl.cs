using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingMenuControl : MonoBehaviour {

    public static BuildingMenuControl buildingMenuCtrl;

    public static Building displayedBuilding;

    //accepts 0, 1, or 2 used to pick an output from the Consumable output array
    public static int selectedOutput;

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
        //displays option buttons only if the building is an aging barn
        if (displayedBuilding.objectType != "aging") {
            agingOptions.SetActive(false);
        } else {
            agingOptions.SetActive(true);
        }
    }

    void Update() {
        //sets elements to the specifics of the passed building
        buildingNameText.text = displayedBuilding.objectName;

        if (displayedBuilding.isProcessing) {
            statusText.text = "Processing: " + displayedBuilding.consumableInProcessing.objectName;
        } else if (displayedBuilding.finishedProcessing) {
            //selects final output and displays the name of it
            displayedBuilding.consumableInProcessing.OutputSelect(selectedOutput);
            statusText.text = "Finished processing: " + displayedBuilding.consumableInProcessing.finalOutput.objectName;
        } else {
            statusText.text = "Empty";
        }
    }

    //lets the aging buttons set their desired outputs
    public static void SetOutput(int output) {
        selectedOutput = output;
    }

    public static void GetBuilding(Building building)  {
        displayedBuilding = building;
    }
}
