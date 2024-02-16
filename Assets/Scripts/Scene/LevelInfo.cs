using UnityEngine;


[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    public string SceneName => m_SceneName;

    [SerializeField] private string m_SceneName;
}
