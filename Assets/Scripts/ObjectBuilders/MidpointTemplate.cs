using UnityEngine;
using System.Collections;

public class MidpointTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;
    public float timeRequired = 5f; //hard-set here for testing purposes, will eventually be set via JSON

    public int outputID;

    public MidpointTemplate(int newID, int NewOutputID, string newObjectName, string newDescription) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        description = newDescription;
    }

}
