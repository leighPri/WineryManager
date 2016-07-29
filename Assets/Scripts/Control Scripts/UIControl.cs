using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

    public static UIControl uiControl;

    public GameObject cancelButton;
    public static bool panelIsActive;

    void Start() {
        panelIsActive = false;
    }

    void Update() {
        if (cancelButton != null) {
            if (InHandCtrl.isInHand)
                cancelButton.SetActive(true);
            else if (!InHandCtrl.isInHand)
                cancelButton.SetActive(false);
        }
    }

    public static void ShowPanel(PanelHider panel) {
        if (!InHandCtrl.isInHand) {
            panel.gameObject.SetActive(true);
            panelIsActive = true;
            panel.showPanel = true;
            //BuildingHolder.HideBuildingHolder(true);
        }
    }

    public static void HidePanel(PanelHider panel) {
        panel.gameObject.SetActive(false);
        panelIsActive = false;
        //BuildingHolder.HideBuildingHolder(false);
    }

    public void NonStaticPanelToggle(PanelHider panel) {
        ShowPanel(panel);
    }

    public void NonStaticSave(PanelHider panel) {
        SaveLoad.Save();
        HidePanel(panel);
    }

    public void LoadMainMenu(object holderObject) {
        SceneManager.LoadScene("Start");
    }

    public void TryToReturnToMainMenu() {
        List<object> tempList = new List<object>();
        object tempObject = 1;
        tempList.Add(tempObject);

        string confirmText = "Are you sure you want to return to the main menu?";
        confirmText += " All unsaved progress will be lost.";
        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, this, "LoadMainMenu", tempList);
    }
}
