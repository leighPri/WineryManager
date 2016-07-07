﻿using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class JSONParse : MonoBehaviour {

    //holder objects for each piece of parsed JSON
    JSONNode consumableJSON;
    JSONNode buildingJSON;
    JSONNode midpointJSON;
    JSONNode unagedJSON;
    JSONNode wineJSON;

    // Use this for initialization
    void Awake () {
        consumableJSON = JSON.Parse((Resources.Load("consumableJSON") as TextAsset).text); //loads the text of the JSON file as a TextAsset and passes it to the Parser to initialize a JSONNode object
        ConsumableCreator(consumableJSON["Consumable"], ObjectMaster.consumableList);

        buildingJSON = JSON.Parse((Resources.Load("buildingJSON") as TextAsset).text); //see above
        BuildingCreator(buildingJSON["Building"], ObjectMaster.buildingList);

        midpointJSON = JSON.Parse((Resources.Load("midpointJSON") as TextAsset).text); //see above
        MidpointCreator(midpointJSON["Midpoint"], ObjectMaster.midpointList);

        unagedJSON = JSON.Parse((Resources.Load("unagedJSON") as TextAsset).text); //see above
        UnagedCreator(unagedJSON["Unaged"], ObjectMaster.unagedList);

        wineJSON = JSON.Parse((Resources.Load("wineJSON") as TextAsset).text); //see above
        WineCreator(wineJSON["Wine"], ObjectMaster.wineList);
    }

    //each of these fills the lists found in ObjectMaster.cs with their appropriate data from the parsed JSON objects

    public void ConsumableCreator(JSONNode jsonObj, List<ConsumableTemplate> consList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            ConsumableTemplate tempCons;
            tempCons = new ConsumableTemplate(int.Parse(jsonObj[i]["id"]),
                                             int.Parse(jsonObj[i]["cost"]),
                                             int.Parse(jsonObj[i]["outputID"]),
                                             jsonObj[i]["objectName"].Value,
                                             jsonObj[i]["description"].Value);
            consList.Add(tempCons);
        }
    }

    public void MidpointCreator(JSONNode jsonObj, List<MidpointTemplate> midsList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            MidpointTemplate tempMids;
            tempMids = new MidpointTemplate(int.Parse(jsonObj[i]["id"]),
                                            int.Parse(jsonObj[i]["outputID"]),
                                            jsonObj[i]["objectName"].Value,
                                            jsonObj[i]["description"].Value);
            midsList.Add(tempMids);
        }
    }

    public void UnagedCreator(JSONNode jsonObj, List<UnagedTemplate> unagedList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            UnagedTemplate tempUnaged;
            //needs to step through the array so that the values are set properly and not just set up as references
            int[] tempArray = new int[jsonObj[i]["outputID"].Count];
            for (int j = 0; j < jsonObj[i]["outputID"].Count; j++) {
                tempArray[j] = int.Parse(jsonObj[i]["outputID"][j].Value);
            }
            tempUnaged = new UnagedTemplate(int.Parse(jsonObj[i]["id"]),
                                            tempArray,
                                            jsonObj[i]["objectName"].Value,
                                            jsonObj[i]["description"].Value);
            unagedList.Add(tempUnaged);
        }
    }

    public void BuildingCreator(JSONNode jsonObj, List<BuildingTemplate> buildList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            BuildingTemplate tempBuild;
            tempBuild = new BuildingTemplate(int.Parse(jsonObj[i]["id"].Value),
                                             int.Parse(jsonObj[i]["cost"].Value),
                                             jsonObj[i]["objectName"].Value,
                                             jsonObj[i]["description"].Value,
                                             jsonObj[i]["objectType"].Value);
                buildList.Add(tempBuild);
        }
    }

    public void WineCreator(JSONNode jsonObj, List<WineTemplate> wineList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            WineTemplate tempWine;
            tempWine = new WineTemplate(int.Parse(jsonObj[i]["id"]),
                                        int.Parse(jsonObj[i]["baseSellValue"]),
                                        jsonObj[i]["wineName"].Value);
            wineList.Add(tempWine);
        }
    }


}
