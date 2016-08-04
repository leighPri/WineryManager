using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public Vector3 myOriginalPos;
    bool originalPosIsSet;

    void Start() {
        if (!originalPosIsSet) {
            if (gameObject.GetComponent<Element>()) {
                myOriginalPos = gameObject.GetComponent<Element>().myOriginalPosition;
            } else if (gameObject.GetComponent<Building>()) {
                myOriginalPos = gameObject.GetComponent<Building>().myPos;
            }
            originalPosIsSet = true;
        }
    }

    void OnLevelWasLoaded(int level) {
        if (level == 2) {
            gameObject.transform.position = myOriginalPos;
        }
    }

    // Update is called once per frame
    void Update () {
        //should enable WASD and arrow key movement on any object that has this script attached
        //probably with attach to Elements and Buildings
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * Time.deltaTime); //move left
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * Time.deltaTime); //move right
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * Time.deltaTime); //move up
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.down * Time.deltaTime); //move down
    }


}
