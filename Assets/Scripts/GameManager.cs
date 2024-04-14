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

    [SerializeField] private HealthManager _healthManager;

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _changeDayPanel;

    [SerializeField] private int _maxWaveCount = 3;
    [SerializeField] private int _minWizarstoRequest = 6;
    [SerializeField] private int _wizardRequestIncrement = 2;
    [SerializeField] private float _waitFotCharge = 5;


    private int _currentWizarRequest = 0;
    private int _maxWizardRequest = 6;
    private int _currentWaveCount = 0;

    private Demon _actualDemon;
    private Wizards _wizard;
    private Dungeons _dungeon;

    public Action startGameEvent = delegate { };
    [SerializeField] private IsWinOrLoseData _data;
    public Action<Wizards> failEvent = delegate { };

    [SerializeField] private string _endSceneName = "GameOverScene";
    [SerializeField] private float _waitForChangeScene = 0.5f;

    private void OnEnable()
    {
        _confirm.confirmEvent += HandleRequest;
        _demonController.electedDemonEvent += HandleActualDemon;
        _wizardRequest.wizardRequestEvent += HandleWizardRequest;
        _wizardRequest.allWizardsDeadEvent += HandleAllDead;
        _demonController.allDemonsDieEvent += HandleAllDead;
    }

    private void OnDisable()
    {
        _confirm.confirmEvent -= HandleRequest;
        _demonController.electedDemonEvent -= HandleActualDemon;
        _wizardRequest.wizardRequestEvent -= HandleWizardRequest;
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

    private void HandleCheckRequest()
    {
        int randomChance = UnityEngine.Random.Range(0, 100);
        int dungeonPower = _dungeon.difficulty;
        int modifierPower = 0;
        if (_actualDemon.ShowType().In == _dungeon.name)
        {
            modifierPower = _actualDemon.ShowType().Effect;
        }

        int alliancePower = _wizard.Power + _actualDemon.ShowCategoryPower() + modifierPower;
        if (randomChance > (alliancePower / dungeonPower) * 100)
        {
            Debug.Log("fail");
            failEvent?.Invoke(_wizard);
            _healthManager.ReceiveDagage(10);
        }
    }

    private void HandleRequest()
    {
        _currentWizarRequest++;
        HandleCheckRequest();
        if (_currentWizarRequest >= _maxWizardRequest)
        {
            WaveChanger();
        }
    }

    private void WaveChanger()
    {
        _currentWaveCount++;
        if (_currentWaveCount >= _maxWaveCount)
        {
            StartCoroutine(GameOver(true));
        }
        _currentWizarRequest = 0;
        _maxWizardRequest += _wizardRequestIncrement;
        _changeDayPanel.SetActive(true);
        StartCoroutine(WaveChange());
    }

    private IEnumerator WaveChange()
    {
        yield return new WaitForSeconds(_waitFotCharge);
        _changeDayPanel.SetActive(false);
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