using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridgeScript : ATickable
{
    public GameObject handle;
    public float currentAngle = 0.5f;
    ShipScript ship;
    private int GetDistance()
    {
        float diff = Mathf.Abs(0.5f - currentAngle);
        if (diff < 0.1f)
            return 5;
        if (diff < 0.2f)
            return 3;
        if (diff < 0.35f)
            return 1;
        return 0;
    }
    private void RotateMyself()
    {
        handle.transform.rotation = Quaternion.identity;
        handle.transform.Rotate(Vector3.forward * (currentAngle - 0.5f) * 180);
    }
    private void Activate()
    {
        SFXScript.GetInstance().PlayActivate();
        float add = 0.1f;
        float noise = Random.Range(-0.05f / (level+1), 0.3f / (2*level+1));
        currentAngle = Mathf.Max(currentAngle - add - noise,0);
        RotateMyself();
    }

    public override void SetButtonsEvents(Button buttUpgrade, Button buttUse, PlayerInteractScript player)
    {
        buttUse.onClick.AddListener(Activate);
        buttUpgrade.onClick.AddListener(Upgrade);
    }

    public override void Tick(ShipScript shipscript)
    {
        currentAngle += 0.035f / (level+1);
        currentAngle = Mathf.Min(currentAngle, 1.0f);
        ship.AddDistance(GetDistance());
        RotateMyself();
    }

    private void Start()
    {
        ship = ShipScript.GetInstance();
    }


}
