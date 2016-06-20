using UnityEngine;
using System.Collections;

//this class makes sure that the object it's placed on doesn't destroy on load. And stays static
//It is for static container objects that require no behavior of their own.
public class DontDestroy : MonoBehaviour {

    public static DontDestroy controlHolder;

    void Awake() {
        if (controlHolder == null) {
            controlHolder = this;
        }
        else if (controlHolder != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
}
