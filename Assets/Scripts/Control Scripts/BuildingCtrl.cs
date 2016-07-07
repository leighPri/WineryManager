using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BuildingCtrl : MonoBehaviour {

    public static BuildingCtrl buildingCtrl;

    //holder of current buildings
    public static Building[] playerBuilding = new Building[GameControl.w * GameControl.h];

    void Awake() {
        if (buildingCtrl == null)
            buildingCtrl = this;
        else if (buildingCtrl != this)
            Destroy(gameObject);
    }
    
    void OnLevelWasLoaded (int level) {
        if (level == 1)
            BuildingHolder.buildingHolder.gameObject.SetActive(true);
        else
            BuildingHolder.buildingHolder.gameObject.SetActive(false);
    }

    public static void placeBuilding(Element cell) {
        playerBuilding[cell.myPosition] = Instantiate(InHandCtrl.inHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
        playerBuilding[cell.myPosition].SetParamsByID(InHandCtrl.objectInHand); //populates details of above building instance
        playerBuilding[cell.myPosition].transform.SetParent(BuildingHolder.buildingHolder.gameObject.transform, false);
        InHandCtrl.ClearHand();
    }
}
