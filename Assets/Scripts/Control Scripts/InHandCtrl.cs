using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//controls storage and display of objects in hand
public class InHandCtrl : MonoBehaviour {

    public static InHandCtrl inHandCtrl;
    
    //variables used to hold onto the "inhand" operators
    public static bool isInHand;
    public static string typeOfObject; //stores the type of object that is in hand, used for conditionals

    //the actual types of things that can be stored in hand
    public static Building buildingInHand;
    public static Consumable consumableInHand;

    void Awake() {
        if (inHandCtrl == null) {
            //commented out because the Controls parent object should persist this
            //DontDestroyOnLoad(gameObject);
            inHandCtrl = this;
        } else if (inHandCtrl != this)
            Destroy(gameObject);
    }

    public static void PutBuildingInHand(Building building) {
        buildingInHand = building;
        isInHand = true;
        typeOfObject = "building";
    }

    public static void PutConsumableInHand(Consumable consumable) {
        consumableInHand = consumable;
        isInHand = true;
        typeOfObject = "consumable";
    }

    //does not actually clear the referenced object but sets the conditional checks back to reset so that
    //the referenced object may be overwritten
    public static void ClearHand() {
        isInHand = false;
        typeOfObject = "";
    }

    public void CancelInHand()
    {
        if (typeOfObject == "consumable")
            consumableInHand.SellBackConsumable();
        else if (typeOfObject == "building")
            buildingInHand.SellBackBuilding();

        ClearHand();

        
    }
}
