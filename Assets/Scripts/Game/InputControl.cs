using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private ImageChangingTransparency shieldButton;
    [SerializeField] private ImageChangingTransparency attackButton;

    private PlayerInputControl playerInputControl;


    private float mouse0ButtonClamp = 0;
    private float timeLimitForMouse0Clamp = 1f;
    private float mouse1ButtonClamp = 0;
    private float timeLimitForMouse1Clamp = 1f;

    private void Update()
    {
        if (Application.isEditor)
        {
            if (playerInputControl == null) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            #region Mouse0
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mouse0ButtonClamp += Time.deltaTime;

                if (attackButton)
                    attackButton.RemoveTransparency();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                mouse0ButtonClamp += Time.deltaTime;

                if (mouse0ButtonClamp > timeLimitForMouse0Clamp)
                {
                    UseAttack(mouse0ButtonClamp);
                    mouse0ButtonClamp = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                UseAttack(mouse0ButtonClamp);
                mouse0ButtonClamp = 0;

                if (attackButton)
                    attackButton.AddTransparency();
            }
            #endregion

            #region Mouse1
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mouse1ButtonClamp += Time.deltaTime;
                
                if (shieldButton)
                    shieldButton.RemoveTransparency();
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                mouse1ButtonClamp += Time.deltaTime;

                if (mouse1ButtonClamp > timeLimitForMouse1Clamp)
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

    public void Jump()
    {
        playerInputControl?.Jump();
    }

    public void UseAttack(float timeClamp)
    {
        playerInputControl?.UseAttack(timeClamp);
    }

    public void UseShield(float timeClamp)
    {
        playerInputControl.UseShield(timeClamp);
    }

    public void RotateLeft()
    {
        playerInputControl?.RotateLeft();
    }
    public void RotateRight()
    {
        playerInputControl?.RotateRight();
    }

    public void Move(bool isMove)
    {
        playerInputControl?.Move(isMove);
    }

    public void SetPlayerInputControl(PlayerInputControl playerInputControl)
    {
        this.playerInputControl = playerInputControl;
    }
}
