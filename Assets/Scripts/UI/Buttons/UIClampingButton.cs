using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIClampingButton : UIButton
{
    public event UnityAction<float> ClampTimeChangeEvent;

    private bool isButtonClamp;
    private float clampTime;

    private void Update()
    {
        ChangeClampTime();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        ButtonDown();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        ButtonUp();
    }

    public void ButtonDown()
    {
        if (Interactable == false) return;
        isButtonClamp = true;
    }

    public void ButtonUp()
    {
        if (Interactable == false) return;

        isButtonClamp = false;
        clampTime = 0;
        ClampTimeChangeEvent?.Invoke(clampTime);
    }

    private void ChangeClampTime()
    {
        if (!isButtonClamp) return;

        clampTime += Time.deltaTime;
        ClampTimeChangeEvent?.Invoke(clampTime);
    }

    public void SetUnInteractable()
    {
        Interactable = false;
        isButtonClamp = false;

        clampTime = 0;
        ClampTimeChangeEvent?.Invoke(clampTime);
        OnPointerUpEvent?.Invoke(this);
    }

    public void SetInteractable()
    {
        Interactable = true;
    }
}
