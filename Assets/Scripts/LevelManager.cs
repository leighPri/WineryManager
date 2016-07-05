using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public void CallScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    //only allows a scene change if there is nothing currently in hand
    public void CallIfNotInHand(string sceneName) {
        if (!InHandCtrl.isInHand)
            SceneManager.LoadScene(sceneName);
        else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Building) {
            Debug.Log("Cannot change scenes while " + ObjectMaster.buildingList[InHandCtrl.objectInHand].objectName + " is in hand.");
        } else if (InHandCtrl.typeOfObject == (int)ObjectMaster.listType.Consumable) {
            Debug.Log("Cannot change scenes while " + ObjectMaster.consumableList[InHandCtrl.objectInHand].objectName + " is in hand.");
        }
    }

}
