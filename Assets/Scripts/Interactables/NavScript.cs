using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavScript : ATickable
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private List<SpriteRenderer> spots;
    private int[] values = new int[] { 0, 1, 2, 3, 4, 6, 8 };
    private int[] indeces = new int[3];
    int chosen = 0;

    Color faded = new Color(1, 1, 1, 0.5f);

    private int GetNewValueIndex()
    {
        int i = 0;
        while (i < 4)
        {
            if (Random.Range(0, 6 + level) < 4)
                return i;
            i++;
        }
        return i;
    }

    private void Activate()
    {
        SFXScript.GetInstance().PlayActivate();
        chosen = (chosen + 1) % 3;
        for (int i = 0; i < 3; i++)
        {
            if (i != chosen)
                spots[i].color = faded;
            else
                spots[i].color = Color.white;
        }
    }    

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUpgrade.onClick.AddListener(Upgrade);
        buttUse.onClick.AddListener(Activate);
    }

    public override void Tick(ShipScript shipscript)
    {
        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 100) < 4)
                indeces[i] = GetNewValueIndex();
            spots[i].sprite = sprites[indeces[i]];
        }
        shipscript.AddDistance(values[indeces[chosen]]);
    }

    private void Start()
    {
        Activate();
    }
}
