using UnityEngine;
using UnityEngine.UI;


public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySoundButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlaySoundButton);
    }

    private void PlaySoundButton()
    {
        _audio.Play();
    }
}
