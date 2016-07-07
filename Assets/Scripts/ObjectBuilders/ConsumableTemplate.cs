using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConsumableTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;
    
    public int parentList = (int)ObjectMaster.listType.Consumable;

    public int outputID; //the ID of the object output by this Consumable

    public ConsumableTemplate(int newID, int newCost, int NewOutputID, string newObjectName, string newDescription) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        description = newDescription;
        cost = newCost;
    }

    public void SellBackConsumable() {
        MoneyCtrl.AddMoney(cost);
    }

}
