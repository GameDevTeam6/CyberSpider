using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillSection;
    [SerializeField] private Color fullCol;
    [SerializeField] private Color lowCol;

    public void UpdateHealthBar(float currentVal, float maxVal)
    {
        healthSlider.value = currentVal/maxVal;
        fillSection.color = Color.Lerp(lowCol, fullCol, healthSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
