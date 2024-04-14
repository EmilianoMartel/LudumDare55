using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private List<PopUpLogic> _popUpList = new List<PopUpLogic>();
    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.endDungeonEvent += HandleResolution;
    }

    private void OnDisable()
    {
        _gameManager.endDungeonEvent -= HandleResolution;
    }

    private void HandleResolution(Wizards wizard, Dungeons dungeon, bool isWizardWin)
    {
        for (int i = 0; i < _popUpList.Count; i++)
        {
            if (!_popUpList[i].isActive)
            {
                _popUpList[i].HandleResolution(wizard, isWizardWin);
                return;
            }
        }
    }
}
