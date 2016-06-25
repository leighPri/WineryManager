using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

    public GameObject cancelButton;

    //public GameObject textHolder;
    //Canvas canvas;
    public Building[] buildingsAvailable;
    public Consumable[] grapesAvailable;

    public void ShowPanel(GameObject panel) {
        panel.gameObject.SetActive(true);
    }

    public void HidePanel(GameObject panel) {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cancelButton != null)
        {
            if (InHandCtrl.isInHand)
            {
                ShowPanel(cancelButton);
            }
            else if (!InHandCtrl.isInHand)
            {
                HidePanel(cancelButton);
            }
        }
    }

    //void Start() {
    //canvas = FindObjectOfType<Canvas>();
    //DisplayAvailableBuildings();
    //}

    //void Update() {

    //}

    //void DisplayAvailableBuildings() {
    //    float yOffset = 0.0f;
    //    int i = 0;
    //    List<GameObject> buildingListUI = new List<GameObject>();
    //    foreach (Building building in buildingsAvailable) {
    //        //captures properties of each object in buildingsAvailable...but also includes all parent properties. Need to find out how to pull out building-specific properties to iterate through
    //        //System.Reflection.PropertyInfo[] props = building.GetType().GetProperties();
    //        //foreach (System.Reflection.PropertyInfo prop in props) {}
    //        //Create new GameObject childed to this Canvas--moneyCounter is used only because it is a GameObject with a Text component
    //        buildingListUI.Add(Instantiate(textHolder, transform.position, Quaternion.identity) as GameObject);
    //        buildingListUI[i].AddComponent<Text>();
    //        //attaches each new GameObject to the UI canvas
    //        buildingListUI[i].transform.SetParent(canvas.transform);
    //        //update the Text component to contain an element of buildingsAvailable[]
    //        buildingListUI[i].GetComponent<Text>().text = building.buildingName.ToString();
    //        i++;

    //        //increment yOffset
    //        yOffset += 35f; //not final increment amount
    //        i++;
    //    }
    //}


    //void OnGUI() {
    //    var yOffset = 0.0f;
    //    foreach (Building gobj in buildingsAvailable) {
    //        GUI.Label(new Rect(10, 10 + yOffset,100,300), gobj.buildingName);
    //        GUI.Label(new Rect(100, 10 + yOffset, 100, 300), gobj.description);
    //        GUI.Label(new Rect(200, 10 + yOffset, 100, 300), gobj.cost + "g");
    //        yOffset += 50f;
    //        //Text myText = new Text();
    //        //myText.text = gobj.buildingName;

    //}
    //}
}
