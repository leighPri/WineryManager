using UnityEngine;
using System.Collections;
//these let the class write to a file to persist data outside the instance of the program
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static bool finishedLoading;

    //saves data out to a file, can be called by other objects using GameControl.Save() because class is static and self-referential
    public static void Save() {
        //does the actual reading/writing to file
        BinaryFormatter bf = new BinaryFormatter();
        //Creates actual file that can be written to
        //this DOES WORK on iOS and Android
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        //creates holder for data to be saved
        PlayerData data = new PlayerData();
        //copies data from GameControl using this syntax:
        //data.health = health;
        data.money = MoneyCtrl.moneyOnHand;
        SaveBuildings(data);
        for (int i = 0; i < data.bottleSaver.Length; i++) {
            data.bottleSaver[i] = ObjectMaster.wineList[i];
        }


        //takes the Serializable class, data, and writes it out to file
        bf.Serialize(file, data);
        Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
        //stops editing playerInfo.dat
        file.Close();
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
            //BuildingCtrl.playerBuilding = data.playerBuildings;
            LoadBuildings(data, data.playerBuildings);
            for (int i = 0; i < data.bottleSaver.Length; i++) {
                if (data.bottleSaver[i].bottlesOnHand != 0)
                    ObjectMaster.wineList[i].bottlesOnHand = data.bottleSaver[i].bottlesOnHand;
            }
        }
        finishedLoading = true;
    }

    public static void LoadBuildings(PlayerData data, BuildingTemplate[] buildTemplateArray) {
        for (int i = 0; i < data.playerBuildings.Length; i++) {
            if (data.playerBuildings[i] != null) {
                BuildingCtrl.playerBuilding[i] = GameObject.Instantiate(InHandCtrl.inHandCtrl.buildingInHand, data.playerBuildings[i].myPos, Quaternion.identity) as Building;
                BuildingCtrl.playerBuilding[i].SetParamsByID(data.playerBuildings[i].id, buildTemplateArray); //populates details of above building instance
                BuildingCtrl.playerBuilding[i].transform.SetParent(BuildingHolder.buildingHolder.gameObject.transform, false);
            }
        }
    }

    public static void SaveBuildings(PlayerData data) {
        for (int i = 0; i < data.playerBuildings.Length; i++) {
            data.playerBuildings[i] = new BuildingTemplate(BuildingCtrl.playerBuilding[i].id, BuildingCtrl.playerBuilding[i].cost, BuildingCtrl.playerBuilding[i].objectName, BuildingCtrl.playerBuilding[i].description, BuildingCtrl.playerBuilding[i].objectType, BuildingCtrl.playerBuilding[i].isProcessing, BuildingCtrl.playerBuilding[i].finishedProcessing, BuildingCtrl.playerBuilding[i].hasSelectedOutput, BuildingCtrl.playerBuilding[i].myPos, BuildingCtrl.playerBuilding[i].consumableIDInProcessing);
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
