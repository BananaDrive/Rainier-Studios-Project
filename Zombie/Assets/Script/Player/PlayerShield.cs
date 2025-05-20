using UnityEngine;
using UnityEngine.UI;


public class PlayerShield : MonoBehaviour
{
    public Slider slider;
    public GameObject HeavyShield;
    public float MaxShield = 100;
    public float MinShield = 0;
    public float ShieldGive = 20;
    public float ShieldAmount = 100;

    void Update()
    {
        slider.maxValue = MaxShield;
        slider.value = ShieldAmount;
    }

    public void UpdateSlider(float Shield) => slider.value = Shield;
}
