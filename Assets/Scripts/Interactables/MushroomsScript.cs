using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomsScript : ATickable
{
    public SpriteRenderer watches;
    public Sprite[] spritesWatches;
    public Sprite seedSprite;
    public Sprite moneyShroomSprite;
    public Sprite heatShroomSprite;
    public SpriteRenderer[] shroomSpots;
    public enum ShroomType
    {
        None,Seed,Money,Heat
    }
    ShipScript ship;
    ShroomType[] shrooms = new ShroomType[5];
    int wait=0;

    private void Activate()
    {
        if (wait <= 0)
        {
            SFXScript.GetInstance().PlayActivate();
            wait = 4-level/2;
            watches.sprite = spritesWatches[4-wait];
            for (int i = 0; i <= level; i++)
            {
                switch (shrooms[i])
                {
                    case ShroomType.None:
                        if (ship.money > 0)
                        {
                            ship.money -= 1;
                            shrooms[i] = ShroomType.Seed;
                            return;
                        }
                        break;
                    case ShroomType.Money:
                        shrooms[i] = ShroomType.None;
                        ship.money += Random.Range(5, 21);
                        return;
                    case ShroomType.Heat:
                        shrooms[i] = ShroomType.None;
                        ship.ChangeHeat(-Random.Range(3, 11) * 10); //3-10%
                        return;
                    default:
                        break;
                }
            }
        }
    }

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUpgrade.onClick.AddListener(Upgrade);
        buttUse.onClick.AddListener(Activate);
    }

    public override void Tick(ShipScript shipscript)
    {
        wait -= 1;
        watches.sprite = spritesWatches[Mathf.Min(Mathf.Abs(4-wait), 4)];

        for (int i = 0; i <shrooms.Length; i++)
        {
            switch (shrooms[i])
            {
                case ShroomType.None:
                    shroomSpots[i].gameObject.SetActive(false);
                    break;
                case ShroomType.Seed:
                    shroomSpots[i].gameObject.SetActive(true);
                    if (Random.Range(0, 100) <= 1 )
                    {
                        if (Random.Range(0, 2) == 0)
                            shrooms[i] = ShroomType.Money;
                        else
                            shrooms[i] = ShroomType.Heat;
                        i--; //go again and render correct sprite
                    }
                    else
                        shroomSpots[i].sprite = seedSprite;
                    break;
                case ShroomType.Money:
                    shroomSpots[i].gameObject.SetActive(true);
                    shroomSpots[i].sprite = moneyShroomSprite;
                    break;
                case ShroomType.Heat:
                    shroomSpots[i].gameObject.SetActive(true);
                    shroomSpots[i].sprite = heatShroomSprite;
                    break;
                default:
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = ShipScript.GetInstance();
    }
}
