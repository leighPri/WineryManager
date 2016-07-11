using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour {

    public static Grid grid;

    void Awake() {
        if (grid == null) {
            grid = this;
            DontDestroyOnLoad(this);
        } else if (grid != this)
            Destroy(gameObject);
    }
}

