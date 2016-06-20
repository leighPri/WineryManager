using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    //allows other classes to reference individual GameControl object and lets it make references to itself
    public static GameControl control;

    //declare variables to be persisted here
    public static int w = 13; //grid width
    public static int h = 7; //grid height
    //holder of the actual grid data
    public static Element[] grid = new Element[w * h];

    void Awake()
    {

        if (control == null) //if there is no GameControl object
        {
            //commented out because the Controls parent object should persist this
            //DontDestroyOnLoad(gameObject);
            control = this;
            //this instance becomes THE GameControl object that will persist through all scenes
        }
        else if (control != this) //Destroys any instances that are not THE GameControl
        {
            Destroy(gameObject);
        }
    }
    
}