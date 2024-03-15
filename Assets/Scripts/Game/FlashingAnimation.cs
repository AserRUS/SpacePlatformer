using UnityEngine;

public class FlashingAnimation : MonoBehaviour
{
    [SerializeField] private GameObject m_VisualModel;
    [SerializeField] private float m_FlashingSpeed;

    private float flashingTime;
    private MeshRenderer[] m_MeshRenderers;
    private float m_TargetValue = 0.5f;
    private float value = 1;
    private float timer;
    private bool isStart;
    private void Start()
    {
        m_MeshRenderers = m_VisualModel.GetComponentsInChildren<MeshRenderer>();
    }
    private void Update()
    {
        if (isStart == false) return;

        timer += Time.deltaTime;        

        value = Mathf.MoveTowards(value, m_TargetValue, (m_FlashingSpeed * (timer / flashingTime)) * Time.deltaTime);

        if (value == 0.5)
            m_TargetValue = 1;
        if (value == 1)
            m_TargetValue = 0.5f;

        for (int i = 0; i < m_MeshRenderers.Length; i++)
        {
            Color color = m_MeshRenderers[i].material.color;
            color.a = value;
            m_MeshRenderers[i].material.color = color;
        }
    }

    public void StartAnimation(float flashingTime)
    {
        this.flashingTime = flashingTime;
        isStart = true;
    }
}
