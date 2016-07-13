using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    //allows other classes to reference individual GameControl object and lets it make references to itself
    public static GameControl control;

    //declare variables to be persisted here
    public static int w = 18; //grid width
    //public static int w = 26; //grid width for huge grid
    public static int h = 20; //grid height
    //public static int h = 29; //grid height for huge grid
    //holder of the actual grid data
    public static Element[] grid = new Element[w * h];

    void Awake() {
        if (control == null)
            control = this;
        else if (control != this)
            Destroy(gameObject);
    }

    //hides and shows grid depending on what scene is active. 1 (loading screen) so that buildings can be reinitialized if relevant, 2 (main game) for the actual game
    void OnLevelWasLoaded(int level) {
        if (level == 1 || level == 2) {
            Grid.grid.gameObject.SetActive(true);
        } else {
            Grid.grid.gameObject.SetActive(false);
        }
    }
    
}