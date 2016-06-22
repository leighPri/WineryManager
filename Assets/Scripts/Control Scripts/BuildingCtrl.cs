using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BuildingCtrl : MonoBehaviour {

    public static BuildingCtrl buildingCtrl;

    //holder of current buildings
    public static Building[] playerBuilding = new Building[GameControl.w * GameControl.h];

    void Awake() {
        if (buildingCtrl == null) {
            //DontDestroyOnLoad(gameObject);
            buildingCtrl = this;
        }
        else if (buildingCtrl != this)
            Destroy(gameObject);
    }
    
    void OnLevelWasLoaded (int level) {
        if (level == 1) { 
            //is supposed to instantiate buildings...currently does not
            for (int x = 0; x < playerBuilding.Length; x++) {
                    if (playerBuilding[x] != null) {
                        Debug.Log(playerBuilding[x].objectName + " exists at index location " + x);
                        //playerBuilding[x, y] = Instantiate(playerBuilding[x,y], GameControl.grid[x,y].transform.position, Quaternion.identity) as Building;
                    }
            }
            //shows and hides buildingHolder as having it on its own script doesn't work
            //note that this only works because BuildingControl is being passed an instance of
            //the BuildingHolder, not the BuildingHolder prefab itself
            BuildingHolder.buildingHolder.gameObject.SetActive(true);
        }
        else {
            BuildingHolder.buildingHolder.gameObject.SetActive(false);
        }
    }

    public static void placeBuilding(Element cell) {
        playerBuilding[cell.myPosition] = Instantiate(InHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
        playerBuilding[cell.myPosition].transform.SetParent(BuildingHolder.buildingHolder.gameObject.transform, false);
        InHandCtrl.ClearHand();
        //Debug.Log(playerBuilding[cell.myPosition]);
    }

    public static void HideBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(false);
    }

    public static void ShowBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
    }
}
