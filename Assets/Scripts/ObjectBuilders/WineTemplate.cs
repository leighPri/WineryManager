using UnityEngine;
using System.Collections;

public class WineTemplate {

    public int id;
    public string wineName;
    public int bottlesOnHand;
    public int baseSellValue;

    public WineTemplate(int newID, int newSellVal, string newObjectName) {
        id = newID;
        wineName = newObjectName;
        baseSellValue = newSellVal;
        bottlesOnHand = 0;
    }
}
