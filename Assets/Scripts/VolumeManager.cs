using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    //public Image volumeBar;
    //public float maxVolume = 100f;
    //public float currentVolume = 100f;

    [SerializeField] private AudioMixer _audioMixer;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    LowerVolume();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    HigherVolume();
        //}
    }

    public void ChangeVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }

    //public void LowerVolume()
    //{
    //    currentVolume -= 10;
    //    volumeBar.fillAmount = currentVolume / maxVolume;
    //}

    //public void HigherVolume()
    //{
    //    currentVolume += 10;
    //    volumeBar.fillAmount = currentVolume / maxVolume;
    //}
}
