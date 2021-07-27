using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderSkillTree : SkillTreeBase
{
    private int
        _skill11, _skill12, _skill13,
        _skill21, _skill22,
        _skill31, _skill32;
    override public void SetSP(int skill, Unit skillUnit)
    {
        _unit = skillUnit;
        switch (skill)
        {
            case 11:
                _uniqueSkillID = 1;
                SetShotReduction(10);
                break;
            case 12:
                SetExtraShotChance(10);
                break;
            case 13:
                SetDamageMod(15);                
                break;

            case 21:
                SetDamageMod(-30);
                SetShotSize(-30);
                SetShotAmount(1);
                break;
            case 22:
                SetDamageMod(50);
                SetShotSize(50);
                break;

            case 31:
                SetShotReduction(60);
                SetShotSize(-50);
                break;
            case 32:
                _uniqueSkillID = 1;
                SetShotSize(50);
                break;

        }
    }
   
    
   

  
}
