using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private CameraController m_Camera;
    [SerializeField] private Transform m_CameraFlightTarget;
    [SerializeField] private Transform m_CameraStartPosition;
    [SerializeField] private InputControl m_InputControl;

    private void Start()
    {
        m_Camera.SetFlightMode(m_CameraStartPosition, m_CameraFlightTarget);
        m_Camera.FlightFinishEvent += IntroductionEnd;

        m_InputControl.InputControlEnabled(false); 
    }

    private void OnDestroy()
    {
        m_Camera.FlightFinishEvent -= IntroductionEnd;
    }
    private void IntroductionEnd()
    {
        m_Camera.SetFollowMode();
        m_InputControl.InputControlEnabled(true);
    }
        
    
}
