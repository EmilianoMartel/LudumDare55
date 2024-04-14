using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardRequestLogic : MonoBehaviour
{
    [SerializeField] private JsonDataSo _dataSo;
    [SerializeField] private RequestWizardView _view;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ConfirmLogic _confirmLogic;

    private List<Wizards> _activeWizards;
    private List<Wizards> _deadWizards = new List<Wizards>();

    public Action<Wizards, Dungeons> wizardRequestEvent = delegate { };
    public Action allWizardsDeadEvent = delegate { };

    private void OnEnable()
    {
        _gameManager.startGameEvent += RandomWizardRequest;
        _gameManager.failEvent += HandleWizardDead;
        _confirmLogic.confirmEvent += RandomWizardRequest;
    }

    private void OnDisable()
    {
        _gameManager.startGameEvent -= RandomWizardRequest;
        _gameManager.failEvent -= HandleWizardDead;
        _confirmLogic.confirmEvent -= RandomWizardRequest;
    }

    private void Awake()
    {
        StartCoroutine(WaitForData());
    }

    private IEnumerator WaitForData()
    {
        yield return _dataSo.wizardsNameList;
        _activeWizards = _dataSo.wizardsNameList;
    }

    private void RandomWizardRequest()
    {
        int indexWizard = UnityEngine.Random.Range(0, _activeWizards.Count);
        int indexDungeon = UnityEngine.Random.Range(0, _dataSo.dungeonList.Count);
        wizardRequestEvent?.Invoke(_activeWizards[indexWizard], _dataSo.dungeonList[indexDungeon]);
        _view.ReceiveDungeonData(_activeWizards[indexWizard].Name, _dataSo.dungeonList[indexDungeon].name);
    }

    private void HandleWizardDead(Wizards wizard)
    {
        if (_activeWizards.Contains(wizard))
        {
            _activeWizards.Remove(wizard);
            _deadWizards.Add(wizard);
        }
        if (_activeWizards.Count == 0)
        {
            allWizardsDeadEvent?.Invoke();
        }
    }
}