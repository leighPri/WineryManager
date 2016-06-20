using UnityEngine;
using System.Collections;

//this class makes sure that the object it's placed on doesn't destroy on load. And nothing else.
//It is for static container objects that require no behavior of their own.
public class DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
}
