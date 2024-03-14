using UnityEngine;
using UnityEngine.Events;

public class InputControl : MonoBehaviour
{
    public event UnityAction<bool> InputControlStateChanged;

    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private UIImageChangingTransparency shieldButton;
    [SerializeField] private UIImageChangingTransparency attackButton;
    [SerializeField] private New_UIClampingButton shieldButton_New;

    private PlayerInputControl playerInputControl;


    private float EButtonClamp = 0;
    private float mouse1ButtonClamp = 0;

    private bool isInputControlEnabled = true;
    private bool isAttackEnabled = true;
    private bool isShieldEnabled = true;

    private void Start()
    {
        shieldButton_New.OnPointerDownEvent.AddListener(IncreaseShield);
        shieldButton_New.OnPointerUpEvent.AddListener(StopShieldIncrease);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (playerInputControl == null) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            #region E
            if (Input.GetKeyDown(KeyCode.E))
            {
                EButtonClamp += Time.deltaTime;

                if (attackButton)
                    attackButton.SmoothRemoveTransparency();//RemoveTransparency();
            }

            if (Input.GetKey(KeyCode.E))
            {
                EButtonClamp += Time.deltaTime;

                if (EButtonClamp > buttonPressDuration.TimeLimitForButtonClamp)
                {
                    UseAttack(EButtonClamp);
                    EButtonClamp = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                UseAttack(EButtonClamp);
                EButtonClamp = 0;

                if (attackButton)
                    attackButton.AddTransparency();
            }
            #endregion

            #region Mouse1
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                shieldButton_New.ButtonDown();
                IncreaseShield(shieldButton_New);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                shieldButton_New.ButtonUp();
                StopShieldIncrease(shieldButton_New);
            }
            #endregion

            #region Mouse1_Old
            /*
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mouse1ButtonClamp += Time.deltaTime;

                if (shieldButton)
                    shieldButton.SmoothRemoveTransparency(); //RemoveTransparency();
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                mouse1ButtonClamp += Time.deltaTime;

                if (mouse1ButtonClamp > buttonPressDuration.TimeLimitForButtonClamp)
                {
                    UseShield(mouse1ButtonClamp);
                    mouse1ButtonClamp = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                UseShield(mouse1ButtonClamp);
                mouse1ButtonClamp = 0;

                if (shieldButton)
                    shieldButton.AddTransparency();
            }
            */
            #endregion
        }

    }

    private void FixedUpdate()
    {
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
                Move(true);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
                Move(true);
            }

            else
            {
                Move(false);
            }
            
        }     

    }

    private void OnDestroy()
    {
        shieldButton_New.OnPointerDownEvent.RemoveListener(IncreaseShield);
        shieldButton_New.OnPointerUpEvent.RemoveListener(StopShieldIncrease);
    }

    public void Jump()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.Jump();
    }

    public void UseAttack(float timeClamp)
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.UseAttack(timeClamp);
    }

    public void UseShield(float timeClamp)
    {
        if (isInputControlEnabled == false) return;
        playerInputControl.UseShield(timeClamp);
    }

    private void IncreaseShield(UIButton button)
    {
        if (isInputControlEnabled == false) return;
        if (isShieldEnabled == false) return;
        playerInputControl.IncreaseShield();
    }

    private void StopShieldIncrease(UIButton button)
    {
        if (isInputControlEnabled == false) return;
        if (isShieldEnabled == false) return;
        playerInputControl.StopShieldIncrease();
    }

    public void RotateLeft()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.RotateLeft();
    }
    public void RotateRight()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.RotateRight();
    }

    public void Move(bool isMove)
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.Move(isMove);
    }

    public void SetPlayerInputControl(PlayerInputControl playerInputControl)
    {
        this.playerInputControl = playerInputControl;
    }

    public void InputControlEnabled(bool value)
    {
        isInputControlEnabled = value;
        InputControlStateChanged?.Invoke(value);
    }
    public void AttackEnabled(bool value)
    {
        isAttackEnabled = value;
    }

    public void ShieldEnabled(bool value)
    {
        isShieldEnabled = value;
    }
}
