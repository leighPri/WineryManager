using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConfirmationPanel : MonoBehaviour {

    public static ConfirmationPanel confirmPanel;

    public static bool returnBool;
    public static bool buttonWasPressed;

    public GameObject confirmText;
    public GameObject cancelButton;
    public GameObject confirmButton;

	void Awake() {
        if (confirmPanel == null) {
            confirmPanel = this;
            DontDestroyOnLoad(this);
        } else if (confirmPanel != this)
            Destroy(gameObject);
    }

    void Start() {
        CancelButton();
        ConfirmButton();
        //hides item but needs to be active first in order to set it active again
        gameObject.SetActive(false);
    }

    public void CancelButton() {
        //hides entire confirmation panel on click
        cancelButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate() { returnBool = false; buttonWasPressed = true; });
    }

    public void ConfirmButton() {
        confirmButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate () { returnBool = true; buttonWasPressed = true; });
    }

    public void ShowPanel(string confirmTextFromMethod) {
        gameObject.SetActive(true);
        buttonWasPressed = false;
        confirmText.gameObject.GetComponent<Text>().text = confirmTextFromMethod;
    }

    public void ShowAndWait(string confirmTextFromMethod, Object myClass, string methodName, List<object> args) {
        ShowPanel(confirmTextFromMethod);
        StartCoroutine(Wait(myClass, methodName, args));
    }

    //TODO: needs to be able to accept string(s) as parameter(s) which are then used to invoke the method named in the first string
    public IEnumerator Wait(Object myClass, string methodName, List<object> args) {
        yield return new WaitUntil(() => buttonWasPressed);
        //place only if returnBool was true after button is pressed
        if (returnBool) {
            InvokeMethod(myClass, methodName, args);
        }
        //hides the panel
        UIControl.panelIsActive = false;
        gameObject.SetActive(false);
    }

    //myClass will probably be invoked as "this" when passing params to ShowAndWait() in other classes
    //Syntax for setting up params for args is:
    //List<object> tempList = new List<object>();
    //tempList.Add(whatever); <-- repeat for more than one param
    //pass tempList for args
    public void InvokeMethod(Object myClass, string methodName, List<object> args) {
        myClass.GetType().GetMethod(methodName).Invoke(myClass, args.ToArray());
    }

}
