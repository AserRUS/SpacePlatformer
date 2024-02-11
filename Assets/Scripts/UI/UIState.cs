using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    [SerializeField] private Slider m_HealthSlider;

    public void SetMaxValue(int value)
    {
        m_HealthSlider.maxValue = value;
    }

    public void ValueChange(int value)
    {
        m_HealthSlider.value = value;
    }
}
