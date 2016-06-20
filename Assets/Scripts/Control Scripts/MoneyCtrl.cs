using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This class handles the static money object and its display behaviors
//this script should be placed on a GameObject with a Text component
public class MoneyCtrl : MonoBehaviour {
    
    public static MoneyCtrl moneyCtrl;

    //the main money object, this holds current amount of money for the player
    public static int moneyOnHand;

    public GameObject moneyDisplay;
    Text moneyDisplayText;

    void Awake() {
        if (moneyCtrl == null) {
            //commented out because the Controls parent object should persist this
            //DontDestroyOnLoad(gameObject);
            moneyCtrl = this;
            //starting money for player, change or remove this line later for a better solution
            moneyOnHand = 5000;
        }
        else if (moneyCtrl != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        //gets Text component so that it can be modified in Update()
        moneyDisplayText = moneyDisplay.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //displays money available
        moneyDisplayText.text = moneyOnHand.ToString() + "g";
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

    public static void SubtractMoney(int money)
    {
        moneyOnHand -= money;
    }
}
