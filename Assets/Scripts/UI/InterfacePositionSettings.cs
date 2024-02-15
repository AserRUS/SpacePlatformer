
using UnityEngine;
using UnityEngine.UI;

public class InterfacePositionSettings : MonoBehaviour
{
    private enum InterfaceLayout
    {
        Left,
        Right
    }

    [SerializeField] RectTransform[] m_MainUIElements;
    [SerializeField] RectTransform[] m_SecondaryUIElements;
    [SerializeField] Toggle m_LeftToggle;
    [SerializeField] Toggle m_RightToggle;

    private InterfaceLayout interfaceLayout = InterfaceLayout.Right;


    private void Start()
    {
        Load();

        if (m_LeftToggle != null && m_RightToggle != null)
        {
           m_LeftToggle.isOn = interfaceLayout == InterfaceLayout.Left;
            m_RightToggle.isOn = interfaceLayout == InterfaceLayout.Right;
        }

        if (interfaceLayout == InterfaceLayout.Right)
        {  
            Right();
        }
        else if (interfaceLayout == InterfaceLayout.Left)
        {       
            Left();
        }
    }

    public void Left()
    { 
        for (int i = 0; i < m_MainUIElements.Length; i++)
        {            
            m_MainUIElements[i].anchorMax = new Vector2(0, m_MainUIElements[i].anchorMax.y);
            m_MainUIElements[i].anchorMin = new Vector2(0, m_MainUIElements[i].anchorMin.y);

            m_MainUIElements[i].anchoredPosition = new Vector2(Mathf.Abs(m_MainUIElements[i].anchoredPosition.x), m_MainUIElements[i].anchoredPosition.y);
        }
        for (int i = 0; i < m_SecondaryUIElements.Length; i++)
        { 
            
            m_SecondaryUIElements[i].anchorMax = new Vector2(1, m_SecondaryUIElements[i].anchorMax.y);
            m_SecondaryUIElements[i].anchorMin = new Vector2(1, m_SecondaryUIElements[i].anchorMin.y);

            m_SecondaryUIElements[i].anchoredPosition = new Vector2(-Mathf.Abs(m_SecondaryUIElements[i].anchoredPosition.x), m_SecondaryUIElements[i].anchoredPosition.y);

        }

        interfaceLayout = InterfaceLayout.Left;
        Save();
    }

    public void Right()
    {
        for (int i = 0; i < m_MainUIElements.Length; i++)
        {  
            m_MainUIElements[i].anchorMax = new Vector2(1, m_MainUIElements[i].anchorMax.y);
            m_MainUIElements[i].anchorMin = new Vector2(1, m_MainUIElements[i].anchorMin.y);

            m_MainUIElements[i].anchoredPosition = new Vector2(-Mathf.Abs(m_MainUIElements[i].anchoredPosition.x), m_MainUIElements[i].anchoredPosition.y);
        }
        for (int i = 0; i < m_SecondaryUIElements.Length; i++)
        {  
            m_SecondaryUIElements[i].anchorMax = new Vector2(0, m_SecondaryUIElements[i].anchorMax.y);
            m_SecondaryUIElements[i].anchorMin = new Vector2(0, m_SecondaryUIElements[i].anchorMin.y);

            m_SecondaryUIElements[i].anchoredPosition = new Vector2(Mathf.Abs(m_SecondaryUIElements[i].anchoredPosition.x), m_SecondaryUIElements[i].anchoredPosition.y);
        }

        interfaceLayout = InterfaceLayout.Right;
        Save();
    }


    private void Save()
    {
        PlayerPrefs.SetInt("InterfaceLayout", (int)interfaceLayout);
    }
    private void Load()
    {
        interfaceLayout = (InterfaceLayout) PlayerPrefs.GetInt("InterfaceLayout", 1);
    }
}
