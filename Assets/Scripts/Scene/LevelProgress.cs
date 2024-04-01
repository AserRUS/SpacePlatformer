using UnityEngine;
using UnityEngine.Events;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance;
    public static event UnityAction OnLevelFinished;

    [SerializeField] private Market m_Market;
    [SerializeField] private UIFinish m_UIFinish;    
           
    private int starCount;
    private int moneyCount;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }                       
    }    
    
    public void LevelFinished()
    {        
        LevelProgressManager.Instance.LevelFinished(starCount);
        m_Market.AddMoney(moneyCount);
        m_UIFinish.Finish(starCount, moneyCount);
        OnLevelFinished?.Invoke();
    }
       

    public void AddStar()
    {
        starCount++;
    }
    public void AddMoney()
    {
        moneyCount++;
    }
}
