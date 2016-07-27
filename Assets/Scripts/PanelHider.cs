using UnityEngine;
using System.Collections;

public class PanelHider : MonoBehaviour {

    public GameObject panelToHide;
    public UIControl uiControl;

	void OnMouseUpAsButton(){
        //triggers hide panel 2 levels up since the collider will be inside a holder that's inside the desired object to hide
        uiControl.HidePanel(panelToHide);
    }

}
