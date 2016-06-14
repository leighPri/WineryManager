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
    public static Element[,] grid = new Element[w, h];

    //holder of current buildings
    //public static List<Building> playerBuilding = new List<Building>();
    public static Building[,] playerBuilding = new Building[w, h];
    public static Canvas canvas;



    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();

        if (control == null) //if there is no GameControl object
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            //this instance becomes THE GameControl object that will persist through all scenes
        }
        else if (control != this) //Destroys any instances that are not THE GameControl
        {
            Destroy(gameObject);
        }
    }

    public static void placeBuilding(Element cell) {
        //int i = playerBuilding.Count;
        playerBuilding[cell.x, cell.y] = Instantiate(InHandCtrl.buildingInHand, cell.transform.position, Quaternion.identity) as Building;
        //playerBuilding.Add(Instantiate(buildingInHand, cell.transform.position, Quaternion.identity) as Building);
        //sets instance of building to canvas so the text part appears
        //playerBuilding[i].transform.SetParent(canvas.transform);
        Debug.Log(playerBuilding[cell.x,cell.y]);
    }
    
}