using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Building : MonoBehaviour {

    public string objectName;
    public string description;
    public int cost;
    
    public static bool isProcessing;
    public static Consumable grapeInProcessing;

    public void BuyBuilding () {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutBuildingInHand(this);
                MoneyCtrl.SubtractMoney(cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + objectName);
    }

    public static void fillPress(Consumable grape)
    {
        isProcessing = true;
        grapeInProcessing = grape;
    }

    public static void emptyBuilding() {
        isProcessing = false;
    }

    void OnMouseUpAsButton() {
        if (isProcessing) {
            if (objectName == "Wine Press") {
                Debug.Log("Currently proccessing " + grapeInProcessing);
            }
        }
        if (InHandCtrl.isInHand) {
            if (InHandCtrl.typeOfObject == "grape" && objectName == "Wine Press") {
                fillPress(InHandCtrl.consumableInHand);
            }
        }
    }

    public void DestroyBuilding() {
        DestroyObject(this);
    }
}
