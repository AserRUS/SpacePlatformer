using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    //public event UnityAction OnCutsceneStart;
    public event UnityAction OnCutsceneEnd;

    public static CutsceneManager Instance;
    public static GameObject activeCutscene;


    private void Awake()
    {
        Instance = this;
    }
    public void StartCutscene(GameObject cutScene)
    {
        if (activeCutscene)
            return;
        
        activeCutscene = cutScene;
        activeCutscene.SetActive(true);
    }

    public void EndCutscene()
    {
        if (activeCutscene)
        {
            activeCutscene.SetActive(false);
            activeCutscene = null;
            OnCutsceneEnd?.Invoke();
        }
    }
}
