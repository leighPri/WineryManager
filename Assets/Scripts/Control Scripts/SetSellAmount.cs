using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetSellAmount : MonoBehaviour {

    public static SetSellAmount setSellAmt;
    
    public int amtToSell;
    public int wineIDToSell;

    public GameObject confirmTextObj;
    private Text confirmText;
    public GameObject onesObj;
    private Text ones;
    public GameObject tensObj;
    private Text tens;
    public GameObject hundObj;
    private Text hundreds;
    public GameObject thouObj;
    private Text thousands;


    // Use this for initialization
    void Awake () {
        if (setSellAmt == null) {
            setSellAmt = this;
            DontDestroyOnLoad(this);
            amtToSell = 0; //initializes to zero so it's not trying to do calculations on null
        } else if (setSellAmt != this)
            Destroy(gameObject);

        //sets text objects once
        if (confirmText == null)
            confirmText = confirmTextObj.GetComponent<Text>();
        if (ones == null)
            ones = onesObj.GetComponent<Text>();
        if (tens == null)
            tens = tensObj.GetComponent<Text>();
        if (hundreds == null)
            hundreds = hundObj.GetComponent<Text>();
        if (thousands == null)
            thousands = thouObj.GetComponent<Text>();

        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        confirmText.text = ConfirmTextString();
        ones.text = Displayones();
        tens.text = Displaytens();
        hundreds.text = Displayhundreds();
        thousands.text = Displaythousands();
    }

    public void SetWineID(int wineID) {
        wineIDToSell = wineID;
    }

    public string ConfirmTextString() {
        return "How many bottles of " + ObjectMaster.wineList[wineIDToSell].wineName + " would you like to sell?";
    }

    public string Displayones() {
        if (amtToSell <= 0) {
            return "0";
        } else if (amtToSell >= 1 && amtToSell <= 9) {
            return AmtToSellString().Substring(0);
        } else if (amtToSell >= 10 && amtToSell <= 99) {
            return AmtToSellString().Substring(1);
        } else if (amtToSell >= 100 && amtToSell <= 999) {
            return AmtToSellString().Substring(2);
        } else if (amtToSell >= 1000 && amtToSell <= 9999) {
            return AmtToSellString().Substring(3);
        } else
            return "#";
    }

    public string Displaytens() {
        if (amtToSell < 10) {
            return "0";
        }else if (amtToSell >= 10 && amtToSell <= 99) {
            return AmtToSellString().Substring(0);
        } else if (amtToSell >= 100 && amtToSell <= 999) {
            return AmtToSellString().Substring(1);
        } else if (amtToSell >= 1000 && amtToSell <= 9999) {
            return AmtToSellString().Substring(2);
        } else
            return "#";
    }

    public string Displayhundreds() {
        if (amtToSell < 100) {
            return "0";
        } else if (amtToSell >= 100 && amtToSell <= 999) {
            return AmtToSellString().Substring(0);
        } else if (amtToSell >= 1000 && amtToSell <= 9999) {
            return AmtToSellString().Substring(1);
        } else
            return "#";
    }

    public string Displaythousands() {
        if (amtToSell < 1000) {
            return "0";
        } else if (amtToSell >= 1000 && amtToSell <= 9999) {
            return AmtToSellString().Substring(0);
        } else
            return "#";
    }

    public void CancelButton() {
        amtToSell = 0;
        gameObject.SetActive(false);
    }

    //only functions if amtToSell has been set properly
    public void ConfirmButton() {
        if (amtToSell > 0) {
            ObjectMaster.objectMaster.TryToSellBottles(wineIDToSell, amtToSell);
            amtToSell = 0; //resets counter
            setSellAmt.gameObject.SetActive(false);
        }
    }

    public string AmtToSellString() {
        return amtToSell.ToString();
    }

    //rate should be how much each bit increases by. So a button can increase by 1, 10, 100, etc
    public void AmtButtons(int rate) {
        amtToSell += rate;
        //prevents selling more bottles than are actually available
        if (amtToSell >= ObjectMaster.wineList[wineIDToSell].bottlesOnHand)
            amtToSell = ObjectMaster.wineList[wineIDToSell].bottlesOnHand;
        //prevents negative numbers
        if (amtToSell <= 0)
            amtToSell = 0;
    }
}
