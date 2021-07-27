using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class SkillTreeBase: MonoBehaviour
{
    public abstract void SetSP(int skill,Unit skillUnit);
    [SerializeField] protected int _ID;
    [SerializeField] protected GameObject _unitManager;
    public int ReturnUnitID() { return _ID; }
    protected Unit _unit;
    protected int _uniqueSkillID;
    protected void SetShotSize(float percentage)
    {
        _unit.SetShotSize(percentage);
    }
    protected void SetShotAmount(int amount)
    {
        _unit.SetShotAmount(amount);
    }
    protected void SetShotReduction(int amount)
    {

        _unit.SetShotIntervelModifier(amount);
    }
    protected void SetDamageMod(float amount)
    {
        _unit.SetDamageModifier(amount);
    }
    protected void SetExtraShotChance(float amount)
    {
        _unit.ExtraShotMod(amount);
    }
    public int GetUniqueSkill()
    {
        return _uniqueSkillID;

    }
}
