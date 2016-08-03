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
                Debug.Log("Error: " + e);
                SceneManager.LoadScene("MainGame");
            }
        } else {
            SaveLoad.NewGame();
            SceneManager.LoadScene("MainGame");
        }
    }

    void Update() {
        if (SaveLoad.finishedLoading)
            SceneManager.LoadScene("MainGame");
    }
}
