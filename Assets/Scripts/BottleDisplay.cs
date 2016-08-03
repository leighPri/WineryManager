using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottleDisplay : MonoBehaviour {
    
    public int wineID;

    Text numBottles; 
    
	void Start () {
        if (numBottles == null)
            numBottles = GetComponent<Text>();
	}
	
	void Update () {
        numBottles.text = ObjectMaster.wineList[wineID].bottlesOnHand.ToString();
    }
}
