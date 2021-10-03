using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGrabScript : ATickable
{
    ShipScript ship;
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    Sprite[] sprites;
    int state=0; //0 - nothing, otherwise -1 to access sprite/value

    private void Grab()
    {
        int money = 0;
        switch (state)
        {
            case 1:
                money = Random.Range(1, 6);
                break;
            case 2:
                money = Random.Range(10, 21);
                break;
            case 3:
                money = Random.Range(25, 41);
                break;
            default:
                break;
        }
        ship.money += money;
        if(money>0)
            SFXScript.GetInstance().PlayActivate();
        state = 0;
        sr.gameObject.SetActive(false);
    }
    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUpgrade.onClick.AddListener(Upgrade);
        buttUse.onClick.AddListener(Grab);
    }

    public override void Tick(ShipScript shipscript)
    {
        if (Random.Range(0, 10) != 0)
            return;
        if (state != 0)
        {
            sr.gameObject.SetActive(false);
            state = 0;
            return;
        }
        int roll = Random.Range(0, 100 - 10 * level);
        if (roll < 10)
            state = 3;
        else if (roll < 30)
            state = 2;
        else
            state = 1;
        sr.gameObject.SetActive(true);
        sr.sprite = sprites[state - 1];
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = ShipScript.GetInstance();
    }

}
