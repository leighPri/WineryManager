using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public void CallScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    //only allows a scene change if there is nothing currently in hand
    public void CallIfNotInHand(string sceneName) {
        if (!InHandCtrl.isInHand)
            SceneManager.LoadScene(sceneName);
        else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building)
            Debug.Log("Cannot change scenes while " + ObjectMaster.buildingList[InHandCtrl.objectInHand].objectName + " is in hand.");
        else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Consumable)
            Debug.Log("Cannot change scenes while " + ObjectMaster.consumableList[InHandCtrl.objectInHand].objectName + " is in hand.");
    }

    public void StartNewGame(object sceneName) {
        SceneManager.LoadScene((string)sceneName);
    }

    public void TryToStartNewGame(string sceneName) {
        string confirmText = "Are you sure you want to start a new game? You will lose all previous progress.";
        ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, this, "StartNewGame", ConfirmationPanel.confirmPanel.WrapStrings(sceneName));
    }

}
