using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Element : MonoBehaviour {

    //holders for this square's location info
    public int x;
    public int y;

    //hold to display building name if there is a building on this location
    public Text buildingName;

    void Start () {
        buildingName = GetComponent<Text>();
        //squares register themselves to the Grid
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        if (GameControl.grid[x, y] == null) {
            GameControl.grid[x, y] = this;
        } else {
            Debug.Log("reference exists");
        }
    }
    
    void Update() {
        if (GameControl.playerBuilding[x,y] != null) { 
            buildingName.text = GameControl.playerBuilding[x, y].name;
        }
    }

    void OnMouseUpAsButton() {
        Debug.Log(GameControl.grid[x,y].transform.position);
        if (InHandCtrl.isInHand) {
            GameControl.placeBuilding(this);
            //clears hand after placing building
            InHandCtrl.clearHand();
        }
    }
}
