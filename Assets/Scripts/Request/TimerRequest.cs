using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TimerRequest : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _cdTimeRequest = 30f;
    [SerializeField] private Image _timerRadialImage;

    private float _actualTime = 0;
    private bool _activeTimer = false;

    private void Update()
    {
        if (!_activeTimer)
            return;

        _actualTime -= Time.deltaTime;
        if (_actualTime <= 0)
        {
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
}
