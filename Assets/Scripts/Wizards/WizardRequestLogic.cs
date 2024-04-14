using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WizardRequestLogic : MonoBehaviour
{
    [SerializeField] private JsonDataSo _dataSo;
    [SerializeField] private RequestWizardView _view;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ConfirmLogic _confirmLogic;
    [SerializeField] private TimerRequest _timeRequest;
    [SerializeField] private int _maxDungeonRequest = 3;
    [SerializeField] private float _waitForNewRequest = 30f;

    private int _activeQuest = 1;

    private List<Wizards> _activeWizards;
    private List<Wizards> _inactiveWizards = new List<Wizards>();

    private List<Dungeons> _activeDungeons;
    private List<Dungeons> _waitDungeons = new List<Dungeons>();

    public Action<Wizards, Dungeons> wizardRequestEvent = delegate { };
    public Action allWizardsDeadEvent = delegate { };
    public Action noneRequestEvent = delegate { };
    public Action reactiveRequestEvent = delegate { };

    private void OnEnable()
    {
        _gameManager.startGameEvent += RandomWizardRequest;
        _gameManager.endDungeonEvent += HandleEndResolution;
        _gameManager.startDungeonEvent += HandleWaitResolution;
        _gameManager.finalDayEvent += HandleFinalDay;
        _confirmLogic.confirmEvent += RandomWizardRequest;
        _timeRequest.endTimerEvent += RandomWizardRequest;
    }

    private void OnDisable()
    {
        _gameManager.startGameEvent -= RandomWizardRequest;
        _gameManager.endDungeonEvent -= HandleEndResolution;
        _gameManager.startDungeonEvent -= HandleWaitResolution;
        _gameManager.finalDayEvent -= HandleFinalDay;
        _confirmLogic.confirmEvent -= RandomWizardRequest;
        _timeRequest.endTimerEvent -= RandomWizardRequest;
    }

    private void Awake()
    {
        StartCoroutine(WaitForData());
    }

    private IEnumerator WaitForData()
    {
        yield return _dataSo.wizardsNameList;
        _activeWizards = _dataSo.wizardsNameList;
        _activeDungeons = _dataSo.dungeonList;
    }

    private void RandomWizardRequest()
    {
        int indexWizard = UnityEngine.Random.Range(0, _activeWizards.Count);
        int indexDungeon = UnityEngine.Random.Range(0, _activeDungeons.Count);
        wizardRequestEvent?.Invoke(_activeWizards[indexWizard], _dataSo.dungeonList[indexDungeon]);
        _view.ReceiveDungeonData(_activeWizards[indexWizard].Name, _dataSo.dungeonList[indexDungeon].name);
        if (_activeQuest >= _maxDungeonRequest)
        {
            _view.ReciveNoneData();
            noneRequestEvent?.Invoke();
            StartCoroutine(WaitForRequest());
            return;
        }
    }

    private IEnumerator WaitForRequest()
    {
        yield return new WaitForSeconds(_waitForNewRequest);
        reactiveRequestEvent?.Invoke();
        RandomWizardRequest();
    }

    private void HandleWaitResolution(Wizards wizard, Dungeons dungeon)
    {
        _activeQuest++;
        if (_activeWizards.Contains(wizard))
        {
            _inactiveWizards.Add(wizard);
            _activeWizards.Remove(wizard);
        }
        if (_waitDungeons.Contains(dungeon))
        {
            _waitDungeons.Add(dungeon);
            _activeDungeons.Remove(dungeon);
        }
    }

    private void HandleEndResolution(Wizards wizard, Dungeons dungeon, bool isWizardWin)
    {
        _activeQuest--;
        if (isWizardWin)
        {
            if (_inactiveWizards.Contains(wizard))
            {
                _inactiveWizards.Remove(wizard);
                _activeWizards.Add(wizard);
            }
            if (_waitDungeons.Contains(dungeon))
            {
                _waitDungeons.Remove(dungeon);
                _activeDungeons.Add(dungeon);
            }
        }
        else
        {
            if (_waitDungeons.Contains(dungeon))
            {
                _waitDungeons.Remove(dungeon);
                _activeDungeons.Add(dungeon);
            }
        }
        if (_activeWizards.Count == 0)
        {
            allWizardsDeadEvent?.Invoke();
        }
    }

    private void HandleFinalDay()
    {
        _view.WaitForEndDay();
    }
}