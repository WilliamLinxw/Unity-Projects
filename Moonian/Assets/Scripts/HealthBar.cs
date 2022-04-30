using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PropBars;

// public class HealthBar : MonoBehaviour
// {
//     public Slider slider;
//     public Gradient gradient;
//     public Image fill;

//     float lerpSpeed;
//     float targetHealth;

//     public void ResetMaxHealth(float health)
//     {
//         slider.maxValue = health;
//     }

//     public void SetHealth(float health)
//     {
//         if (health > slider.maxValue)
//         {
//             health = slider.maxValue;
//         }

//         targetHealth = health;
//     }

//     private void Update() 
//     {
//         lerpSpeed = 3f * Time.deltaTime;
//         slider.value = Mathf.Lerp(slider.value, targetHealth, lerpSpeed);
//         fill.color = gradient.Evaluate(slider.normalizedValue);
//     }

// }

public class HealthBar : BarsBase
{
    
}
