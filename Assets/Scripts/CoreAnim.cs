using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoreAnim : MonoBehaviour
{
    public float speed;
    public float hoverRange;
    public float hoverFrequence;

    public float heat = 0;
    Vector3 pos;
    Vector3 scale;
    SpriteRenderer image;
    ShipScript ship;
    private void Start()
    {
        pos = transform.position;
        scale = transform.localScale;
        image = GetComponent<SpriteRenderer>();
        ship = ShipScript.GetInstance();

    }

    // Update is called once per frame
    void Update()
    {
        if (!ship.gameRunning)
            return;
        transform.Rotate(new Vector3(0, 0, speed*(Mathf.Min(heat,1)*3+1))*Time.deltaTime);
        transform.localScale = scale * (heat*2 + 1);
        transform.position = pos + new Vector3(0,(Mathf.Sin(Time.time*hoverFrequence)/2+0.4f)*hoverRange,0);
        image.color = new Color(1, 1-heat, 1-heat);
    }
}
