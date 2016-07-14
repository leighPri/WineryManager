using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Element : MonoBehaviour {

    //holders for this square's location info
    public int myPosition;

    void Start () {
        //squares register themselves to the Grid
        for (int i = 0; i < GameControl.grid.Length; i++) {
            if (GameControl.grid[i] == null) {
                GameControl.grid[i] = this;
                myPosition = i;
                break;
            }
        }
    }

    void Update() {
        //enables and disables colliders based on whether or not the BuildingMenu is active (only on MainGame)
        if (SceneManager.GetActiveScene().buildIndex == 2 && BuildingMenuControl.buildingMenuCtrl.gameObject.activeSelf)
            GetComponent<PolygonCollider2D>().enabled = false;
        else
            GetComponent<PolygonCollider2D>().enabled = true;
    }

    void OnMouseUpAsButton() {
        if (InHandCtrl.isInHand)
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building) {
                BuildingCtrl.placeBuilding(this);
                SaveLoad.Save();
            }
    }
    
}
