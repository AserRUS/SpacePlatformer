using UnityEngine;


public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_MusicClips; 

    private void Start()
    {
        GetLevelMusic();
    }

    private void GetLevelMusic()
    {
        MusicPlayer.Instance.SetLevelMusic(m_MusicClips[Random.Range(0, m_MusicClips.Length)]);
    }
}
