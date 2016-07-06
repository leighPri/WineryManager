using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMaster : MonoBehaviour {

    public static List<ConsumableTemplate> consumableList = new List<ConsumableTemplate>();
    public static List<BuildingTemplate> buildingList = new List<BuildingTemplate>();
    public static List<MidpointTemplate> midpointList = new List<MidpointTemplate>();
    public static List<UnagedTemplate> unagedList = new List<UnagedTemplate>();

    public enum listType { Building, Consumable, Midpoint, Unaged };

}
