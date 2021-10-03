using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankScript : ATickable
{
    PlayerInteractScript player;

    private void Use()
    {
        SFXScript.GetInstance().PlayActivate();
        switch (level)
        {
            case 0:
                player.item = Item.Plank;
                break;
            case 1:
                player.item = Item.Plank2;
                break;
            case 2:
                player.item = Item.Plank3;
                break;
            default:
                break;
        }
    }

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        this.player = player;
        buttUpgrade.onClick.AddListener(Upgrade);
        buttUse.onClick.AddListener(Use);

    }

    public override void Tick(ShipScript shipscript)
    {
        //nothing
    }
}
