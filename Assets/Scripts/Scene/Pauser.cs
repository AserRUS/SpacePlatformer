using UnityEngine;


public class Pauser : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel;

    private bool isPause;

    
    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {                        
            ChangePauseState();            
        }
    }  

    public void Pause()
    {
        isPause = true;
        m_PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void UnPause()
    {
        isPause = false;
        m_PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ChangePauseState()
    {       

        if (isPause == true)
        {            
            UnPause();
        }            
        else
        {            
            Pause();
        }

       
    }
}
