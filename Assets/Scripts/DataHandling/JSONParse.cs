using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class JSONParse : MonoBehaviour {

    //TextAsset consumableJSON;
    //TextAsset buildingJSONOG;

    JSONNode consumableJSON;
    JSONNode buildingJSON;
    JSONNode midpointJSON;
    JSONNode unagedJSON;
    JSONNode wineJSON;

    // Use this for initialization
    void Awake () {
        //consumableJSON = Resources.Load("exampleJSON") as TextAsset;
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
    
    public void ConsumableCreator(JSONNode jsonObj, List<ConsumableTemplate> consList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            ConsumableTemplate tempCons;
            tempCons = new ConsumableTemplate(int.Parse(jsonObj[i]["id"]),
                                      int.Parse(jsonObj[i]["cost"]),
                                      int.Parse(jsonObj[i]["outputID"]),
                                      jsonObj[i]["objectName"].Value,
                                      jsonObj[i]["description"].Value,
                                      jsonObj[i]["buildingNeeded"].Value);
            consList.Add(tempCons);
            //consList[i].ExampleCall();
        }
    }

    public void MidpointCreator(JSONNode jsonObj, List<MidpointTemplate> midsList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            MidpointTemplate tempMids;
            tempMids = new MidpointTemplate(int.Parse(jsonObj[i]["id"]),
                                      int.Parse(jsonObj[i]["outputID"]),
                                      jsonObj[i]["objectName"].Value,
                                      jsonObj[i]["description"].Value,
                                      jsonObj[i]["buildingNeeded"].Value);
            midsList.Add(tempMids);
            //midsList[i].ExampleCall();
        }
    }

    public void UnagedCreator(JSONNode jsonObj, List<UnagedTemplate> unagedList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            UnagedTemplate tempUnaged;
            int[] tempArray = new int[jsonObj[i]["outputID"].Count];
            for (int j = 0; j < jsonObj[i]["outputID"].Count; j++) {
                tempArray[j] = int.Parse(jsonObj[i]["outputID"][j].Value);
            }
            tempUnaged = new UnagedTemplate(int.Parse(jsonObj[i]["id"]),
                                      tempArray,
                                      jsonObj[i]["objectName"].Value,
                                      jsonObj[i]["description"].Value,
                                      jsonObj[i]["buildingNeeded"].Value);
            unagedList.Add(tempUnaged);
            //unagedList[i].ExampleCall();
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
            //buildList[i].ExampleCall();
        }
    }

    public void WineCreator(JSONNode jsonObj, List<WineTemplate> wineList) {
        for (int i = 0; i < jsonObj.Count; i++) {
            WineTemplate tempWine;
            tempWine = new WineTemplate(int.Parse(jsonObj[i]["id"]),
                                      int.Parse(jsonObj[i]["baseSellValue"]),
                                      jsonObj[i]["wineName"].Value);
            wineList.Add(tempWine);
            //consList[i].ExampleCall();
        }
    }


}
