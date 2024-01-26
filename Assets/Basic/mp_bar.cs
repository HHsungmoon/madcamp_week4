using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mp_bar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider2;

    public void SetMaxMP(int mp)
    {
        slider2.maxValue = mp;
        slider2.value = mp;
    }
    public void SetMP(int mp)
    {
        slider2.value = mp;
    }
}
