using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;

    private AudioSource audioSource;

    private Button[] uIButtons;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        uIButtons = GetComponentsInChildren<Button>(true);

        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].onClick.AddListener(OnPointerClicked);
        }

    }


    private void OnDestroy()
    {
        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].onClick.RemoveListener(OnPointerClicked);
        }
    }

   
    private void OnPointerClicked()
    {
        audioSource.PlayOneShot(click);
    }
}
