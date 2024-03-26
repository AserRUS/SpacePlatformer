using System.Collections;
using UnityEngine;

public class SecondaryButtonsController : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private InputControl inputControl;

    [Header("Shield button")]
    [SerializeField] private New_UIClampingButton shieldButton;
    [SerializeField] private UIImageChangingTransparency shieldButtonImage;

    [Header("Attack button")]
    [SerializeField] private New_UIClampingButton attackButton;
    [SerializeField] private UIImageChangingTransparency attackButtonImage;
    [SerializeField] private float timeButtonEnabledAfterJump = 0.1f;

    [Header("ScriptableObjects")]
    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private ShieldPropertes shieldPropertes;

    private Player player;
    private PlayerMovement playerMovement;
    private Storage energyStorage;
    private Storage cartridgeStorage;
    private bool isAttackEnabled = true;
    private bool isShieldEnabled = true;
    private Coroutine enableAttackButtonTimerCoroutine;

    private void Start()
    {
        playerSpawner.PlayerSpawned += SetVariables;

        if (player == null)
            SetVariables(playerSpawner.GetPlayer());
    }

    private void OnDestroy()
    {
        playerSpawner.PlayerSpawned -= SetVariables;
    }

    private void CheckCartridgeStorage(int value) { }

    private void SetVariables(Player player)
    {
        this.player = player;
        playerMovement = this.player.GetComponent<PlayerMovement>();
        playerMovement.OnJumpEvent += DisableAttack;
        playerMovement.OnRotationEvent += DisableAttack;
        FindStorages(this.player);
        this.player.DeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        player.DeathEvent -= OnPlayerDeath;
        energyStorage.StorageChangeEvent -= CheckEnergyStorage;
        cartridgeStorage.StorageChangeEvent -= CheckEnergyStorage;
        playerMovement.OnJumpEvent -= DisableAttack;
        playerMovement.OnRotationEvent -= DisableAttack;
    }

    private void FindStorages(Player player)
    {
        Storage[] storages = player.GetComponents<Storage>();

        if (storages != null)
        {
            foreach (Storage storage in storages)
            {
                if (storage.StorageType == StorageType.Energy)
                {
                    energyStorage = storage;
                    energyStorage.StorageChangeEvent += CheckEnergyStorage;
                }

                if (storage.StorageType == StorageType.Energy)
                {
                    cartridgeStorage = storage;
                    cartridgeStorage.StorageChangeEvent += CheckCartridgeStorage;
                }
            }
        }
    }
    private void CheckEnergyStorage(int value)
    {
        if (value < shieldPropertes.RequiredEnergyInPeroidOfTime)
        {
            DisableShield();
        }
        else if (!isShieldEnabled)
        {
            EnableShield();
        }
    }

    private void DisableShield()
    {
        isShieldEnabled = false;
        shieldButtonImage.AddTransparency();
        shieldButton.SetUnInteractable();
        inputControl.ShieldEnabled(false);
    }

    private void EnableShield()
    {
        isShieldEnabled = true;
        shieldButtonImage.RemoveTransparency();
        shieldButton.SetInteractable();
        inputControl.ShieldEnabled(true);
    }

    private void DisableAttack()
    {
        isAttackEnabled = false;
        attackButtonImage.AddTransparency();
        attackButton.SetUnInteractable();
        inputControl.AttackEnabled(false);

        if (enableAttackButtonTimerCoroutine != null)
            StopCoroutine(enableAttackButtonTimerCoroutine);
        enableAttackButtonTimerCoroutine = StartCoroutine(EnableAttackButtonTimer(timeButtonEnabledAfterJump));
    }

    private void EnableAttack()
    {
        isAttackEnabled = true;
        attackButtonImage.RemoveTransparency();
        attackButton.SetInteractable();
        inputControl.AttackEnabled(true);
    }

    private IEnumerator EnableAttackButtonTimer(float time)
    {
        yield return new WaitForSeconds(time);
        EnableAttack();
    }
}
