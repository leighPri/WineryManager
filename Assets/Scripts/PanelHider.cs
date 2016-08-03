using UnityEngine;
using System.Collections;

public class PanelHider : MonoBehaviour {
    
    public bool showPanel;

    void Update() {
        if (!showPanel)
            UIControl.HidePanel(this);
    }

    private void OnGUI() {
        HideIfClickedOutside(gameObject, Event.current);
    }

    private bool HideIfClickedOutside(GameObject panel, Event e) {
        // do easy checks first.
        if (e.type == EventType.MouseDown && panel.activeSelf) {
            // get the transform of the GUI item you are checking
            RectTransform tform = panel.GetComponent<RectTransform>();
            //use the transform position and size to construct a Rect. 
            // the position is the center of the GUI element, so move it back to the corner
            Vector2 location = new Vector2(
                tform.position.x - tform.rect.size.x / 2,
                tform.position.y - tform.rect.size.y / 2);
            Rect toCheck = new Rect(location, tform.rect.size);
            // invert the y coordinate of the mouse to conform to the GUI coordinates
            if (!toCheck.Contains(new Vector2(e.mousePosition.x, Screen.height - e.mousePosition.y))) {
                showPanel = false;
                return true;
            }
        }
        return false;
    }

}
