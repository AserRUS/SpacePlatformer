
using UnityEngine;

public class InterfacePositionSettings : MonoBehaviour
{
    private enum InterfaceLayout
    {
        Left,
        Right
    }

    [SerializeField] RectTransform[] m_MainUIElements;
    [SerializeField] RectTransform[] m_SecondaryUIElements;

    private InterfaceLayout interfaceLayout = InterfaceLayout.Left;


    private void Start()
    {
        
    }
    public void Left()
    {
        if (interfaceLayout == InterfaceLayout.Left) return;

        for (int i = 0; i < m_MainUIElements.Length; i++)
        {
            m_MainUIElements[i].anchoredPosition = Vector2.one;
        }

        interfaceLayout = InterfaceLayout.Left;
    }

    public void Right()
    {
        if (interfaceLayout == InterfaceLayout.Right) return;


        interfaceLayout = InterfaceLayout.Right;
    }
}
