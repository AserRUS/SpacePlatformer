using UnityEngine;

[CreateAssetMenu]
public class ButtonPressDuration : ScriptableObject
{
    [SerializeField] private float timeLimitForButtonClamp = 1f;

    public float TimeLimitForButtonClamp => timeLimitForButtonClamp;
}
