using UnityEngine;

public class ObjectHealth : Health
{
    public override void OtherDamageLogic(float damage)
    {

    }

    public override void HandleDeath()
    {
        Destroy(gameObject);
    }

    public override void OtherHealthLogic()
    {

    }
}
