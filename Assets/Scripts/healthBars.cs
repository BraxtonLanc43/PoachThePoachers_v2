using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBars : MonoBehaviour
{
    //UI
    public Slider slider;

    public void setHealth(int healthToSet)
    {
        slider.value = healthToSet;
    }

    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

}
