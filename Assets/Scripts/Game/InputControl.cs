using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private ImageChangingTransparency shieldButton;

    private PlayerInputControl playerInputControl;

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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Fire();
            }

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

    public void Fire()
    {
        playerInputControl?.Fire();
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
