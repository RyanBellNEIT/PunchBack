using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Gradient gradient;
    private Slider slider;
    private Image fill;

    private void Start()
    {
        slider = GetComponent<Slider>();
        fill = this.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
