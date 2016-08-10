using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    
    bool originalPosIsSet;
    public static bool canMoveLeft; //static so that they all either move or don't move
    public static bool canMoveRight; //static so that they all either move or don't move
    public static bool canMoveUp; //static so that they all either move or don't move
    public static bool canMoveDown; //static so that they all either move or don't move
    int moveSpeed = 2;

    void Awake() {
        canMoveDown = true;
        canMoveLeft = true;
        canMoveUp = true;
        canMoveRight = true;
    }

    // Update is called once per frame
    void Update () {
        if (!UIControl.panelIsActive) {
            //should enable WASD and arrow key movement on any object that has this script attached
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && canMoveLeft)
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed); //move left
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && canMoveRight)
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed); //move right
            else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && canMoveUp)
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed); //move up
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && canMoveDown)
                transform.Translate(Vector3.down * Time.deltaTime * moveSpeed); //move down
        }
    }

    public void SnapBackToOrigin(Vector3 posToMoveTo) {
        gameObject.transform.localPosition = posToMoveTo;
    }


}
