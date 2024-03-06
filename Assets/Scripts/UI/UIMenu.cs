using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private LevelManager m_LevelManager;

    

    public void NewGame()
    {
        LevelProgressManager.Instance.Remove();
        m_LevelManager.ActivateLevels();
    }
}
