using UnityEngine;
using UnityEngine.UI;

public class BarSystem : MonoBehaviour
{
    public Slider Slider;
    public void SetMaxValue(float value)
    {
        Slider.maxValue = value;
    }
    public void SetValue(float value)
    {
        Slider.value = value;
    }
}
