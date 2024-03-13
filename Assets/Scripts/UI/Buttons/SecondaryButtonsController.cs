using UnityEngine;

public class SecondaryButtonsController : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private InputControl inputControl;
    [SerializeField] private New_UIClampingButton shieldButton;
    [SerializeField] private New_UIClampingButton attackButton;
    [SerializeField] private UIImageChangingTransparency shieldButtonImage;
    [SerializeField] private UIImageChangingTransparency attackButtonImage;

    [Header("ScriptableObjects")]
    [SerializeField] private ButtonPressDuration buttonPressDuration;
    [SerializeField] private ShieldPropertes shieldPropertes;

    private Player player;
    private Storage energyStorage;
    private Storage cartridgeStorage;
    private bool isAttackEnabled = true;
    private bool isShieldEnabled = true;

    private void Start()
    {
        SetPlayer();
    }

    private void CheckCartridgeStorage(int value) { }

    private void SetPlayer()
    {
        player = playerSpawner.GetPlayer();
        FindStorages(player);
        player.DeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        player.DeathEvent -= OnPlayerDeath;
        energyStorage.StorageChangeEvent -= CheckEnergyStorage;
        cartridgeStorage.StorageChangeEvent -= CheckEnergyStorage;
        SetPlayer();
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
}
