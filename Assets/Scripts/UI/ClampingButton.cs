using UnityEngine;
using UnityEngine.EventSystems;

public enum ClampingButtonType
{
    Shield,
    Attack
}

public class ClampingButton : UIButton
{
    [SerializeField] private InputControl inputControl;
    [SerializeField] private ClampingButtonType type;

    private float clampTime;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        clampTime = Time.fixedTime;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        clampTime = Time.fixedTime - clampTime;

        if (type == ClampingButtonType.Shield)
            UseShield(clampTime);

        if (type == ClampingButtonType.Attack) 
            UseAttack(clampTime);
    }

    public void UseShield(float time)
    {
        inputControl.UseShield(time);
    }

    public void UseAttack(float time)
    {
        inputControl.UseAttack(time);
    }
}
