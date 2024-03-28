using UnityEngine;
using UnityEngine.UI;

public class UISliderButton : MonoBehaviour
{
    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private UIClampingButton clampingButton;
    
    private Image imageSlider;

    private void Start()
    {
        clampingButton.ClampTimeChangeEvent += UpdateFillAmount;
        imageSlider = GetComponent<Image>();
        imageSlider.fillAmount = 0;
    }

    private void OnDestroy()
    {
        clampingButton.ClampTimeChangeEvent -= UpdateFillAmount;
    }

    private void UpdateFillAmount(float time)
    {
        imageSlider.fillAmount = time / buttonPressDuration.MaxTimeButtonClamp;
    }
}
