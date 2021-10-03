using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableUI
{
    public Text buttonUseText;
    public Text buttonUpgradeText;
    public SpriteRenderer sr;
    public Text nameText;
    public Text levelText;
    public Text upgradeText;
    public Text descText;
}




public abstract class ATickable : MonoBehaviour
{
    public int level;
    public int[] costs;
    public UIObjData uiData;
    abstract public void Tick(ShipScript shipscript);

    protected void Upgrade()
    {
        if (level < costs.Length)
        {
            ShipScript ship = ShipScript.GetInstance();
            if (ship.money >= costs[level])
            {
                ship.money -= costs[level];
                level++;
                SFXScript.GetInstance().PlayUpgrade();
            }
        }
    }

    abstract public void SetButtonsEvents(Button buttUpgrade, Button buttUse,PlayerInteractScript player);
    public void SetUI(InteractableUI ui)
    {
        ui.nameText.text = uiData.name;
        ui.sr.sprite = uiData.sprite;
        ui.buttonUseText.text = uiData.activateText;
        ui.buttonUpgradeText.text = uiData.upgradeText;
        ui.levelText.text = $"Level: {level + 1}";
        ui.descText.text = uiData.descriptionText;
        if (level >= costs.Length)
            ui.upgradeText.text = "max level";
        else
            ui.upgradeText.text = $"Upgrade Cost: {costs[level]}";
    }
}
