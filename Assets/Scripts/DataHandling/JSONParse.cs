using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class JSONParse : MonoBehaviour {

    //TextAsset consumableJSON;
    //TextAsset buildingJSONOG;

    JSONNode consumableJSON;
    JSONNode buildingJSON;

    // Use this for initialization
    void Awake () {
        //consumableJSON = Resources.Load("exampleJSON") as TextAsset;
        //consumableJSON = JSON.Parse((Resources.Load("consumableJSON") as TextAsset).text); //loads the text of the JSON file as a TextAsset and passes it to the Parser to initialize a JSONNode object
        //ConsumableCreator(consumableJSON["Consumable"], ObjectMaster.consumableList);
        buildingJSON = JSON.Parse((Resources.Load("buildingJSON") as TextAsset).text); //see above
        BuildingCreator(buildingJSON["Building"], ObjectMaster.buildingList);
        //Debug.Log(int.Parse(buildingJSON["Building"][1]["id"]).GetType());
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
            //consList[i].id = int.Parse(jsonObj[i]["id"]);
            //consList[i].cost = int.Parse(jsonObj[i]["cost"]);
            //consList[i].outputID = int.Parse(jsonObj[i]["outputID"]);
            //consList[i].objectName = jsonObj[i]["objectName"].Value;
            //consList[i].description = jsonObj[i]["description"].Value;
            //consList[i].buildingNeeded = jsonObj[i]["buildingNeeded"].Value;
            consList[i].ExampleCall();
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
            buildList[i].ExampleCall();
        }
    }

}
