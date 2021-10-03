using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderScript : MonoBehaviour
{
    public Image fill;
    public Text percentText;


    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if (percentText == null)
            percentText = GameObject.Find("PercentText").GetComponent<Text>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fill.color = new Color(slider.value,1f-slider.value,0.1f);
        percentText.text = $"{(int)(slider.value*100)}%";
    }
}
