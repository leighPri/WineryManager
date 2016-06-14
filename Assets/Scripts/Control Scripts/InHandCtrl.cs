using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//controls storage and display of objects in hand
public class InHandCtrl : MonoBehaviour {

    public static InHandCtrl inHandCtrl;

    //public GameObject inHand;
    Text inHandText;
    
    //variables used to hold onto the "inhand" operators
    public static bool isInHand;
    public static Building buildingInHand;

    void Awake() {
        if (inHandCtrl == null) {
            DontDestroyOnLoad(gameObject);
            inHandCtrl = this;
        } else if (inHandCtrl != this)
            Destroy(gameObject);
    }

    void Start() {
        inHandText = GetComponent<Text>();
    }

    void Update() {
        if (buildingInHand != null)
            inHandText.text = "In Hand: " + buildingInHand.name;
        else
            inHandText.text = "In Hand: ";
    }
    
    public static void clearHand() {
        buildingInHand = null;
        isInHand = false;
    }
}
