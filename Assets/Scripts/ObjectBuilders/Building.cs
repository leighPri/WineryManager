using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    public static Building staticBuilding;

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int cost;

    //valid inputs are "press" "ferment" and "aging"
    //used for conditionals, must be correct
    public string objectType;
    public int parentList = (int)ObjectMaster.listType.Building;

    public bool isProcessing;
    public bool finishedProcessing;
    public bool hasSelectedOutput;
    
    public int canProcess; //use the ObjectMaster enum list
    public int consumableIDInProcessing;
    public Vector3 myPos;

    public Sprite[] spriteArray;
    SpriteRenderer spriteRenderer;

    //These values are used for timing
    public float timeConsumableIsPlaced;  //marks time consumable is placed on building
    public float tempTimerValue = 5f;  //this is a placeholder and should be filled with the JSON value of how long a consumable takes to process
    public float timeRemainingTilComplete;
    public bool timeConsumableTimerComplete = false;
    public int roundedTimeTilComplete;

    void Start() {
        SetEnum();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetEnum() {
        if (objectType == "press") {
            canProcess = (int)ObjectMaster.listType.Consumable;
        } else if (objectType == "ferment") {
            canProcess = (int)ObjectMaster.listType.Midpoint;
        } else if (objectType == "aging") {
            canProcess = (int)ObjectMaster.listType.Unaged;
        } else if (objectType == "vineyard") {
            canProcess = (int)ObjectMaster.listType.Vine;
        }
    }
    
    void Update() {
        //enables and disables colliders based on whether or not the BuildingMenu is active (only on MainGame)
        if ((SceneManager.GetActiveScene().buildIndex == 2 && BuildingMenuControl.buildingMenuCtrl.gameObject.activeSelf) || (InHandCtrl.isInHand && InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building))
            GetComponent<BoxCollider2D>().enabled = false;
        else
            GetComponent<BoxCollider2D>().enabled = true;

        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = spriteArray[id]; //enables visual sprite if an instance of this object has been populated
        if (isProcessing && !timeConsumableTimerComplete)
                TimerCheck();
    }

    //used to set an instance of a Building to the details from the relevant template on the BuildingTemplate list
    public void SetParamsByID(int templateID) {
        id = ObjectMaster.buildingList[templateID].id;
        objectName = ObjectMaster.buildingList[templateID].objectName;
        description = ObjectMaster.buildingList[templateID].description;
        cost = ObjectMaster.buildingList[templateID].cost;
        objectType = ObjectMaster.buildingList[templateID].objectType;

        myPos = gameObject.transform.position;
    }

    public void SetParams(BuildingTemplate buildTempl) {
        id = buildTempl.id;
        objectName = buildTempl.objectName;
        description = buildTempl.description;
        cost = buildTempl.cost;
        objectType = buildTempl.objectType;

        //status variables
        isProcessing = buildTempl.isProcessing;
        finishedProcessing = buildTempl.finishedProcessing;
        hasSelectedOutput = buildTempl.hasSelectedOutput;
        consumableIDInProcessing = buildTempl.consumableIDInProcessing;

        myPos = new Vector3(buildTempl.myPos[0], buildTempl.myPos[1], buildTempl.myPos[2]);
    }

    //the int passed in in the inspector should be the object ID from ObjectMaster.buildingList
    public void BuyBuilding (int ID) {
        if (MoneyCtrl.CanAfford(ObjectMaster.buildingList[ID].cost)) {
            if (!InHandCtrl.isInHand) {
                InHandCtrl.PutBuildingInHand(ID);
                MoneyCtrl.SubtractMoney(ObjectMaster.buildingList[ID].cost);
                SceneManager.LoadScene("MainGame");
            }
        } else
            Debug.Log("Not enough cash on hand to buy " + ObjectMaster.buildingList[ID].objectName);
    }

    public void FillBuilding() {
        //fills only if the object in hand is compatible and if this building is not currently processing anything and if it's not holding a finished product waiting to be picked up
        if (canProcess == InHandCtrl.typeOfObject && isProcessing == false && finishedProcessing == false) {
            isProcessing = true;
            finishedProcessing = false;
            timeConsumableIsPlaced = Time.time;
            consumableIDInProcessing = InHandCtrl.objectInHand;
            
            InHandCtrl.ClearHand();
            //clears previously-used building if applicable (see BuildingMenuControl.CanClearPrev() comments for breakdown of conditionals)
            if (BuildingMenuControl.CanClearPrev(this))
                BuildingMenuControl.previousBuilding.EmptyBuilding();
            SaveLoad.Save();
        }
    }
    public void EmptyBuilding() {
        finishedProcessing = false;
        isProcessing = false; //This is redundant on purpose, isProcessing should be set to false by FinishedProcessing()
        hasSelectedOutput = false;

        if(canProcess == (int)ObjectMaster.listType.Vine) {
            isProcessing = true;
            timeConsumableIsPlaced = Time.time;
        }
    }

    public void FinishedProcessing() {
        if (isProcessing) {
            finishedProcessing = true;
            isProcessing = false;
        }
    }

    void OnMouseUpAsButton() {
        if (!isProcessing && !finishedProcessing && InHandCtrl.isInHand)
            FillBuilding();
        ShowBuildingMenu();
    }

    void ShowBuildingMenu() {
        BuildingMenuControl.buildingMenuCtrl.gameObject.SetActive(true);
        if (objectType == "aging" && !hasSelectedOutput)
            BuildingMenuControl.DisplayAgingOptions(true);
        else
            BuildingMenuControl.DisplayAgingOptions(false);
        //passes this building to the menu so that it can be populated 
        BuildingMenuControl.GetBuilding(this);
    }
    

    public void TimerCheck() {
        //gets how much time has elapsed since consumable placed
        float timeCheck = Time.time - timeConsumableIsPlaced;

        timeRemainingTilComplete = tempTimerValue - timeCheck;

        roundedTimeTilComplete = (int)timeRemainingTilComplete;

        if (timeRemainingTilComplete <= 0) {
            timeConsumableTimerComplete = true;
            finishedProcessing = true;
            isProcessing = false;
        }
    }
}
