using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlder;

    public void SetMaxHealth(int health)
    {
        healthSlder.maxValue = health;
        healthSlder.value = health;
    }

    public void UpdateHealthBar(int health)
    {
        healthSlder.value = health;
    }
}