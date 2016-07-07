using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottleDisplay : MonoBehaviour {
    
    public int wineID;

    Text numBottles; 
    
	void Start () {
        numBottles = GetComponent<Text>();
	}
	
	void Update () {
        numBottles.text = ObjectMaster.wineList[wineID].bottlesOnHand.ToString();
    }
}
