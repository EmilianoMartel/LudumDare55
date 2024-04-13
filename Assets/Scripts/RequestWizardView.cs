using System;
using UnityEngine;
using TMPro;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class RequestWizardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _wizardName;
    [SerializeField] private TMP_Text _dungeonNameText;

    public class DungeonData
    {
        public string name { get; set; }
        public Difficulty difficulty { get; set; }
    }

    public void ReceiveDungeonData(string wizardName, string dungeonName)
    {
        // muestro los datos recibidos del json en UI
        _dungeonNameText.text = dungeonName;
        _wizardName.text = wizardName;
    }
}
