using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACScript : ATickable
{
    ShipScript ship;
    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUpgrade.onClick.AddListener(Upgrade);
    }

    public override void Tick(ShipScript shipscript)
    {
        ship.ChangeHeat(-(1 + level * 2));
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = ShipScript.GetInstance(); 
    }
}
