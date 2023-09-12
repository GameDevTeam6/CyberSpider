using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void UpdateHealthBar(float currentVal, float maxVal)
    {
        healthSlider.value = currentVal/maxVal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
