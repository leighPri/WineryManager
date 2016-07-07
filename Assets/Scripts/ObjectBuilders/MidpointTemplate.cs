using UnityEngine;
using System.Collections;

public class MidpointTemplate {

    public int id;
    public string objectName; //the name that the player sees
    public string description;

    public int parentList = (int)ObjectMaster.listType.Midpoint;

    public int outputID;

    public MidpointTemplate(int newID, int NewOutputID, string newObjectName, string newDescription) {
        id = newID;
        objectName = newObjectName;
        outputID = NewOutputID;
        description = newDescription;
    }

}
