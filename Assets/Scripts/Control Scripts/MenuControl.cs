using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuControl : MonoBehaviour {

    public static MenuControl menuCtrl;

    public Building buildingGeneric;
    public ObjectMaster objMaster;
    public GameObject exampleMenuObject;
    public GameObject[] menuObjects;

    private bool onSellMenu;

    void OnLevelWasLoaded(int level) {
        if (level == 3)
            FillMenuWithBuildings();
        else if (level == 4)
            FillMenuWithGrapes();
        else if (level == 6)
            SellMenu();
    }

    void Update() {
    }

    private void FillMenuWithBuildings() {
        menuObjects = new GameObject[ObjectMaster.buildingList.Count];
        Vector3 initialPos = new Vector3(-171,143,0);
        float yOffset = 29.4f;
        for (int i = 0; i < ObjectMaster.buildingList.Count; i++) {
            int tempID = ObjectMaster.buildingList[i].id;
            menuObjects[i] = Instantiate(exampleMenuObject, initialPos, Quaternion.identity) as GameObject; 
            menuObjects[i].transform.SetParent(gameObject.transform, false);
            //displays name of building
            menuObjects[i].GetComponent<Text>().text = ObjectMaster.buildingList[i].objectName;
            //displays cost of building in child of object
            menuObjects[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ObjectMaster.buildingList[i].cost.ToString() + "g";
            //enables button functionality
            menuObjects[i].gameObject.AddComponent<Button>().onClick.AddListener(delegate() { buildingGeneric.BuyBuilding(tempID); });
            //menuObjects[i].GetComponent<Button>().onClick.AddListener(delegate { Building.staticBuilding.BuyBuilding(ObjectMaster.buildingList[i].id); });
            //stores initialPos over again so that the offsets are maintained
            initialPos = new Vector3(initialPos.x, //x
                                    initialPos.y - yOffset, //y, with an offset
                                     initialPos.z); //z
        }
    }

    private void FillMenuWithGrapes() {
        menuObjects = new GameObject[ObjectMaster.consumableList.Count];
        Vector3 initialPos = new Vector3(-171, 143, 0);
        float yOffset = 29.4f;
        for (int i = 0; i < ObjectMaster.consumableList.Count; i++) {
            int tempID = ObjectMaster.consumableList[i].id;
            menuObjects[i] = Instantiate(exampleMenuObject, initialPos, Quaternion.identity) as GameObject;
            menuObjects[i].transform.SetParent(gameObject.transform, false);
            //displays name of building
            menuObjects[i].GetComponent<Text>().text = ObjectMaster.consumableList[i].objectName;
            //displays cost of building in child of object
            menuObjects[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ObjectMaster.consumableList[i].cost.ToString() + "g";
            //enables button functionality
            menuObjects[i].gameObject.AddComponent<Button>().onClick.AddListener(delegate () { objMaster.BuyGrape(tempID); });
            //menuObjects[i].GetComponent<Button>().onClick.AddListener(delegate { Building.staticBuilding.BuyBuilding(ObjectMaster.buildingList[i].id); });
            //stores initialPos over again so that the offsets are maintained
            initialPos = new Vector3(initialPos.x, //x
                                    initialPos.y - yOffset, //y, with an offset
                                     initialPos.z); //z
        }
    }

    private void SellMenu() {
        onSellMenu = true;
        menuObjects = new GameObject[ObjectMaster.wineList.Count];
        Vector3 initialPos = new Vector3(-171, 143, 0);
        float yOffset = 29.4f;
        for (int i = 0; i < ObjectMaster.wineList.Count; i++) {
            if (ObjectMaster.wineList[i].bottlesOnHand > 0) { //only populates list if there are bottles available to sell
                int tempID = ObjectMaster.consumableList[i].id;
                menuObjects[i] = Instantiate(exampleMenuObject, initialPos, Quaternion.identity) as GameObject;
                menuObjects[i].transform.SetParent(gameObject.transform, false);
                //displays name of building
                menuObjects[i].GetComponent<Text>().text = ObjectMaster.wineList[i].wineName;
                //displays number of bottles available
                menuObjects[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ObjectMaster.wineList[i].bottlesOnHand.ToString();
                //enables button functionality
                menuObjects[i].gameObject.AddComponent<Button>().onClick.AddListener(delegate () { objMaster.SellBottles(tempID); });
                //menuObjects[i].GetComponent<Button>().onClick.AddListener(delegate { Building.staticBuilding.BuyBuilding(ObjectMaster.buildingList[i].id); });
                //stores initialPos over again so that the offsets are maintained
                initialPos = new Vector3(initialPos.x, //x
                                        initialPos.y - yOffset, //y, with an offset
                                         initialPos.z); //z
            }
        }
    }

    private void UpdateBottlesOnHand() {
        for (int i = 0; i < ObjectMaster.wineList.Count; i++) {
            if (menuObjects[i] != null)
                menuObjects[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = ObjectMaster.wineList[i].bottlesOnHand.ToString();
        }
    }

}
