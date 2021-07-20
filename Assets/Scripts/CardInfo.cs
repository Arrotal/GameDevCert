using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfo : MonoBehaviour
{
    [SerializeField] TMP_Text _nameText, _descriptionText, _unitCost, _level1SP, _level2SP, _level3SP, _spNameText;
    [SerializeField] GameObject _cardImage, _spCard, _activeCard, _skillButton, _level1Panel, _level2Panel, _level3Panel;

    private int _cost;
    private Unit _unitOnCard;
    public void GiveCardInfo(Unit unit )
    {
        _unitOnCard = unit;
        _nameText.text = unit.GetUnitName().ToUpper();
        _descriptionText.text = unit.GetDescription();
        _cost = unit.GetUnitCost();
        _unitCost.text = unit.GetUnitCost().ToString();
    }
    public void UpdateSPCard()
    {
        _spNameText.text = _nameText.text.ToUpper() + " SKILL TREE";
        _level1SP.text = _unitOnCard.GetLevel1SP().ToString();
        _level2SP.text = _unitOnCard.GetLevel2SP().ToString();
        _level3SP.text = _unitOnCard.GetLevel3SP().ToString();
 
    }
    private List<int> _level1Skills = new List<int>(), _level2Skills = new List<int>(), _level3Skills = new List<int>();
    private List<string> _level1SkillNames = new List<string>(), _level2SkillNames = new List<string>(), _level3SkillNames = new List<string>(),
        _level1SkillDescriptions = new List<string>(), _level2SkillDescriptions = new List<string>(), _level3SkillDescriptions = new List<string>();
    private void UpdateSkillTree()
    {
       
            _level1Skills.AddRange(_unitOnCard.GetLevel1Skill());
            _level1SkillNames.AddRange(_unitOnCard.GetLevel1SkillTree());
            _level1SkillDescriptions.AddRange(_unitOnCard.GetLevel1SkillDescription());      
     
            _level2Skills.AddRange(_unitOnCard.GetLevel2Skill());
            _level2SkillNames.AddRange(_unitOnCard.GetLevel2SkillTree());
            _level2SkillDescriptions.AddRange(_unitOnCard.GetLevel2SkillDescription());    

            _level3Skills.AddRange(_unitOnCard.GetLevel3Skill());
            _level3SkillNames.AddRange(_unitOnCard.GetLevel3SkillTree());
            _level3SkillDescriptions.AddRange(_unitOnCard.GetLevel3SkillDescription());
      
    }
    public void ChosenCard()
    {
        if(UnitPicker.Instance.CheckScrapValue(_cost))
        { 
        UnitPicker.Instance.GetChosenCard(_unitOnCard);
            _activeCard.SetActive(false);
            _spCard.SetActive(true);
            UpdateSPCard();
            UpdateSkillTree();
            SetSkillButtons();
        }
    }
    private void SetSkillButtons()
    {
        for (int l = 0; l < _unitOnCard.GetLevel1Skill().Count; l++)
        {
            GameObject skillButton = Instantiate(_skillButton, _level1Panel.transform);
            skillButton.GetComponent<SkillButton>().AddInfo(_level1Skills[l], _level1SkillNames[l], _level1SkillDescriptions[l],_unitOnCard,1,this);
        }
        for (int l = 0; l < _unitOnCard.GetLevel2Skill().Count; l++)
        {
            GameObject skillButton = Instantiate(_skillButton, _level2Panel.transform);
            skillButton.GetComponent<SkillButton>().AddInfo(_level2Skills[l], _level2SkillNames[l], _level2SkillDescriptions[l], _unitOnCard,2, this);
        }
        for (int l = 0; l < _unitOnCard.GetLevel3Skill().Count; l++)
        {
            GameObject skillButton = Instantiate(_skillButton, _level3Panel.transform);
            skillButton.GetComponent<SkillButton>().AddInfo(_level3Skills[l], _level3SkillNames[l], _level3SkillDescriptions[l], _unitOnCard,3, this);
        }
    }
    private void OnDestroy()
    {
        UnitPicker._cardChosen -= UpdateSPCard;
    }
    private void Awake()
    {
        UnitPicker._cardChosen += UpdateSPCard;
        _activeCard.SetActive(true);
        _spCard.SetActive(false);
    }

}
