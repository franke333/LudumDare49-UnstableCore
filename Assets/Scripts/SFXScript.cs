using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    [SerializeField]
    AudioSource alarmS;
    [SerializeField]
    AudioSource upgradeS;
    [SerializeField]
    AudioSource selectS;
    [SerializeField]
    AudioSource crackS;
    [SerializeField]
    AudioSource fireS;
    [SerializeField]
    AudioSource extS;
    [SerializeField]
    AudioSource repairS;
    private SFXScript() { }
    static private SFXScript _Instance;


    private float waited = 0;
    static public SFXScript GetInstance()
    {
        if (_Instance != null)
            return _Instance;
        _Instance = new SFXScript();
        return _Instance;
    }

    private void Start()
    {
        _Instance = this;
    }

    private void Update()
    {
        waited += Time.deltaTime;
    }

    public void PlayUpgrade()
    {
        upgradeS.Play();
    }

    public void PlayActivate()
    {
        selectS.Play();
    }

    public void PlaySelect()
    {
        PlayActivate();
    }

    public void PlayFire()
    {
        if (waited > 1)
        {
            fireS.Play();
            waited = 0;
        }
    }

    public void PlayCrack()
    {
        if (waited > 1)
        {
            crackS.Play();
            waited = 0;
        }
    }

    public void PlayAlarm()
    {
        alarmS.Play();
    }

    public void PlayExt()
    {
        extS.Play();
    }

    public void PlayRepair()
    {
        repairS.Play();
    }
}
