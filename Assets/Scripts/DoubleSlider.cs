using UnityEngine;
using UnityEngine.UI;

public class DoubleSlider : MonoBehaviour
{
    public Slider minSlider;
    public Slider maxSlider;

    [SerializeField]
    private float minValue = 0f;

    [SerializeField]
    private float maxValue = 100f;

    void Start()
    {
        minSlider.onValueChanged.AddListener(OnValueChanged_MinSlider);
        maxSlider.onValueChanged.AddListener(OnValueChanged_MaxSlider);
    }

    public void OnValueChanged_MinSlider(float newValue)
    {
        minValue = newValue;
        minValue = Mathf.Clamp(minValue, minSlider.minValue, maxSlider.value);
        if (minValue > maxSlider.value)
        {
            maxSlider.value = minValue;
        }
        minSlider.value = minValue;
    }

    public void OnValueChanged_MaxSlider(float newValue)
    {
        maxValue = newValue;
        maxValue = Mathf.Clamp(maxValue, minSlider.value, maxSlider.maxValue);
        if (maxValue < minSlider.value)
        {
            minSlider.value = maxValue;
        }
        maxSlider.value = maxValue;
    }

    public void setSlidersUpperLimit(int value)
    {
        maxValue = value;
        minSlider.maxValue = value;
        maxSlider.maxValue = value;
    }

    public void setSlidersLowerLimit(int value)
    {
        minValue = value;
        minSlider.minValue = value;
        maxSlider.minValue = value;
    }

    public void setMinSliderValue(int value)
    {
        minSlider.value = value;
    }

    public void setMaxSliderValue(int value)
    {
        maxSlider.value = value;
    }

    public int getMinSliderValue()
    {
        return (int)minSlider.value;
    }

    public int getMaxSliderValue()
    {
        return (int)maxSlider.value;
    }

    public bool isMaxMinSameValue()
    {
        Debug.Log((int)minSlider.value == (int)maxSlider.value);
        return (int)minSlider.value == (int)maxSlider.value;
    }
}