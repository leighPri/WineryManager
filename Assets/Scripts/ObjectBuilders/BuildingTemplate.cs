using UnityEngine;
using System.Collections;

public class BuildingTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public string objectType;
    public int cost;
    public int canProcess;
    public int parentList = (int)ObjectMaster.listType.Building;

    //filled when saved
    public bool isProcessing;
    public bool finishedProcessing;
    public bool hasSelectedOutput;
    public Vector3 myPos;
    public int consumableIDInProcessing;

    public BuildingTemplate(int newID, int newCost, string newObjectName, string newDescription, string newObjectType) {
        id = newID;
        cost = newCost;
        objectName = newObjectName;
        description = newDescription;
        objectType = newObjectType;
    }

    //to be used for building versions of BuildingTemplate for saving/loading
    public BuildingTemplate(int newID, int newCost, string newObjectName, string newDescription, string newObjectType, 
                            bool newIsProcessing, bool newFinishedProcessing, bool newHasSelectedOutput, Vector3 newMyPos, int newConsumableIDInProcessing) {
        id = newID;
        cost = newCost;
        objectName = newObjectName;
        description = newDescription;
        objectType = newObjectType;

        isProcessing = newIsProcessing;
        finishedProcessing = newFinishedProcessing;
        hasSelectedOutput = newHasSelectedOutput;
        myPos = newMyPos;
        consumableIDInProcessing = newConsumableIDInProcessing;
    }

    public void SellBackBuilding() {
        MoneyCtrl.AddMoney(cost);
    }
}
