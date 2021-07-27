using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderUnique : UniqueBase
{

    public override void SetUniqueSkill(int value)
    {
        uniqueValue = value;

    }

    public override void DoUniqueThingOnShot(PlayerShotMovement shot)
    {
        if (uniqueValue == 1)
            shot.DestroyOnHit(false);
        else
            shot.DestroyOnHit(true);
    }
}
