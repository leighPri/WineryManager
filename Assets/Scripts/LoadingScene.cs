using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	void Awake() {
        //Loads information if there is an existing save file
        try {
            SaveLoad.Load();
        } catch (System.Exception e) {
            //SaveLoad.DeleteSave();
            Debug.Log("Error: " + e);
            SceneManager.LoadScene("MainGame");
        }
    }

    void Update() {
        if (SaveLoad.finishedLoading)
            SceneManager.LoadScene("MainGame");
    }
}
