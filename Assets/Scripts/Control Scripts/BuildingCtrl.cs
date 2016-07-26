using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BuildingCtrl : MonoBehaviour {

    public static BuildingCtrl buildingCtrl;

    public static Building prevBuild;

    //holder of current buildings
    public static Building[] playerBuilding = new Building[GameControl.w * GameControl.h];

    void Awake() {
        if (buildingCtrl == null)
            buildingCtrl = this;
        else if (buildingCtrl != this)
            Destroy(gameObject);
    }
    
    //hides buildings when not on the loading screen (needed for initialization) or the Main Game (because duh)
    void OnLevelWasLoaded (int level) {
        if (level == 1 || level == 2)
            BuildingHolder.buildingHolder.gameObject.SetActive(true);
        else
            BuildingHolder.buildingHolder.gameObject.SetActive(false);
    }

    public static void PlaceBuilding(Element cell) {
           playerBuilding[cell.myPosition] = Instantiate(InHandCtrl.inHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
           playerBuilding[cell.myPosition].SetParamsByID(InHandCtrl.objectInHand); //populates details of above building instance
           playerBuilding[cell.myPosition].transform.SetParent(BuildingHolder.buildingHolder.gameObject.transform, false);
           InHandCtrl.ClearHand();
           SaveLoad.Save();
    }

    public void TryToPlaceBuilding(Element cell) {
        if (playerBuilding[cell.myPosition] == null && InHandCtrl.isInHand && InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building) { //only place a building if there is not one already there and if a building is in hand
            //must have Element in a list so that it can be passed to the master Wait function
            List<object> tempCell = new List<object>();
            tempCell.Add(cell);
            ConfirmationPanel.confirmPanel.ShowAndWait("Are you sure you want to place " + ObjectMaster.buildingList[InHandCtrl.objectInHand].objectName + " here?", this, "PlaceBuilding", tempCell);
        }
    }

    public static bool CanClearPrev(Building nextBuild) {
        //if previousBuilding and displayedBuilding are not null
            //commented out temporarily since displayedBuilding will no longer be a large part of the logic
        //if the upcoming building is not the exact same instance as the one currently stored in previousBuilding
        //if previous building is finished processing
        //if the next building is currently processing (i.e., has been passed a consumable but has not processed it)
        if (prevBuild != null && /*displayedBuilding != null &&*/ prevBuild != nextBuild && prevBuild.finishedProcessing && nextBuild.isProcessing)
            return true;
        else
            return false;
    }
}
