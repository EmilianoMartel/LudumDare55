using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TimerRequest : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private WizardRequestLogic _wizardRequestLogic;
    [SerializeField] private float _cdTimeRequest = 30f;
    [SerializeField] private Image _timerRadialImage;

    private float _actualTime = 0;
    private bool _activeTimer = false;

    public Action endTimerEvent;

    private void OnEnable()
    {
        _gameManager.startGameEvent += HandleStartTimer;
        _gameManager.newWaveEvent += HandleStartTimer;
        _wizardRequestLogic.wizardRequestEvent += HandelRestartTimer;
        _wizardRequestLogic.noneRequestEvent += HandlePauseTimer;
    }

    private void OnDisable()
    {
        _gameManager.startGameEvent -= HandleStartTimer;
        _gameManager.newWaveEvent -= HandleStartTimer;
        _wizardRequestLogic.wizardRequestEvent -= HandelRestartTimer;
        _wizardRequestLogic.noneRequestEvent -= HandlePauseTimer;
    }

    private void Update()
    {
        if (!_activeTimer)
            return;

        _actualTime -= Time.deltaTime;
        if (_actualTime <= 0)
        {
            endTimerEvent?.Invoke();
            _activeTimer = false;
            return;
        }
        _timerRadialImage.fillAmount = _actualTime / _cdTimeRequest;
    }

    private void HandleStartTimer()
    {
        _activeTimer = true;
        _actualTime = _cdTimeRequest;
    }

    private void HandelRestartTimer(Wizards wizard, Dungeons dungeon)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        _actualTime = _cdTimeRequest;
        _activeTimer = true;
    }

    private void HandlePauseTimer()
    {
        _activeTimer = false;
        _timerRadialImage.fillAmount = 1;
    }
}
