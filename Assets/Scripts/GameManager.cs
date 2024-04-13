using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ConfirmLogic _confirm;
    [SerializeField] private WizardRequestLogic _wizardRequest;
    [SerializeField] private DemonCreatorController _demonController;

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
    public Action<bool> isWinGameEvent = delegate { };
    public Action<Wizards, Demon> failEvent = delegate { };

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
        int alliancePower = _wizard.Power + _actualDemon.ShowCategoryPower();
        int dungeonPower = _dungeon.difficulty;
        if (randomChance > (alliancePower / dungeonPower) * 100)
        {
            failEvent?.Invoke(_wizard, _actualDemon);
            //MANDAR EL DAÑO AL TERMOMETRO
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
            isWinGameEvent?.Invoke(true);
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
        isWinGameEvent?.Invoke(false);
    }
}
