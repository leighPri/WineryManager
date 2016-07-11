using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    void OnMouseUpAsButton() {
        if (InHandCtrl.isInHand)
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building) {
                BuildingCtrl.placeBuilding(this);
                SaveLoad.Save();
            }
    }
    
}
