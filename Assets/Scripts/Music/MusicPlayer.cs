using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    [SerializeField] private float m_MaxVolume;
    [SerializeField] private float m_VolumeRate;
    [SerializeField] private AudioMixerGroup m_AudioMixerGroup;

    private AudioSource currentSource;
    private AudioSource nextSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        currentSource = GetComponent<AudioSource>();

        currentSource.volume = 0;

    }

    public void SetLevelMusic(AudioClip audioClip)
    {
        if (currentSource.clip == null)
        {
            currentSource.clip = audioClip;
            currentSource.Play();
        }
        else if (currentSource.clip != audioClip)
        {
            nextSource = gameObject.AddComponent<AudioSource>();
            nextSource.priority = 0;
            nextSource.outputAudioMixerGroup = m_AudioMixerGroup;
            nextSource.clip = audioClip;
            nextSource.volume = 0;
            nextSource.loop = true;
            nextSource.Play();

        }
    }

    private void Update()
    {
        if (currentSource.clip != null && nextSource == null)
            currentSource.volume = Mathf.MoveTowards(currentSource.volume, m_MaxVolume, m_VolumeRate * Time.deltaTime);
        else if (currentSource.clip != null && nextSource != null)
            currentSource.volume = Mathf.MoveTowards(currentSource.volume, 0, m_VolumeRate * Time.deltaTime);

        if (nextSource != null)
        {
            nextSource.volume = Mathf.MoveTowards(nextSource.volume, m_MaxVolume, m_VolumeRate * Time.deltaTime);
            if (nextSource.volume == m_MaxVolume && currentSource.volume == 0)
            {
                Destroy(currentSource);
                currentSource = nextSource;                
                nextSource = null;
            } 
        }
            
    }


}
