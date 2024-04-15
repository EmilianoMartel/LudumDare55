using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [Tooltip("sounds must be put in order")]
    [SerializeField] private List<AudioSource> _audio = new List<AudioSource>();
    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager.damageEvent += HandleDamageAudio;
    }

    private void OnDisable()
    {
        _gameManager.damageEvent -= HandleDamageAudio;
    }

    private void HandleDamageAudio(int damage)
    {
        Debug.Log(damage);
        if (damage >= _audio.Count || damage < 0)
            return;
        Debug.Log("llega");
        _audio[damage].Play();
    }
}
