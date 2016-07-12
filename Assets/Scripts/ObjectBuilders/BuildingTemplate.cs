using UnityEngine;
using System.Collections;

[System.Serializable]
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
    public float[] myPos = new float[3]; //used to store Vector3s in a serializable format. 0 should be x, 1 should be y, and 2 should be z
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
                            bool newIsProcessing, bool newFinishedProcessing, bool newHasSelectedOutput, float newMyPosx, float newMyPosy, float newMyPosz, int newConsumableIDInProcessing) {
        id = newID;
        cost = newCost;
        objectName = newObjectName;
        description = newDescription;
        objectType = newObjectType;

        isProcessing = newIsProcessing;
        finishedProcessing = newFinishedProcessing;
        hasSelectedOutput = newHasSelectedOutput;
        myPos[0] = newMyPosx;
        myPos[1] = newMyPosy;
        myPos[2] = newMyPosz;
        consumableIDInProcessing = newConsumableIDInProcessing;
    }

    public void SellBackBuilding() {
        MoneyCtrl.AddMoney(cost);
    }
}
