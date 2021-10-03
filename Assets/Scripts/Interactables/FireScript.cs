using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : ATickable
{

    SpriteRenderer sr;
    PlayerInteractScript player;

    Vector3 baseScale;
    public float spawnChance = 0.03f;
    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        this.player = player;
        buttUse.onClick.AddListener(Repair);
        buttUse.onClick.AddListener(delegate { SetUI(player.ui); });
    }

    public void Repair()
    {
        bool fail = false;
        switch (player.item)
        {
            case Item.Extinguisher:
                player.item = Item.None;
                gameObject.SetActive(false);
                break;
            case Item.Extinguisher2:
                player.item = Item.Extinguisher;
                gameObject.SetActive(false);
                break;
            case Item.Extinguisher3:
                player.item = Item.Extinguisher2;
                gameObject.SetActive(false);
                break;
            default:
                fail = true;
                break;
        }
        if (!fail)
            SFXScript.GetInstance().PlayExt();
    }

    public override void Tick(ShipScript shipscript)
    {
        if (gameObject.activeSelf)
        {
            shipscript.ChangeHeat(level / 3 + 1);
            level++;
            transform.localScale = baseScale * (level / 20f + 1);
        }
        else if (Random.Range(0f, 1f) < spawnChance)
        {
            SFXScript.GetInstance().PlayFire();
            transform.localScale = baseScale;
            gameObject.SetActive(true);
            level = 0;
        }
    }

    private void Start()
    {
        baseScale = transform.localScale;
        gameObject.SetActive(false);

    }
}
