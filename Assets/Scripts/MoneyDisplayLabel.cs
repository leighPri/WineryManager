using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyDisplayLabel : MonoBehaviour {

    Text moneyDisplayText;
    
    void Start() {
        if (moneyDisplayText == null)
           moneyDisplayText = GetComponent<Text>();
    }
    
    void Update() {
        //displays money available
        moneyDisplayText.text = MoneyCtrl.moneyOnHand.ToString() + "g";
    }
}
