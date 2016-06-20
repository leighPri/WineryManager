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

    // Use this for initialization
    void OnLevelWasLoaded () {
        //loops through playerBuilding[] and, if a building is present, places a clone of it on 
        //the matching grid[] location
        for (int x = 0; x < playerBuilding.Length; x++) {
                if (playerBuilding[x] != null) {
                    Debug.Log("thing exists");
                    //playerBuilding[x, y] = Instantiate(playerBuilding[x,y], GameControl.grid[x,y].transform.position, Quaternion.identity) as Building;
                }
        }
    }

    public static void placeBuilding(Element cell) {
        playerBuilding[cell.myPosition] = Instantiate(InHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
        playerBuilding[cell.myPosition].transform.parent = BuildingHolder.buildingHolder.gameObject.transform;
        InHandCtrl.ClearHand();
        Debug.Log(playerBuilding[cell.myPosition]);
    }
}
