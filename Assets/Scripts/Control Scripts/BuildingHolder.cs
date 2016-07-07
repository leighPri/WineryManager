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

    public static void HideBuildingHolder(bool toHide) {
        if (toHide)
            buildingHolder.gameObject.SetActive(false);
        else
            buildingHolder.gameObject.SetActive(true);
    }
    
}
