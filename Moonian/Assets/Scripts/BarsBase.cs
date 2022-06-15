using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PropBars
{
    // the base class of the bars. All bar objects inherit from this
    public class BarsBase : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;
        public bool isGradFilled = true;

        float lerpSpeed, targetValue;

        public void ResetMaxValue(float value)
        {
            slider.maxValue = value;
        }

        // set the value of the slider
        public void SetValue(float value)
        {
            if (value > slider.maxValue)
            {
                value = slider.maxValue;
            }
            targetValue = value;
        }

        private void Update() {
            lerpSpeed = 3f * Time.deltaTime;  // to make it smooth
            slider.value = Mathf.Lerp(slider.value, targetValue, lerpSpeed);
            if (isGradFilled)
            {
                fill.color = gradient.Evaluate(slider.normalizedValue);
            }
        }
    }
}
