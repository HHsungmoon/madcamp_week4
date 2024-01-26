using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    public Slider slider;
    public Gradient grad;
    public Image fill;

    public void SetMaxHP(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;

        fill.color=grad.Evaluate(1f);
    }
    public void SetHP(int hp)
    {
        slider.value = hp;

        fill.color = grad.Evaluate(slider.normalizedValue);
    }
}
