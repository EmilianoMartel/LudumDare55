using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ConfirmLogic _confirm;


    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _changeDayPanel;

    [SerializeField] private int _maxWaveCount = 3;
    [SerializeField] private int _minWizarstoRequest = 6;
    [SerializeField] private int _wizardRequestIncrement = 2;
    [SerializeField] private float _waitFotCharge = 5;

    private int _currentWizarRequest = 0;
    private int _maxWizardRequest = 6;
    private int _currentWaveCount = 0;

    public Action startGameEvent = delegate { };
    public Action<bool> isWinGameEvente = delegate { };

    private void OnEnable()
    {
        _confirm.confirmEvent += HandleRequest;
    }

    private void OnDisable()
    {
        _confirm.confirmEvent -= HandleRequest;
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

    private void HandleCheckRequest(string dungeon, Demon demon, string mage)
    {

    }

    private void HandleRequest()
    {
        _currentWizarRequest++;
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
            isWinGameEvente?.Invoke(true);
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
}
