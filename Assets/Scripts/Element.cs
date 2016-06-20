using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Element : MonoBehaviour {

    //holders for this square's location info
    public int myPosition;

    //hold to display building name if there is a building on this location
    //public Text buildingName;

    void Start () {
        //buildingName = GetComponent<Text>();
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
        if (InHandCtrl.isInHand) {
            BuildingCtrl.placeBuilding(this);
            //clears hand after placing building
            //InHandCtrl.ClearHand();
        }
    }
}
