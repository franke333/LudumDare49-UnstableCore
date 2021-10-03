using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightScript : ATickable
{
    public SpriteRenderer sr;
    bool on = true;

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        
    }

    override public void Tick(ShipScript shipscript)
    {
        if (on)
        {
            on = false;
            sr.color = Color.black;
        }
        else
        {
            on = true;
            sr.color = Color.white;
        }
    }

}
