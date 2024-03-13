using UnityEngine;


public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_PlayerMovement;
   
    [SerializeField] private AudioSource m_MovementAudioSource;
    [SerializeField] private float m_SoundAttenuationRate;
    private void Update()
    {        
        SoundUpdate();
    }

    private void SoundUpdate()
    {       
        if (m_PlayerMovement.IsGround == true)
        {
            m_MovementAudioSource.volume = Mathf.Abs(m_PlayerMovement.Velocity.x) / m_PlayerMovement.MaxSpeed;
        }
        else
        {
            m_MovementAudioSource.volume = Mathf.MoveTowards(m_MovementAudioSource.volume, 0, m_SoundAttenuationRate * Time.deltaTime);
        }
        
    }
    

}
