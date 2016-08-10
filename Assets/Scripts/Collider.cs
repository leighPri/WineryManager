using UnityEngine;
using System.Collections;

public class Collider : MonoBehaviour {

    public bool isTopCollider;
    public bool isLeftCollider;
    public bool isRightCollider;
    public bool isBottomCollider;

    //The background moves with the grid and buildings but does not trigger the OnEnters as it does not have a rigidbody

    //stops all Mover objects at edges, but only in the direction that it's trying to keep moving to
    void OnTriggerEnter2D(Collider2D col) {
        if (isTopCollider) {
            Mover.canMoveUp = false;
        } else if (isLeftCollider) {
            Mover.canMoveLeft = false;
        } else if (isRightCollider) {
            Mover.canMoveRight = false;
        } else if (isBottomCollider) {
            Mover.canMoveDown = false;
        }
    }

    //allows Mover objects to move in the respective direction again
    void OnTriggerExit2D(Collider2D col) {
        if (isTopCollider) {
            Mover.canMoveUp = true;
        } else if (isLeftCollider) {
            Mover.canMoveLeft = true;
        } else if (isRightCollider) {
            Mover.canMoveRight = true;
        } else if (isBottomCollider) {
            Mover.canMoveDown = true;
        }
    }
}
