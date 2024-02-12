using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    private PlayerInputControl playerInputControl;
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
        }
           
    }

    private void FixedUpdate()
    {
        if (Application.isEditor)
        {
            if (playerInputControl == null) return;

            if (Input.GetKey(KeyCode.A))
            {
                SetMovementDirection(-1);
                SetWeaponDirection(-1);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                SetMovementDirection(1);
                SetWeaponDirection(1);
            }

            else
                SetMovementDirection(0);
        }
        

    }

    public void Jump()
    {
        playerInputControl.Jump();
    }

    public void Fire()
    {
        playerInputControl.Fire();
    }

    public void SetMovementDirection(int direction)
    {
        playerInputControl.SetMovementDirection(direction);
    }
    public void SetWeaponDirection(int direction)
    {
        playerInputControl.SetWeaponDirection(direction);
    }


    public void SetPlayerInputControl(PlayerInputControl playerInputControl)
    {
        this.playerInputControl = playerInputControl;
    }
}
