using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : ATickable
{
    private ShipScript ship;

    private void GameStart()
    {
        ship.gameRunning = true;
        gameObject.tag = "Untagged";
        SFXScript.GetInstance().PlayActivate();
    }
    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUse.onClick.AddListener(GameStart);
    }

    public override void Tick(ShipScript shipscript)
    {
        //nope
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = ShipScript.GetInstance();
    }

}
