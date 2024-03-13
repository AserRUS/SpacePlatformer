using UnityEngine;

public class Player : Destructible
{
    private bool isInvulnerable;

    protected override void Start()
    {
        base.Start();
        isInvulnerable = false;
    }

    public override void RemoveHitpoints(int value, GameObject owner)
    {
        if (isInvulnerable) return;

        base.RemoveHitpoints(value, owner);
    }

    public void SetInvulnerable(bool value)
    {
        isInvulnerable = value;
    }
}
