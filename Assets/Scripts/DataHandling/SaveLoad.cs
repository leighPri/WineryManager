using UnityEngine;
using System.Collections;
//these let the class write to a file to persist data outside the instance of the program
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {

    public static SaveLoad saveLoad;

    public static bool finishedLoading = false;

    //saves data out to a file, can be called by other objects using GameControl.Save() because class is static and self-referential
    public static void Save() {
        //does the actual reading/writing to file
        BinaryFormatter bf = new BinaryFormatter();
        //Creates actual file that can be written to
        //this DOES WORK on iOS and Android
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

        //creates holder for data to be saved
        PlayerData data = new PlayerData();
        //copies data using this syntax:
        data.money = MoneyCtrl.moneyOnHand;
        SaveBuildings(data);
        for (int i = 0; i < data.bottleSaver.Length; i++) {
            data.bottleSaver[i] = ObjectMaster.wineList[i];
        }

        //takes the Serializable class, data, and writes it out to file
        bf.Serialize(file, data);
        //stops editing playerInfo.dat
        file.Close();
    }

    //resets all parameters to zero but DOES NOT save over the current save file
    //however, as soon as the player does something that initiates SaveLoad.Save() the old data will be overwritten
    public static void NewGame() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            MoneyCtrl.moneyOnHand = 5000;
            for (int i = 0; i < BuildingCtrl.playerBuilding.Length; i++) {
                if (BuildingCtrl.playerBuilding[i] != null) {
                    BuildingCtrl.playerBuilding[i].DemolishBuilding();
                }
            }
            BuildingCtrl.playerBuilding = new Building[GameControl.w * GameControl.h];
            for (int i = 0; i < ObjectMaster.wineList.Count; i++) {
                ObjectMaster.wineList[i].bottlesOnHand = 0;
            }
        }
    }

    public static void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            //does the actual reading/writing to file
            BinaryFormatter bf = new BinaryFormatter();
            //opens previously created file
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            //reads from file and puts it in a PlayerData instance
            //the parenthesis casts it from a generic object to specifically a PlayerData object
            PlayerData data = (PlayerData)bf.Deserialize(file);
            //have the data in the data class, no longer need file
            file.Close();

            //takes information from PlayerData instance and loads them into the game using this syntax:
            MoneyCtrl.moneyOnHand = data.money;
            LoadBuildings(data, data.playerBuildings);
            for (int i = 0; i < data.bottleSaver.Length; i++) {
                if (data.bottleSaver[i].bottlesOnHand != 0)
                    ObjectMaster.wineList[i].bottlesOnHand = data.bottleSaver[i].bottlesOnHand;
            }
        }
        finishedLoading = true;
    }

    public static void DeleteSave() {
        File.Delete(Application.persistentDataPath + "/playerInfo.dat");
    }

    public static void LoadBuildings(PlayerData data, BuildingTemplate[] buildTemplateArray) {
        for (int i = 0; i < data.playerBuildings.Length; i++) {
            if (data.playerBuildings[i] != null) {
                BuildingCtrl.playerBuilding[i] = GameObject.Instantiate(InHandCtrl.inHandCtrl.buildingInHand, new Vector3(data.playerBuildings[i].myPos[0], data.playerBuildings[i].myPos[1], data.playerBuildings[i].myPos[2]), Quaternion.identity) as Building;
                BuildingCtrl.playerBuilding[i].SetParams(data.playerBuildings[i]); //populates details of above building instance
                BuildingCtrl.playerBuilding[i].transform.SetParent(BuildingHolder.buildingHolder.gameObject.transform, false);
            }
        }
    }

    public static void SaveBuildings(PlayerData data) {
        for (int i = 0; i < BuildingCtrl.playerBuilding.Length; i++) {
            if (BuildingCtrl.playerBuilding[i] != null) {
                BuildingTemplate tempBuildTempl;
                tempBuildTempl = new BuildingTemplate(BuildingCtrl.playerBuilding[i].id, BuildingCtrl.playerBuilding[i].cost, BuildingCtrl.playerBuilding[i].objectName, BuildingCtrl.playerBuilding[i].description, BuildingCtrl.playerBuilding[i].objectType, BuildingCtrl.playerBuilding[i].isProcessing, BuildingCtrl.playerBuilding[i].finishedProcessing, BuildingCtrl.playerBuilding[i].hasSelectedOutput, BuildingCtrl.playerBuilding[i].myPos.x, BuildingCtrl.playerBuilding[i].myPos.y, BuildingCtrl.playerBuilding[i].myPos.z, BuildingCtrl.playerBuilding[i].consumableIDInProcessing);
                data.playerBuildings[i] = tempBuildTempl;
            }
        }
    }
}

//holder that organizes data passed to it from GameControl and Serializes it in order to save it to a file
[Serializable]
public class PlayerData {
    //parameters to save
    public int money;
    public BuildingTemplate[] playerBuildings = new BuildingTemplate[GameControl.w * GameControl.h];
    public WineTemplate[] bottleSaver = new WineTemplate[ObjectMaster.wineList.Count];
}
