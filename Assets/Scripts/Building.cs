using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Building : MonoBehaviour {

    public string buildingName;
    public string description;
    public int cost;

    public void BuyBuilding() {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.IsInHand()) {
                InHandCtrl.buildingInHand = this;
                MoneyCtrl.SubtractMoney(cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + name);
    }

    void OnMouseUpAsButton() {
        Debug.Log(name);
    }
    
    public void DestroyBuilding() {
        DestroyObject(this);
    }
}
