using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] private string _name, _description;
    [SerializeField] private int _shots, _shotType, _unitCost,_ID;
    [SerializeField] private List<int>_unitType, _level1Skills,_level2Skills, _level3Skills;
    private float _shotIntervel, _shotDamage,_shotSpeed;
    private float _shotIntervelModifier, _shotDamageModifier, _shotSpeedModifier,_shotMod=1, _shotSize=1;
    [SerializeField] private GameObject _unit;
    [SerializeField] private List<string> 
        _level1SkillDescription, _level2SkillDescription, _level3SkillDescription,
        _level1SkillName, _level2SkillName, _level3SkillName;
    private int _level1SP=0, _level2SP=0, _level3SP=0, _SP=0;
    private bool _notLevel3 =true;

    [SerializeField] private List<float> _levelShotDamage, _levelShotIntervel, _levelShotSpeed, _levelShotSize;
    private int _level=0;
    private float _shotSizeModifier = 1;

    public string GetUnitName() { return _name; }

    public List<string> GetLevel1SkillTree() { return _level1SkillName; }
    public List<string> GetLevel2SkillTree() { return _level2SkillName; }
    public List<string> GetLevel3SkillTree() { return _level3SkillName; }

    public List<string> GetLevel1SkillDescription() { return _level1SkillDescription; }
    public List<string> GetLevel2SkillDescription() { return _level2SkillDescription; }
    public List<string> GetLevel3SkillDescription() { return _level3SkillDescription; }

    public List<int> GetLevel1Skill() { return _level1Skills; }
    public List<int> GetLevel2Skill() { return _level2Skills; }
    public List<int> GetLevel3Skill() { return _level3Skills; }

    public float GetUnitShotAmount() { return _shots* ((100+_shotMod)/100); }
    public int GetUnitShotType() { return _shotType; }
    public List<int> GetUnitType() { return _unitType; }
    public float GetShotIntervel() { return (_shotIntervel * (100 - _shotIntervelModifier) / 100); }
    public string GetDescription(){ return _description; }
    public GameObject GetUnitModel() { return _unit; }
    public float GetShotDamage() { return _shotDamage*((100+_shotDamageModifier)/100); }
    public float GetShotSpeed() { return _shotSpeed* ((100+_shotSpeedModifier)/100); }
    public int GetUnitCost() { return _unitCost; }
    public int GetLevel1SP() { return _level1SP; }
    public int GetLevel2SP() { return _level2SP; }
    public int GetLevel3SP() { return _level3SP; }
    public bool NotLevel3() { return _notLevel3; }
    public int CheckID() { return _ID; }
    public void SetSP() 
    {
        _SP++;
        
        if (_SP % 3 == 0)
        {
            if (_SP % 9== 0)
            {
                _level3SP++;
                _notLevel3 = false;
                SetLevel(3);
            }
            else
            {
                _level2SP++;
                if (_level == 1)
                    SetLevel(2);
            }
        }
        else
        {
            if (_level == 0)
                SetLevel(1);
            _level1SP++;
        }
    }

    private void SetLevel(int level)
    {
        _level = level;
        _shotDamage = _levelShotDamage[_level-1];
        _shotIntervel = _levelShotIntervel[_level - 1];
        _shotSize = _levelShotSize[_level - 1];
        _shotSpeed = _levelShotSpeed[_level - 1];
    
    }
    public void SetShotAmount(int amount)
    {
        _shots += amount;
    }
    public void SetShotSize(float percentage)
    {
        _shotSizeModifier += percentage/100;
    }
    public float GetShotSize()
    {
        return _shotSize*_shotSizeModifier;
    }
    public void SetShotIntervelModifier(float shotIntervelMod)
    {
        _shotIntervelModifier += shotIntervelMod;
    }
    public void SetDamageModifier(float damageMod)
    {
        _shotDamageModifier += damageMod;
    }
    public void SetSpeedModifier(float speedMod)
    {
        _shotSpeedModifier += speedMod;
    }
    public void ExtraShotMod(float amount)
    {
        _shotMod += amount;
    }

    public void ConsumeSP(int level)
    {
        switch (level)
        {
            case 1:
                _level1SP--;
                break;
            case 2:
                _level2SP--;
                break;
            case 3:
                _level3SP--;
                break;
        }
    }
    public bool CheckSP(int level)
    {
        switch (level)
        {

            case 0:
                if(_SP>1)
                    return true;                
                return false;
            case 1:
                if (_level1SP > 0)
                    return true;
                return false;

            case 2:
                if (_level2SP > 0)
                    return true;
                return false;
            case 3:
                if (_level3SP > 0)
                    return true;
                return false;
        }
        return false ;
    }

    public float GetUnitSize()
    {
        return _level;
    }
}
