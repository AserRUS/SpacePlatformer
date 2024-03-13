using UnityEngine;

[CreateAssetMenu]
public class ShieldPropertes : ScriptableObject
{
    [SerializeField] private int requiredEnergyInPeroidOfTime;
    [SerializeField] private int hitPointsInPeroidOfTime;

    public int RequiredEnergyInPeroidOfTime => requiredEnergyInPeroidOfTime;
    public int HitPointsInPeroidOfTime => hitPointsInPeroidOfTime;
}
