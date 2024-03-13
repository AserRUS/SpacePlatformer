using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSetting : MonoBehaviour
{
    [SerializeField] private float m_MaxValue;
    [SerializeField] private float m_MinValue;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameSetting;
    [SerializeField] private Text m_ValueText;
    [SerializeField] private float m_Step;

    private float step;

    private float m_Value;

    private void Start()
    {
        step = (m_MaxValue - m_MinValue) / (100 / m_Step);
        Load();
        Apply();
    }
   
    public void More()
    {
        if ((m_Value + step) > m_MaxValue) return;
        m_Value += step; 
        Apply();
        Save();
    }
    public void Less()
    {
        if ((m_Value - step) < m_MinValue) return;
        m_Value -= step;
        Apply();
        Save();
    }

    private void Apply()
    {
        audioMixer.SetFloat(nameSetting, m_Value);
        m_ValueText.text = Mathf.Round(Mathf.Lerp(0, 100, (m_Value - m_MinValue) / (m_MaxValue - m_MinValue))).ToString();
    }

    private void Load()
    {
        m_Value = PlayerPrefs.GetFloat(nameSetting, m_MaxValue);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(nameSetting, m_Value);
    }
}
