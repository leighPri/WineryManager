using UnityEngine;
using System.Collections;

public class ControlHolder : MonoBehaviour {

    public static ControlHolder controlHolder;

    void Awake() {
        if (controlHolder == null) {
            DontDestroyOnLoad(gameObject);
            controlHolder = this;
        }
        else if (controlHolder != this)
        {
            Destroy(gameObject);
        }
    }
    
}
