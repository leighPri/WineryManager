using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Grape : MonoBehaviour {

    public string objectName;
    public string description;
    public int cost;

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
