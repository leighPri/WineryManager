using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyDisplayLabel : MonoBehaviour {

    Text moneyDisplayText;

    // Use this for initialization
    void Start() {
        //gets Text component so that it can be modified in Update()
        moneyDisplayText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        //displays money available
        moneyDisplayText.text = MoneyCtrl.moneyOnHand.ToString() + "g";
    }
}
