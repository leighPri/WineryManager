using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Element : MonoBehaviour {

    //holders for this square's location info
    public int myPosition;
    public Vector3 myOriginalPosition;

    void Start () {
        //squares register themselves to the Grid
        for (int i = 0; i < GameControl.grid.Length; i++) {
            if (GameControl.grid[i] == null) {
                GameControl.grid[i] = this;
                myPosition = i;
                myOriginalPosition = gameObject.transform.position;
                break;
            }
        }
    }

    void Update() {
        //enables and disables colliders based on whether or not panels are active (only on MainGame)
        if (SceneManager.GetActiveScene().buildIndex == 2 && UIControl.panelIsActive)
            GetComponent<PolygonCollider2D>().enabled = false;
        else
            GetComponent<PolygonCollider2D>().enabled = true;
    }

    void OnMouseUpAsButton() {
        if (InHandCtrl.isInHand)
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building) {
                BuildingCtrl.buildingCtrl.TryToPlaceBuilding(this);
                SaveLoad.Save();
            }
    }
    
}
