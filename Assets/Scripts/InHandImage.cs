using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InHandImage : MonoBehaviour {

    public static InHandImage inHandImage;

    void Awake() {
        if (!InHandCtrl.isInHand)
            gameObject.SetActive(false);
    }
	
	void Update () {
        if (InHandCtrl.isInHand) {
            if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building)
                GetComponentInChildren<Image>().sprite = InHandCtrl.inHandCtrl.buildingInHand.spriteArray[InHandCtrl.objectInHand];
        }
    }

    void OnMouseUpAsButton() {
        InHandCtrl.inHandCtrl.CancelInHand();
    }
}
