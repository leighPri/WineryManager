using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VineTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public int cost;
    public float timeRequired = 5f; //hard-set here for testing purposes, will eventually be set via JSON

    public int parentList = (int)ObjectMaster.listType.Vine;

    public int outputID; //the ID of the object output by this Consumable

    public VineTemplate(int newID, int newCost, int NewOutputID, string newObjectName) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        cost = newCost;
    }

    public void SellBackVine() {
        MoneyCtrl.AddMoney(cost);
    }

}
