using System.Collections;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    [SerializeField] private EvilDoctorController evilDoctor;
    [SerializeField] private Transform teleportPoint;

    [Header("Shield")]
    [SerializeField] private GameObject shield;
    [SerializeField] private float shieldLifeTime;

    [Header("Lightning")]
    [SerializeField] private GameObject middleLightning;
    [SerializeField] private GameObject[] sideLightning;

    private DoorAnimationController animationController;
    private bool isShieldActive = false;
    private bool isShieldReady = true;

    public Transform TeleportPoint => teleportPoint;

    private void Start()
    {
        evilDoctor.OnDamageReceivedForShield += OnEvilDoctorDamageReceived;

        animationController = shield.GetComponent<DoorAnimationController>();
        animationController.OnClossingEnd += OnClossingEnd;
        animationController.OnMiddlePath += OnMiddlePath;
        shield.SetActive(false);
    }

    public void OnEvilDoctorDamageReceived()
    {
        if (!isShieldActive) return;
        if (!isShieldReady) return;

        OpenShield();
    }

    private void OpenShield()
    {
        isShieldReady = false;
        shield.SetActive(true);
        animationController.OpenDoor();
        middleLightning.SetActive(false);
        StartCoroutine(CloseShieldAfterTime(shieldLifeTime));
    }

    private void CloseShield()
    {
        animationController.CloseDoor();
    }

    private IEnumerator CloseShieldAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CloseShield();
    }

    public void SetShieldActive(bool value)
    {
        isShieldActive = value;

        if (isShieldActive)
            isShieldReady = true;
    }

    public void OnClossingEnd()
    {
        middleLightning.SetActive(true);
        shield.SetActive(false);
    }

    public void OnMiddlePath()
    {
        foreach (GameObject go in sideLightning)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}
