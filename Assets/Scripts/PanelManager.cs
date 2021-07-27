using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public void DisableButtonsInChildren()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.GetComponent<SkillButton>().SetButtonStatus(false);
        }
    }
}
