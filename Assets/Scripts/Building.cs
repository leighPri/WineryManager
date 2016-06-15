using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Building : MonoBehaviour {

    public string buildingName;
    public string description;
    public int cost;

    //keeps track of locations of buildings players have placed
    //public int[,] location = new int[13, 7];

    public void BuyBuilding() {
        if (MoneyCtrl.CanAfford(cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.buildingInHand = this;
                InHandCtrl.isInHand = true;
                MoneyCtrl.SubtractMoney(cost);
                Debug.Log(InHandCtrl.buildingInHand + " " + InHandCtrl.isInHand);
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
