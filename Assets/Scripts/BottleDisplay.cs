using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottleDisplay : MonoBehaviour {

    //public Wine wine;
    public int wineID;

    Text numBottles; 

	// Use this for initialization
	void Start () {
        numBottles = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        numBottles.text = ObjectMaster.wineList[wineID].bottlesOnHand.ToString();
    }
}
