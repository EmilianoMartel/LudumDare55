using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [Tooltip("sounds must be put in order")]
    [SerializeField] private List<AudioSource> _audio = new List<AudioSource>();
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PauseController _pauseController;

    private int _actualIndex;

    private void OnEnable()
    {
        _gameManager.damageEvent += HandleDamageAudio;
        _pauseController.pauseEvent += HandlePause;
    }

    private void OnDisable()
    {
        _gameManager.damageEvent -= HandleDamageAudio;
        _pauseController.pauseEvent -= HandlePause;
    }

    private void HandleDamageAudio(int damage)
    {
        Debug.Log(damage);
        if (damage >= _audio.Count || damage < 0)
            return;
        _actualIndex = damage;
        for (int i = 0; i < _audio.Count; i++)
        {
            _audio[i].Stop();
        }
        _audio[damage].Play();
    }

    private void HandlePause(bool isPaused)
    {
        if (isPaused)
        {
            for (int i = 0; i < _audio.Count; i++)
            {
                _audio[i].Stop();
            }
        }
        else
        {
            _audio[_actualIndex].Play();
        }
    }
}
