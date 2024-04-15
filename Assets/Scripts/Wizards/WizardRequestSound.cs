using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardRequestSound : MonoBehaviour
{
    [SerializeField] private WizardRequestLogic _wizardRequest;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TimerRequest _timer;

    [SerializeField] private AudioSource _wizardRequestSound;
    [SerializeField] private AudioSource _wizardDieSound;
    [SerializeField] private AudioSource _winWizard;
    [SerializeField] private AudioSource _discartWizard;


    private void OnEnable()
    {
        _wizardRequest.wizardRequestEvent += HandleSound;
        _gameManager.endDungeonEvent += HandleDieSound;
        _timer.endTimerEvent += DiscartWizardSound;
    }

    private void OnDisable()
    {
        _wizardRequest.wizardRequestEvent -= HandleSound;
        _gameManager.endDungeonEvent -= HandleDieSound;
        _timer.endTimerEvent -= DiscartWizardSound;
    }

    private void HandleSound(Wizards wizard, Dungeons dungeon)
    {
        _wizardRequestSound.Play();
    }

    private void HandleDieSound(Wizards w, Dungeons s, bool win)
    {
        if (win)
        {
            _winWizard.Play();
        }
        else
        {
            _wizardDieSound.Play();
        }
    }

    private void DiscartWizardSound()
    {
        _discartWizard.Play();
    }
}
