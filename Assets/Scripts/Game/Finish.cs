using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    [SerializeField] private InputControl m_InputControl;
    [SerializeField] private Pauser m_Pauser;
 
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();

        if (player != null)
        {
            m_InputControl.InputControlEnabled(false);
            m_InputControl.Stop(true);
            LevelProgress.Instance.LevelFinished();
            m_Pauser.SetFinish();
        }
    }

    
}
