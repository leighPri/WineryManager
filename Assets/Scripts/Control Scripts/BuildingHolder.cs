using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BuildingHolder : MonoBehaviour {

    public static BuildingHolder buildingHolder;

    void Awake() {
        if (buildingHolder == null)
        {
            DontDestroyOnLoad(gameObject);
            buildingHolder = this;
        }
        else if (buildingHolder != this)
        {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            //hide this object and its children
            gameObject.SetActive(true);
        }
        else
        {
            //reveal object and its children
            gameObject.SetActive(false);
        }
    }
}
