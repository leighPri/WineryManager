using UnityEngine;
using System.Collections;

public class ScrollView : MonoBehaviour {

    public bool isSet;
    
	void OnLevelWasLoaded() {
        if (gameObject.transform.childCount == 0)
            isSet = false;
        else
            isSet = true;
	}
	
}
