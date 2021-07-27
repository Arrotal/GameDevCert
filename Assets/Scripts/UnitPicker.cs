using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnitPicker : MonoBehaviour
{
    private static UnitPicker _instance;
    public static UnitPicker Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Unit Picker Not created");
            }
            return _instance;
        }
    }
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<GameObject> _skillTreeObject;
    private List<SkillTreeBase> _skillTrees = new List<SkillTreeBase>();
    [SerializeField] private GameObject _card, _panel, _unitManager;
    [SerializeField] private Canvas _unitPickerCanvas,_levelCompleteCanvas;
    [SerializeField] private TMP_Text _scrapCount,_waveText, _waveCompleteText,_interestText,_totalText;

    private List<Unit> _unitsToDisplay= new List<Unit>();
    private Unit _tempUnit;
    private int _randomUnit;
    public void ShowPossibleUnits(int howMany)
    {
        _unitsToDisplay.Clear();
        foreach (Transform child in _panel.transform)
        {
           Destroy(child.gameObject);
        }
        for (int l = 0; l <= howMany; l++)
        {
            _randomUnit = Random.Range(0, _usableUnits.Count);
            _tempUnit = _usableUnits[_randomUnit];
            if (_tempUnit.NotLevel3())
            {
                _unitsToDisplay.Add(_tempUnit);
                GameObject newCard = Instantiate(_card, _panel.transform);
                newCard.GetComponent<CardInfo>().GiveCardInfo(_tempUnit);
            }
            else
            {
                _usableUnits.RemoveAt(_randomUnit);
            }
            
        }   
    }

    private List<Unit> _selectedUnits= new List<Unit>();
    private float _rotation;
    private Vector3 _rotateVector = new Vector3(0, 0, 1), _rotatePos = new Vector3(-1f,0,0);
    private bool _alreadyHave = false;

    public delegate void CardChosen();
    public static event CardChosen _cardChosen;

    public void GetChosenCard(Unit chosen)
    {
        _alreadyHave = false;
        CheckIfAlreadyHave(chosen);
        if (!_alreadyHave)
        {
            _selectedUnits.Add(chosen);
            
        }
        _cardChosen();
    }
    private OrbitalMovement _unitmovement;
    private int _unitID;
    private void SetUnitLocation()
    {
        foreach (Transform child in _unitManager.transform)
        {
            Destroy(child.gameObject);
        }
        _rotation = 360 / _selectedUnits.Count;
        float unitRotation;
        int loop = 0, _skilltreeloop = 0;
        foreach (Unit unit in _selectedUnits)
        {
            _skilltreeloop = 0;
            foreach (SkillTreeBase skillTree in _skillTrees)
            {
                if (unit.CheckID() == skillTree.ReturnUnitID())
                {
                    _unitID = _skilltreeloop;
                }
                _skilltreeloop++;
            }
            GameObject newOrbital = Instantiate(_selectedUnits[loop].GetUnitModel(), _unitManager.transform);
            _unitmovement = newOrbital.GetComponent<OrbitalMovement>();
            _unitmovement.SetBetweenShot( _selectedUnits[loop].GetShotIntervel());
            _unitmovement.SetShotDamage(unit.GetShotDamage());
            _unitmovement.SetShotSpeed(unit.GetShotSpeed());
            _unitmovement.SetShotAmount(unit.GetUnitShotAmount());
            _unitmovement.SetShotSize(unit.GetShotSize());
            _unitmovement.SetScale(unit.GetUnitSize());
            newOrbital.GetComponent<UniqueBase>().SetUniqueSkill(_skillTrees[_unitID].GetUniqueSkill());
            loop++;
            unitRotation = _rotation * loop;
            newOrbital.transform.position += _rotatePos;
            newOrbital.transform.RotateAround(_unitManager.transform.position, _rotateVector, unitRotation);
           
        }
    }

    private void CheckIfAlreadyHave(Unit chosen)
    {
        foreach (Unit item in _usableUnits)
        {
            if (chosen.CheckID() == item.CheckID())
            {
                item.SetSP();
                if (item.CheckSP(0))
                {
                    _alreadyHave = true;
                    
                }
                
                
            }
        }
    }

    public delegate void StartWaves(int level, int wave);
    public static event StartWaves startWaves;
    public void StartWave()
    {
        SetUnitLocation();
        _unitPickerCanvas.enabled = false;
            startWaves(_currentLevel,_currentWave);
        Time.timeScale = 1;
    }
    private bool buyable=false;
    public bool CheckScrapValue(int cost)
    {
        if ((_scrapValue - cost) >= 0)
        {
            buyable = true;
            _scrapValue -= cost;
            _scrapCount.text = _scrapValue.ToString();
        }
        else {
            buyable = false;
        }
        return buyable;
    }
    private int _scrapValue;
    private void Awake()
    {
        CopyUnits();
        _currentLevel = 1;
        _currentWave = 0;
        _scrapValue = 5;
        _instance=this;
        Time.timeScale = 0;
        OpenPicker();
        CopySkillTrees();
    }

    private void CopySkillTrees()
    {
        foreach (GameObject skilltree in _skillTreeObject)
        {
            _skillTrees.Add(skilltree.GetComponent<SkillTreeBase>());
        }
    }

    private List<Unit> _usableUnits = new List<Unit>();
    
    private void CopyUnits()
    {
        _usableUnits.Clear();
        foreach (Unit item in _units)
        {
            _tempUnit = Instantiate(item);
            _usableUnits.Add(_tempUnit);
        }
    }
    private int _currentLevel, _currentWave;
    public void OpenPicker()
    {
        _unitPickerCanvas.enabled = true;
        _levelCompleteCanvas.enabled = false;
        if (_currentWave < 5)
            _currentWave++;
        else
        {
            //SpawnPowerUp
            if (_currentLevel < 6)
                _currentLevel++;
            else
            {
                //EndGame 
            }
        }
        _waveText.text = _currentLevel + "-" + _currentWave;

        _scrapCount.text = _scrapValue.ToString();
        ShowPossibleUnits(2);
    }
    public delegate void CompletedWave();
    public static event CompletedWave completedWave;


    public void OpenWaveComplete()
    {
        completedWave();
        StartCoroutine(WaveComplete());
    }
    IEnumerator WaveComplete()
    {
        //WaveComplete Canvas
        //new ScrapTotal    
        yield return new WaitForSeconds(2f);
       
        _levelCompleteCanvas.enabled = true;
        EndOfWaveCalc();
        Time.timeScale = 0;
    }
    private int _waveClearScrap, _interestScrap;
    private void EndOfWaveCalc()
    {
        _waveClearScrap = 5 * _currentLevel;
        _interestScrap = _scrapValue / 10;
        _waveCompleteText.text = _scrapValue.ToString() + "+" + _waveClearScrap.ToString();
        _scrapValue += _waveClearScrap;
        _interestText.text = _scrapValue.ToString() + "+" + _interestScrap.ToString();
        _scrapValue += _interestScrap;
        _totalText.text = _scrapValue.ToString();
    }
    public int GetCurrentLevel(){ return _currentLevel;}
    public int GetCurrentWave() { return _currentWave; }

    public void SetSkill(Unit unit, int skill)
    {
        
        switch (unit.CheckID())
        {
            case 100:
                _skillTrees[0].SetSP(skill, unit);
                break;

        }
    }
}
