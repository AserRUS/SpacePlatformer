using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILevelButton : UIButton
{       
    [SerializeField] private SceneLoader m_SceneLoader;

    private LevelInfo levelInfo;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        LoadScene();
    }    

    public void LoadScene()
    {
        if (levelInfo == null) return;
        LevelProgressManager.Instance.SetCurrentLevel(levelInfo);
        m_SceneLoader.LoadScene(levelInfo.SceneName);
    }

    public void SetLevelInfo(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }
}
