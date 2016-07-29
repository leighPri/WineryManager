using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

    public static UIControl uiControl;

    public GameObject cancelButton;
    public static bool panelIsActive;

    void Start() {
        panelIsActive = false;
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

    public void ManageButton(PanelHider panel) {
        ShowPanel(panel);
    }

    void Update() {
        if (cancelButton != null) { 
            if (InHandCtrl.isInHand)
                cancelButton.SetActive(true);
            else if (!InHandCtrl.isInHand)
                cancelButton.SetActive(false);
        }
    }
}
