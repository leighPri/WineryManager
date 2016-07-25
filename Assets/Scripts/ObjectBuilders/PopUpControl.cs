using UnityEngine;
using System.Collections;

public class PopUpControl : MonoBehaviour {

    public Building parentBuilding;
    
    public SpriteRenderer spriteRenderer;
    public Sprite[] itemSpriteArray;

    void Awake() {
        //gets the gameObject that will be used to display whatever item is ready in the buliding
        spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
        parentBuilding = gameObject.transform.parent.gameObject.GetComponent<Building>();
        //hides self by default
        gameObject.SetActive(false);
    }

    //should display item sprite in child object, call from building?
    public void DisplayProcessedItem(int itemArrayID) {
        spriteRenderer.sprite = itemSpriteArray[itemArrayID];
    }

    void OnMouseUpAsButton() {
        //need eventually to not have to manually show the building menu...separate out building processing functions from the actual menu object
        parentBuilding.ShowBuildingMenu();
        BuildingMenuControl.buildingMenuCtrl.GetOutput();
    }
}
