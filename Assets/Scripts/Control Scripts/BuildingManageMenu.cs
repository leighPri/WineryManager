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

        demolishButton.GetComponent<Button>().onClick.AddListener(delegate () { DemolishButton(); });
        clearButton.GetComponent<Button>().onClick.AddListener(delegate () { ForceClearButton(); });

        gameObject.SetActive(false); //sets active by default but allows it to be recovered via script
    }

    void Update() {
        if (gameObject.activeSelf)
            UIControl.panelIsActive = true;
    }

    //sets displayedBuilding so this menu can call building stuff too
    public void SetBuilding() {
        displayedBuilding = BuildingMenuControl.displayedBuilding;
    }

    public void DemolishButton() {
        displayedBuilding.TryToDemolishBuilding(gameObject);
    }

    public void ForceClearButton() {
        displayedBuilding.TryToClearBuilding(gameObject);
    }
}
