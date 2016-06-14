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
        else
            Debug.Log("Cannot change scenes while " + InHandCtrl.buildingInHand.name + " is in hand.");
    }

}
