using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingManageMenu : MonoBehaviour {

    public static BuildingManageMenu buildingManageMenu;

    public static Building displayedBuilding;

    public GameObject buildingName;
    public static Text buildingNameText;

    public GameObject demolishButton;
    public GameObject clearButton;

    void Awake() {
        if (buildingManageMenu == null)
            buildingManageMenu = this;
        else if (buildingManageMenu != this)
            Destroy(gameObject);
        gameObject.SetActive(false); //sets active by default but allows it to be recovered via script
    }

    void Start() {
        demolishButton.GetComponent<Button>().onClick.AddListener(delegate () { TryToDemolishBuilding(); });
        clearButton.GetComponent<Button>().onClick.AddListener(delegate () { TryToClearBuilding(); });
    }

    void Update() {
        if (gameObject.activeSelf)
            UIControl.panelIsActive = true;
    }

    //sets displayedBuilding so this menu can call building stuff too
    public void SetBuilding() {
        displayedBuilding = BuildingMenuControl.displayedBuilding;
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
