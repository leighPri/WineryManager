using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//controls storage and display of objects in hand
public class InHandCtrl : MonoBehaviour {

    public static InHandCtrl inHandCtrl;

    //allows the name of the inHand object to be displayed
    public GameObject inHand;
    Text inHandText;
    
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

    void Start() {
        inHandText = inHand.GetComponent<Text>();
        if (!isInHand) {
            inHandText.text = "In Hand: ";
        }
    }

    void Update() {
        if (isInHand) {
            if (typeOfObject == "building") {
                inHandText.text = "In Hand: " + buildingInHand.objectName;
            } else if (typeOfObject == "grape") {
                inHandText.text = "In Hand: " + consumableInHand.objectName;
            }
        } else
            inHandText.text = "In Hand: ";
    }

    public static void PutBuildingInHand(Building building) {
        buildingInHand = building;
        isInHand = true;
        typeOfObject = "building";
    }

    public static void PutGrapeInHand(Consumable grape) {
        consumableInHand = grape;
        isInHand = true;
        typeOfObject = "grape";
    }

    //does not actually clear the referenced object but sets the conditional checks back to reset so that
    //the referenced object may be overwritten
    public static void ClearHand() {
        isInHand = false;
        typeOfObject = "";
    }
}
