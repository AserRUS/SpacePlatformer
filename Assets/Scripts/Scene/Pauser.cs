using UnityEngine;


public class Pauser : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject[] m_ClosingPanels;
    [SerializeField] private InputControl m_InputControl;
    [SerializeField] private PlayerSpawner m_PlayerSpawner;
    private bool isPause;
    private bool isFinish = false;

    
    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {                        
            ChangePauseState();            
        }
    }  

    public void Pause()
    {
        m_PlayerSpawner.Player.transform.GetComponent<AudioSource>().Pause();
        m_InputControl.enabled = false;
        isPause = true;
        m_PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void UnPause()
    {
        m_PlayerSpawner.Player.transform.GetComponent<AudioSource>().UnPause();
        m_InputControl.enabled = true;
        isPause = false;
        m_PausePanel.SetActive(false);
        for (int i = 0; i < m_ClosingPanels.Length; i++)
        {
            m_ClosingPanels[i].SetActive(false);
        }
        Time.timeScale = 1.0f;
    }

    public void ChangePauseState()
    {
        if (isFinish == true) return;

        if (isPause == true)
        {            
            UnPause();
        }            
        else
        {            
            Pause();
        }

       
    }

    internal void SetFinish()
    {
        isFinish = true;
        UnPause();
    }
}
