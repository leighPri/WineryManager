using UnityEngine;
using System.Collections;

//This class handles the static money object and its display behaviors
//this script should be placed on a GameObject with a Text component
public class MoneyCtrl : MonoBehaviour {
    
    public static MoneyCtrl moneyCtrl;

    //the main money object, this holds current amount of money for the player
    public static int moneyOnHand;

    void Awake() {
        if (moneyCtrl == null) {
            moneyCtrl = this;
            moneyOnHand = 5000; //starting money for player, change or remove this line later for a better solution
        } else if (moneyCtrl != this)
            Destroy(gameObject);
    }

    public static bool CanAfford(int cost) {
        if (moneyOnHand >= cost)
            return true;
        else
            return false;
    }

    public static void AddMoney(int money) {
        moneyOnHand += money;
    }

    public static void SubtractMoney(int money) {
        moneyOnHand -= money;
    }
}
