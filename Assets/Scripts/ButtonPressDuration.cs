using UnityEngine;

[CreateAssetMenu]
public class ButtonPressDuration : ScriptableObject
{
    [SerializeField] private float timeLimitForButtonClamp = 1f;

    [SerializeField] private float minTimeButtonClamp = 0.1f;
    [SerializeField] private float maxTimeButtonClamp = 2f;

    public float TimeLimitForButtonClamp => timeLimitForButtonClamp;
    public float MinTimeButtonClamp => minTimeButtonClamp;
    public float MaxTimeButtonClamp => maxTimeButtonClamp;
}
