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

    //the actual types of things that can be stored in hand
    public Building buildingInHand;
    public Consumable consumableInHand;

    //value should be the ID of the specific object, typeOfObject should handle the list selection
    public static int objectInHand;

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
        objectInHand = ID;
    }

    //public static void PutBuildingInHand(Building building) {
    //    //buildingInHand = building;
    //    isInHand = true;
    //    typeOfObject = (int)ObjectMaster.listType.Building;
    //}

    public static void PutConsumableInHand(Consumable consumable) {
        //consumableInHand = consumable;
        isInHand = true;
        typeOfObject = (int)ObjectMaster.listType.Consumable;
    }

    //does not actually clear the referenced object but sets the conditional checks back to reset so that
    //the referenced object may be overwritten
    public static void ClearHand() {
        isInHand = false;
    }

    public void CancelInHand()
    {
        if (typeOfObject == (int)ObjectMaster.listType.Consumable)
            consumableInHand.SellBackConsumable();
        else if (typeOfObject == (int)ObjectMaster.listType.Building)
            buildingInHand.SellBackBuilding();

        ClearHand();

        
    }
}
