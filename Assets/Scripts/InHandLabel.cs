using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InHandLabel : MonoBehaviour {

    Text inHandText;
	
    void Awake() {
        inHandText = GetComponent<Text>();
    }

    void Start() {
        if (!InHandCtrl.isInHand)
        {
            inHandText.text = "In Hand: ";
        }
    }

    // Update is called once per frame
    void Update () {
        if (InHandCtrl.isInHand) {
            if (InHandCtrl.typeOfObject == "building") {
                inHandText.text = "In Hand: " + InHandCtrl.buildingInHand.objectName;
            }
            else if (InHandCtrl.typeOfObject == "consumable") {
                inHandText.text = "In Hand: " + InHandCtrl.consumableInHand.objectName;
            }
        }  else  {
            inHandText.text = "In Hand: ";
        }
    }
}
