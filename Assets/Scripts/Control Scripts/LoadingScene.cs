using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	void Awake() {
        //Loads information if there is an existing save file
        if (GameControl.TryToLoad) {
            try {
                SaveLoad.finishedLoading = false;
                SaveLoad.Load();
            } catch (System.Exception e) {
                //SaveLoad.DeleteSave();
                Debug.Log("Error: " + e);
                SceneManager.LoadScene("MainGame");
            }
        } else {
            SaveLoad.NewGame();
            SceneManager.LoadScene("MainGame");
        }
    }

    //public void TryToStartNewGame(int makeItWork) {
    //    List<object> tempList = new List<object>();
    //    object tempObject = makeItWork;
    //    tempList.Add(tempObject);

    //    string confirmText = "Are you sure you want to start a new game? All previous data will be lost.";
    //    ConfirmationPanel.confirmPanel.ShowAndWait(confirmText, SaveLoad.saveLoad, "NewGame", tempList);
    //}

    void Update() {
        if (SaveLoad.finishedLoading)
            SceneManager.LoadScene("MainGame");
    }
}
