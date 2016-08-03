using UnityEngine;
using System.Collections;

public class UnagedTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public int parentList = (int)ObjectMaster.listType.Unaged;
    public float timeRequired = 5f; //hard-set here for testing purposes, will eventually be set via JSON

    public int[] outputID; //the IDs of the objects output by this Consumable

    public UnagedTemplate(int newID, int[] NewOutputID, string newObjectName, string newDescription) {
        id = newID;
        objectName = newObjectName;
        outputID = new int[NewOutputID.Length];
        for (int i = 0; i < NewOutputID.Length; i++) {
            outputID[i] = NewOutputID[i];
        }
        description = newDescription;
    }

}
