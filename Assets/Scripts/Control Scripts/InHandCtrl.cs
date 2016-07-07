using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//controls storage and display of objects in hand
public class InHandCtrl : MonoBehaviour {

    public static InHandCtrl inHandCtrl;
    
    //variables used to hold onto the "inhand" operators
    public static bool isInHand;
    public static int typeOfObject; //stores the type of object that is in hand, used for conditionals
    public static int objectInHand; //value should be the ID of the specific object, typeOfObject should handle the list selection

    //the actual types of things that can be stored in hand
    public Building buildingInHand;
    //public Consumable consumableInHand;
    
    void Awake() {
        if (inHandCtrl == null) {
            //DontDestroyOnLoad(gameObject);
            inHandCtrl = this;
        } else if (inHandCtrl != this)
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
        //Debug.Log("My enum type is " + typeOfObject);
        objectInHand = ID;
    }

    public static void PutMidpointInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Midpoint;
        //Debug.Log("My enum type is " + typeOfObject);
        objectInHand = ID;
    }

    public static void PutUnagedWineInHand(int ID) {
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Unaged;
        //Debug.Log("My enum type is " + typeOfObject);
        objectInHand = ID;
    }

    //public static void PutBuildingInHand(Building building) {
    //    //buildingInHand = building;
    //    isInHand = true;
    //    typeOfObject = (int)ObjectMaster.listType.Building;
    //}

    //public static void PutConsumableInHand(Consumable consumable) {
    //    //consumableInHand = consumable;
    //    isInHand = true;
    //    typeOfObject = (int)ObjectMaster.listType.Consumable;
    //}

    //does not actually clear the referenced object but sets the conditional checks back to reset so that
    //the referenced object may be overwritten
    public static void ClearHand() {
        isInHand = false;
    }

    public void CancelInHand() {
        if (typeOfObject == (int)ObjectMaster.listType.Consumable) {
            ObjectMaster.consumableList[objectInHand].SellBackConsumable();
        } else if (typeOfObject == (int)ObjectMaster.listType.Building)
            ObjectMaster.buildingList[objectInHand].SellBackBuilding();
        ClearHand();
    }
}
