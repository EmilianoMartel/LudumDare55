using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonSound;

    public void OnButtonPressed()
    {
        _buttonSound.Play();
    }
}
