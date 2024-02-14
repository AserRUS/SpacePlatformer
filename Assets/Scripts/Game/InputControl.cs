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
