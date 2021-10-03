using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreCooldownScript : ATickable
{

    public Sprite spriteReady;
    public Sprite spriteCD;
    public Sprite spriteActive;
    bool cooling = false;
    public int[] cooldowns = new int[] { 30,25,20 };
    private int coolTime = 15;

    public SpriteRenderer sr;

    private int coolTimeRemaining = 0;
    private int cooldownRemaining = 0;
    public override void Tick(ShipScript shipscript)
    {
        coolTimeRemaining -= 1;
        cooldownRemaining -= 1;
        if (cooling && coolTimeRemaining<=0)
        {
            cooling = false;
            cooldownRemaining = cooldowns[level];
            sr.sprite = spriteCD;
        }
        else if(!cooling && cooldownRemaining <= 0)
        {
            sr.sprite = spriteReady;
        }
        if (cooling)
            shipscript.ChangeHeat(-10-4*level);
    }


    private void Activate()
    {
        if(cooling==false && cooldownRemaining <= 0)
        {
            SFXScript.GetInstance().PlayActivate();
            cooling = true;
            coolTimeRemaining = coolTime;
            sr.sprite = spriteActive;
        }

    }

    

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse,PlayerInteractScript player)
    {
        buttUse.onClick.AddListener(Activate);
        buttUpgrade.onClick.AddListener(Upgrade);
    }
}
