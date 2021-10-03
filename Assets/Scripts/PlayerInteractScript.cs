using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Item
{
    None, Extinguisher, Extinguisher2, Extinguisher3, Plank, Plank2, Plank3
}

public class PlayerInteractScript : MonoBehaviour
{
    public GameObject tooltipCog;
    public SpriteRenderer srItem;
    public ShipScript ship;
    public GameObject tutorial;
    //ui
    public Canvas canvas;
    public Button buttonUse;
    public Button buttonUpgrade;
    public SpriteRenderer sr;
    public Text nameText;
    public Text levelText;
    public Text upgradeText;
    public Text descText;

    public Sprite[] itemSprites;

    public InteractableUI ui;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        ui = new InteractableUI();
        ui.buttonUseText = buttonUse.GetComponentInChildren<Text>();
        ui.buttonUpgradeText = buttonUpgrade.GetComponentInChildren<Text>();
        ui.sr = sr;
        ui.nameText = nameText;
        ui.levelText = levelText;
        ui.upgradeText = upgradeText;
        ui.descText = descText;
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.E))
            buttonUse.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.Q))
            buttonUpgrade.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.H))
            tutorial.SetActive(!tutorial.activeInHierarchy);
        int index = (int)item;
        if (item == 0)
            srItem.gameObject.SetActive(false);
        else
        {
            srItem.gameObject.SetActive(true);
            srItem.sprite = itemSprites[index];
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canvas.gameObject.SetActive(true);
            buttonUse.onClick.RemoveAllListeners();
            buttonUpgrade.onClick.RemoveAllListeners();
            
            ATickable tickable = collision.gameObject.GetComponent<ATickable>();
            tickable.SetUI(ui);
            tickable.SetButtonsEvents(buttonUpgrade, buttonUse,this);
            buttonUpgrade.onClick.AddListener(delegate { tickable.SetUI(ui); });
            tooltipCog.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
            canvas.gameObject.SetActive(false);
            tooltipCog.SetActive(false);
            buttonUpgrade.onClick.RemoveAllListeners();
            buttonUse.onClick.RemoveAllListeners();
    }
}
