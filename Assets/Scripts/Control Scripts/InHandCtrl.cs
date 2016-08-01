using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//controls storage and display of objects in hand
public class InHandCtrl : MonoBehaviour {

    public static InHandCtrl inHandCtrl;
    
    //variables used to hold onto the "inhand" operators
    public static bool isInHand;
    public static bool justBought; //whether something came directly from a store or a building--true if store, false from building
    public static int typeOfObject; //stores the type of object that is in hand, used for conditionals
    public static int objectInHand; //value should be the ID of the specific object, typeOfObject should handle the list selection

    //used to instantiate buildings that are then filled with the information from the BuildingTemplate
    public Building buildingInHand;
    
    void Awake() {
        if (inHandCtrl == null)
            inHandCtrl = this;
        else if (inHandCtrl != this)
            Destroy(gameObject);
    }

    public static void PutBuildingInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Building;
        objectInHand = ID;
    }

    public static void PutConsumableInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Consumable;
        objectInHand = ID;
    }

    public static void PutVineInHand(int ID)
    {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Vine;
        objectInHand = ID;
    }

    public static void PutMidpointInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Midpoint;
        objectInHand = ID;
    }

    public static void PutUnagedWineInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Unaged;
        objectInHand = ID;
    }

    //does not actually clear the referenced object but sets the conditional checks back to reset so that the referenced object may be overwritten
    public static void ClearHand() {
        isInHand = false;
    }

    public void CancelInHand() {
        if (isInHand) {
            if (justBought) { //sells only if it came from a store, then sets justBought to false. Clears without a sellback otherwise
                //only consumables, vines, and buildings can be canceled and sold back for money, so only those are noted. All others are just cleared (they are not lost due to BuildingMenuControl's CanClearPrev())
                if (typeOfObject == (int)ObjectMaster.listType.Consumable)
                    ObjectMaster.consumableList[objectInHand].SellBackConsumable();
                else if (typeOfObject == (int)ObjectMaster.listType.Vine)
                    ObjectMaster.vineList[objectInHand].SellBackVine();
                else if (typeOfObject == (int)ObjectMaster.listType.Building)
                    ObjectMaster.buildingList[objectInHand].SellBackBuilding();
                justBought = false;
            }
            ClearHand();
        }
    }
}
