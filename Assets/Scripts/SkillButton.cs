using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _skillLevelCurrentText,_skilLevelTotalText, _skillDes, _skillName;
    [SerializeField] private GameObject _skillTextPanel;
    [SerializeField] private Button _levelUpButton;
    private int _skillLevelMax, _skillLevelCurrent=0;
    public void SetDescription(string description) { _skillDes.text = description;}
 
    public void SetSkillMax(int max) { _skillLevelMax = max; }
    public void CheckIfSkillGettable()
    {
       
        if (!_cardUnit.CheckSP(_tierSP)&& _skillLevelCurrent<_skillLevelMax)
        {
            SetButtonStatus(false);
        }
        else
        {
            SetButtonStatus(true);
        }
        
    }

    private void Update()
    {
        CheckIfSkillGettable();
    }
    private Unit _cardUnit;
    public int _tierSP, _skillPosandTier;
    private CardInfo _card;
    public void AddInfo(int skillMax, string SkillName, string SkillDescription, Unit unit, int tier,CardInfo card, int skillPos)
    {
        _skillLevelMax = skillMax;
        _skilLevelTotalText.text = _skillLevelMax.ToString();
        _skillDes.text = SkillDescription.ToString();
        _skillName.text = SkillName.ToString();
        _skillLevelCurrentText.text = _skillLevelCurrent.ToString();
        _cardUnit = unit;
        _tierSP = tier;
        _card = card;
        _skillPosandTier = _tierSP*10 + skillPos;
        
    }

    public void SetButtonStatus(bool state)
    {
            _levelUpButton.interactable = state;   
   
    }
    private void SetSkillLevel() 
    {
        if (_skillLevelCurrent < _skillLevelMax)
        {
            
            _skillLevelCurrent++;
            _skillLevelCurrentText.text = _skillLevelCurrent.ToString();
            _cardUnit.ConsumeSP(_tierSP);
            _card.UpdateSPCard();
            if(!_cardUnit.CheckSP(_tierSP))
             this.GetComponentInParent<PanelManager>().DisableButtonsInChildren();
            if (_skillLevelCurrent == _skillLevelMax)
            {
                SetButtonStatus(false);
            }
            UnitPicker.Instance.SetSkill(_cardUnit, _skillPosandTier);
        }
    }


    public void CheckifSkillPurchaseable()
    {
        if (_cardUnit.CheckSP(_tierSP))
        {
            SetSkillLevel();
        }
    }

}
