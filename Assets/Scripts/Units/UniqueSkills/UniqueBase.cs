using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniqueBase : MonoBehaviour
{
    protected int uniqueValue;
    public virtual void SetUniqueSkill(int value)
    {
        uniqueValue = value;
    }

    public virtual void DoUniqueThingOnShot(PlayerShotMovement shot)
    { 
    
    }
}
