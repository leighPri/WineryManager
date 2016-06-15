using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BuildingCtrl : MonoBehaviour {

    public static BuildingCtrl buildingCtrl;

    //holder of current buildings
    //public static List<Building> playerBuilding = new List<Building>();
    public static Building[,] playerBuilding = new Building[GameControl.w, GameControl.h];
    //public static Canvas canvas;

    void Awake() {
        if (buildingCtrl == null) {
            DontDestroyOnLoad(gameObject);
            buildingCtrl = this;
        }
        else if (buildingCtrl != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //canvas = FindObjectOfType<Canvas>();
        //loops through playerBuilding[,] and, if a building is present, places a clone of it on 
        //the matching grid[,] location
        for (int x = 0; x < playerBuilding.GetLength(0); x++) {
            for (int y = 0; y < playerBuilding.GetLength(1); y++) {
                if (playerBuilding[x,y] != null) {
                    playerBuilding[x, y] = Instantiate(playerBuilding[x,y], GameControl.grid[x,y].transform.position, Quaternion.identity) as Building;
                }
            }
        }
    }

    public static void placeBuilding(Element cell) {
        //int i = playerBuilding.Count;
        playerBuilding[cell.x, cell.y] = Instantiate(InHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
        //playerBuilding.Add(Instantiate(buildingInHand, cell.transform.position, Quaternion.identity) as Building);
        //sets instance of building to canvas so the text part appears
        //playerBuilding[i].transform.SetParent(canvas.transform);
        Debug.Log(playerBuilding[cell.x, cell.y]);
    }
}
