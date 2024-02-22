using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private ImageChangingTransparency shieldButton;

    private PlayerInputControl playerInputControl;

    private float mouse1ButtonClamp = 0;
    private float timeLimitForMouse1Clamp = 1f;

    private bool isInputControlEnabled = true;

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

   

    public void Jump()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.Jump();
    }

    public void Fire()
    {
        if (isInputControlEnabled == false) return;
        playerInputControl?.Fire();
    }

    public void Stop(bool value)
    {
        playerInputControl?.Stop(value);
    }

    public void UseShield(float timeClamp)
    {
        if (isInputControlEnabled == false) return;
        playerInputControl.UseShield(timeClamp);
    }
    

    public void SetPlayerInputControl(PlayerInputControl playerInputControl)
    {
        this.playerInputControl = playerInputControl;
    }

    public void InputControlEnabled(bool value)
    {
        isInputControlEnabled = value;
    }
}
