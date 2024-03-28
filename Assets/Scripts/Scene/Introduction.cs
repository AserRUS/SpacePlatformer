using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private CameraController m_Camera;
    [SerializeField] private Transform m_CameraFlightTarget;
    [SerializeField] private Transform m_CameraStartPosition;
    [SerializeField] private InputControl m_InputControl;
    [SerializeField] private UIInstructionPanel m_InstructionPanel;
    [SerializeField] private GameObject m_SkipButton;

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
    public void IntroductionEnd()
    {
        m_SkipButton.SetActive(false);
        m_Camera.SetFollowMode();
        m_InputControl.InputControlEnabled(true);

        if (m_InstructionPanel != null)
            m_InstructionPanel.OpenInstructionPanel();
    }
        
    
}
