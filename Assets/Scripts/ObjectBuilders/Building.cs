using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string objectType;

    public bool isProcessing;
    public bool finishedProcessing;
    public Consumable consumableInProcessing;

    public Sprite[] spriteArray;
    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
    }
    void Update() {
        if (spriteRenderer.sprite == null) {
            //enables visual sprite if an instance of this object has been populated
            spriteRenderer.sprite = spriteArray[id];
        }
    }

    //used to set an instance of a Building to the details from the relevant template on the BuildingTemplate list
    public void SetParamsByID(int templateID) {
        id = ObjectMaster.buildingList[templateID].id;
        objectName = ObjectMaster.buildingList[templateID].objectName;
        description = ObjectMaster.buildingList[templateID].description;
        cost = ObjectMaster.buildingList[templateID].cost;
        objectType = ObjectMaster.buildingList[templateID].objectName;
    }

    //the int passed in in the inspector should be the object ID from ObjectMaster.buildingList
    public void BuyBuilding (int ID) {
        if (MoneyCtrl.CanAfford(ObjectMaster.buildingList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                //InHandCtrl.PutBuildingInHand(this);
                InHandCtrl.PutBuildingInHand(ID); //replace the above with this eventually
                MoneyCtrl.SubtractMoney(ObjectMaster.buildingList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    }

    public void SellBackBuilding()
    {
        MoneyCtrl.AddMoney(cost);
    }

    public void FillBuilding() {
            if (InHandCtrl.inHandCtrl.consumableInHand.CanBePlaced(this)) {
                isProcessing = true;
                finishedProcessing = false;
                consumableInProcessing = InHandCtrl.inHandCtrl.consumableInHand;
                InHandCtrl.ClearHand();
            //clears previously-used building if applicable (see BuildingMenuControl.CanClearPrev() comments for breakdown of conditionals)
            if (BuildingMenuControl.CanClearPrev(this))
                BuildingMenuControl.previousBuilding.EmptyBuilding();
            } else {
                Debug.Log("Cannot place " + InHandCtrl.inHandCtrl.consumableInHand.objectName + " in " + objectName + ", object requires a(n) " + InHandCtrl.inHandCtrl.consumableInHand.buildingNeeded + " to be processed.");
            }
    }

    public void EmptyBuilding() {
        finishedProcessing = false;
    }

    public void FinishedProcessing() {
        finishedProcessing = true;
        isProcessing = false;
    }

    void OnMouseUpAsButton() {
        if (!isProcessing && InHandCtrl.isInHand) {
            FillBuilding();
            ShowBuildingMenu();
        } else if (!InHandCtrl.isInHand) {
            ShowBuildingMenu();
        }
    }

    void ShowBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
        if (objectType == "aging")
            BuildingMenuControl.DisplayAgingOptions(true);
        else
            BuildingMenuControl.DisplayAgingOptions(false);
        //passes this building to the menu so that it can be populated 
        BuildingMenuControl.GetBuilding(this);
    }

    public void DestroyObject() {
        DestroyObject(this);
    }
}
