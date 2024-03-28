using UnityEngine;
using UnityEngine.Events;

public class InputControl : MonoBehaviour
{
    public event UnityAction<bool> InputControlStateChanged;

    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private UIClampingButton shieldButton;
    [SerializeField] private UIClampingButton attackButton;

    private PlayerInputControl playerInputControl;

    private bool isInputControlEnabled = true;
    private bool isAttackEnabled = true;
    private bool isShieldEnabled = true;

    private void Start()
    {
        shieldButton.OnPointerDownEvent.AddListener(IncreaseShield);
        shieldButton.OnPointerUpEvent.AddListener(StopShieldIncrease);

        attackButton.ClampTimeChangeEvent += CheckAttackButtonClamp;
        attackButton.OnPointerDownEvent.AddListener(StartAttack);
        attackButton.OnPointerUpEvent.AddListener(StopAttack);
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
                if (isAttackEnabled == false) return;

                attackButton.ButtonDown();
                StartAttack(attackButton);
            } 
            else if (Input.GetKey(KeyCode.E))
            {
                if (isAttackEnabled == false) return;


            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                if (isAttackEnabled == false) return;

                attackButton.ButtonUp();
                StartAttack(attackButton);
            }
            #endregion

            #region Mouse1
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (isShieldEnabled == false) return;

                shieldButton.ButtonDown();
                IncreaseShield(shieldButton);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (isShieldEnabled == false) return;

                shieldButton.ButtonUp();
                StopShieldIncrease(shieldButton);
            }
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
        shieldButton.OnPointerDownEvent.RemoveListener(IncreaseShield);
        shieldButton.OnPointerUpEvent.RemoveListener(StopShieldIncrease);

        attackButton.ClampTimeChangeEvent -= CheckAttackButtonClamp;
        attackButton.OnPointerDownEvent.RemoveListener(StartAttack);
        attackButton.OnPointerUpEvent.RemoveListener(StopAttack);
    }

    public void Jump()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.Jump();
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

    private void StartAttack(UIButton button)
    {
        if (isInputControlEnabled == false) return;
        if (isAttackEnabled == false) return;

        playerInputControl.StartAttack();
    }

    private void CheckAttackButtonClamp(float time)
    {
        if (isInputControlEnabled == false) return;
        if (isAttackEnabled == false) return;

        playerInputControl.CheckAttackButtonClamp(time);
    }

    private void StopAttack(UIButton button)
    {
        if (isInputControlEnabled == false) return;
        if (isAttackEnabled == false) return;

        playerInputControl.StopAttack();
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
