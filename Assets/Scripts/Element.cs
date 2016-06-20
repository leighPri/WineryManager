using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Element : MonoBehaviour {

    //holders for this square's location info
    public int myPosition;

    Renderer rend;

    void Start () {
        rend = GetComponent<SpriteRenderer>();
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
        if (InHandCtrl.IsInHand())
            BuildingCtrl.placeBuilding(this);
    }

    public void Hide() {
        rend.enabled = false;
    }

    public void Show() {
        rend.enabled = true;
    }
}
