using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ConfirmLogic _confirm;
    [SerializeField] private WizardRequestLogic _wizardRequest;
    [SerializeField] private DemonCreatorController _demonController;
    [SerializeField] private TemometerLogic _termometer;

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _changeDayPanel;

    [SerializeField] private int _maxWaveCount = 3;
    [SerializeField] private int _minWizarstoRequest = 6;
    [SerializeField] private int _wizardRequestIncrement = 2;
    [SerializeField] private int _maxFail = 3;
    [SerializeField] private float _waitFotCharge = 5;
    [SerializeField] private float _timeResolutionDungeon = 20f;

    private int _currentWizarRequest = 0;
    private int _maxWizardRequest = 6;
    private int _currentWaveCount = 0;
    private int _finalDungeons = 0;


    private int _failDayCount = 0;

    private Demon _actualDemon;
    private Wizards _wizard;
    private Dungeons _dungeon;

    public Action startGameEvent = delegate { };
    public Action newWaveEvent = delegate { };
    [SerializeField] private IsWinOrLoseData _data;
    public Action<Wizards,Dungeons,bool> endDungeonEvent = delegate { };
    public Action<Wizards, Dungeons> startDungeonEvent = delegate { };
    public Action finalDayEvent = delegate { };

    [SerializeField] private string _endSceneName = "GameOverScene";
    [SerializeField] private float _waitForChangeScene = 0.5f;

    private void OnEnable()
    {
        _confirm.confirmEvent += HandleRequest;
        _demonController.electedDemonEvent += HandleActualDemon;
        _demonController.finalTimeEvent += HandleTimeFinal;
        _wizardRequest.wizardRequestEvent += HandleWizardRequest;
        _wizardRequest.allWizardsDeadEvent += HandleAllDead;
        _demonController.allDemonsDieEvent += HandleAllDead;
    }

    private void OnDisable()
    {
        _confirm.confirmEvent -= HandleRequest;
        _demonController.electedDemonEvent -= HandleActualDemon;
        _wizardRequest.wizardRequestEvent -= HandleWizardRequest;
        _demonController.finalTimeEvent -= HandleTimeFinal;
        _wizardRequest.allWizardsDeadEvent -= HandleAllDead;
        _demonController.allDemonsDieEvent -= HandleAllDead;
    }

    private void Awake()
    {
        _changeDayPanel.SetActive(false);
        _maxWizardRequest = _minWizarstoRequest;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(_waitFotCharge);
        startGameEvent?.Invoke();
        _loadingPanel.SetActive(false);
    }

    private IEnumerator HandleCheckRequest(Wizards wizard, Dungeons dungeon, Demon demon)
    {
        startDungeonEvent?.Invoke(wizard,dungeon);
        yield return new WaitForSeconds(_timeResolutionDungeon);
        int randomChance = UnityEngine.Random.Range(0, 100);
        int dungeonPower = dungeon.difficulty;
        int modifierPower = 0;
        if (demon.ShowType().In == dungeon.name)
        {
            modifierPower = demon.ShowType().Effect;
        }

        int alliancePower = wizard.Power + demon.ShowCategoryPower() + modifierPower;
        if (randomChance > (alliancePower / dungeonPower) * 100)
        {
            Debug.Log("fail");
            endDungeonEvent?.Invoke(wizard,dungeon,false);
            
            _failDayCount++;

            _termometer.HandleDamage((float)_failDayCount / (float)_maxFail);
            if (_failDayCount >= _maxFail)
            {
                StartCoroutine(GameOver(false));
            }
        }
        else
        {
            endDungeonEvent?.Invoke(wizard, dungeon, true);
        }
        _finalDungeons++;
    }

    private void HandleRequest()
    {
        _currentWizarRequest++;
        StartCoroutine(HandleCheckRequest(_wizard, _dungeon, _actualDemon));
        if (_currentWizarRequest >= _maxWizardRequest)
        {
            WaveChanger();
        }
    }

    private void WaveChanger()
    {
        _currentWaveCount++;
        _failDayCount = 0;
        if (_currentWaveCount >= _maxWaveCount)
        {
            StartCoroutine(GameOver(true));
        }
        
        StartCoroutine(WaveChange());
    }

    private IEnumerator WaveChange()
    {
        finalDayEvent?.Invoke();
        yield return StartCoroutine(CheckWaveChanger());
        _changeDayPanel.SetActive(true);
        yield return new WaitForSeconds(_waitFotCharge);
        newWaveEvent?.Invoke();
        _finalDungeons = 0;
        _changeDayPanel.SetActive(false);
        _termometer.ResetAmount();
        _currentWizarRequest = 0;
        _maxWizardRequest += _wizardRequestIncrement;
    }

    private IEnumerator CheckWaveChanger()
    {
        yield return new WaitUntil(() => _finalDungeons >= _maxWizardRequest);
    }

    private void HandleWizardRequest(Wizards wizard, Dungeons dungeon)
    {
        _wizard = wizard;
        _dungeon = dungeon;
    }

    private void HandleActualDemon(Demon demon)
    {
        _actualDemon = demon;
    }

    private void HandleTimeFinal(Demon demon)
    {
        _actualDemon = demon;
        HandleRequest();
    }

    private void HandleAllDead()
    {
        StartCoroutine(GameOver(false));
    }

    private IEnumerator GameOver(bool isWining)
    {
        _data.SetBool(isWining);
        yield return new WaitForSeconds(_waitForChangeScene);
        SceneManager.LoadScene(_endSceneName);
    }
}
