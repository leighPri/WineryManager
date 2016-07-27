﻿using UnityEngine;
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

    public void ShowPanel(GameObject panel) {
        if (!InHandCtrl.isInHand) {
            panel.gameObject.SetActive(true);
            panelIsActive = true;
            //BuildingHolder.HideBuildingHolder(true);
        }
    }

    public void HidePanel(GameObject panel) {
        panel.gameObject.SetActive(false);
        panelIsActive = false;
        //BuildingHolder.HideBuildingHolder(false);
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
