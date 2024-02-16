using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        ActivateLevels();
    }

    public void ActivateLevels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            LevelActivator levelActivator = transform.GetChild(i).GetComponent<LevelActivator>();
            levelActivator.LevelActive();
        }
    }
}
