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

    void Awake() {
        if (control == null)
            control = this;
        else if (control != this)
            Destroy(gameObject);
    }
    
}