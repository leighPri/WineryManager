using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Consumable : MonoBehaviour {

    public string objectName;
    public string description;
    public int cost; //maybe also use for sell price

    //valid inputs are: "grape" "mush" and "unaged"
    //these will be used elsewhere for conditionals
    public string objectType;

    public Consumable[] outputs;

    public void BuyGrape () {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutGrapeInHand(this);
                MoneyCtrl.SubtractMoney(cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + objectName);
    }

    void OnMouseUpAsButton() {
        Debug.Log(objectName);
    }

    public void DestroyBuilding() {
        DestroyObject(this);
    }
}
