using UnityEngine;
using System.Collections;

public class MonoBehaviorHelper : MonoBehaviour {

    //this class is used to implement methods that require monobehavior attributes for non-monobehavior classes
    public void DestroyObject(GameObject gameObject) {
        DestroyObject(gameObject);
    }

    //overloaded to work with Consumable class
    public void OnMouseUpAsButton(Consumable consumable) {
        Debug.Log(consumable.objectName);
    }

    //overloaded to work with Building class
    //public void OnMouseUpAsButton(Building building) {
    //    if (!building.isProcessing && InHandCtrl.isInHand) {
    //        building.FillBuilding();
    //        building.ShowBuildingMenu();
    //    } else if (!InHandCtrl.isInHand) {
    //        building.ShowBuildingMenu();
    //    }
    //}
}
