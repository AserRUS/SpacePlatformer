using UnityEngine;

[RequireComponent(typeof(Destructible))]
public abstract class Boss : MonoBehaviour
{
    protected Destructible destructible;

    protected virtual void Start()
    {
        destructible = GetComponent<Destructible>();
    }

    public virtual void StartFight() { }

    public virtual void OnPlayerDeath() { }
}
