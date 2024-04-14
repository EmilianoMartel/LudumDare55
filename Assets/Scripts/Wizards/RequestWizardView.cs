using System;
using UnityEngine;
using TMPro;

public class RequestWizardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _wizardName;
    [SerializeField] private TMP_Text _dungeonNameText;

    public void ReceiveDungeonData(string wizardName, string dungeonName)
    {
        _dungeonNameText.text = dungeonName;
        _wizardName.text = wizardName;
    }
}
