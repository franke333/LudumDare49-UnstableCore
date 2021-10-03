using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipScript : MonoBehaviour
{
    public SpriteRenderer endScreen;
    public Text endScreenText;
    public Button endScreenButt;
    [SerializeField]
    private Slider heatSlider;
    [SerializeField]
    private CoreAnim coreAnim;
    [SerializeField]
    private List<ATickable> tickables;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text moneyText;

    public SpriteRenderer wall;

    private AudioSource audioS;
    


    public int distance = 0;
    public int money = 0;
    public PlayerInteractScript playerInteractScript;

    private float tickReduction=0.0035f;
    private float tickTime = 1.50f;
    private float tickMin = 0.4f;
    private float untilNextTick = 0;
    private int heat=0;
    private int maxHeat=1000;
    
    public bool gameRunning = false;

    private int[] passiveHeating = new int[] {2,2,3,5,6,8,10,10,15,15,18,20,22,25,27,29,33,37,40,100,1000,1000 };

    private int tickCount = 0;

    

    private static ShipScript _Instance;
    public bool gameOver = false;


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static ShipScript GetInstance() => _Instance;

    public void ChangeHeat(int byValue)
    {
        bool cond = heat < 700;
        bool revCond = heat >= 700;
        heat += byValue;
        heat = Mathf.Max(heat, 0);
        if (cond && heat >= 700)
        {
            SFXScript.GetInstance().PlayAlarm();
            wall.color = new Color(109f/255,28f/255,28f/255);
        }
        if(revCond && heat < 700)
        {
            float grey = 96 / 255f;
            wall.color = new Color(grey, grey, grey);
        }
    }

    public void AddDistance(int value)
    {
        int hundreds = distance / 100;
        distance += value*(Mathf.Min(tickCount/100,3)+1);
        money += (distance / 100 - hundreds) * 5;
    }

    private void Awake()
    {
        _Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        playerInteractScript = GameObject.Find("Player").GetComponent<PlayerInteractScript>();
        if (tickables == null)
            tickables = new List<ATickable>();
        if (!audioS.isPlaying)
            audioS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString("D8");
        distanceText.text = distance.ToString("D8");
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (audioS.isPlaying)
                audioS.Stop();
            else
                audioS.Play();
        }
        if (!gameRunning)
            return;
        if (!gameOver)
        {
            untilNextTick -= Time.deltaTime;
            if (untilNextTick < 0)
            {
                untilNextTick = tickTime;
                ChangeHeat(passiveHeating[tickCount / 50]);
                AddDistance(1);
                //get the game faster
                if (tickTime > tickMin)
                {
                    tickTime -= tickReduction;
                    tickCount++; //multiplier
                }
                foreach (ATickable tickable in tickables)
                    tickable.Tick(this);
                float heatInPercent = Mathf.Min(((float)heat) / maxHeat,1f);
                coreAnim.heat = heatInPercent;
                heatSlider.value = heatInPercent;
                if (heat >= maxHeat)
                    gameOver = true;
            }
        }
        else //gameOver
        {
            coreAnim.heat += Time.deltaTime;
            audioS.volume = Mathf.Max(audioS.volume-Time.deltaTime,0);
            endScreen.color = new Color(1,1,1,Mathf.Min(endScreen.color.a + Time.deltaTime/4, 1f));
            if (endScreen.color.a > 0.75f)
            {
                audioS.Stop();
                if (endScreenText == null)
                    endScreenText = endScreen.GetComponentInChildren<Text>();
                endScreenButt.gameObject.SetActive(true);
                endScreenText.gameObject.SetActive(true);
                endScreenText.text = $"Distance: {distance}";
            }

        }
    }
}
