using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Image volumeBar;
    public float maxVolume = 100f;
    public float currentVolume = 100f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LowerVolume();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HigherVolume();
        }
    }

    public void LowerVolume()
    {
        currentVolume -= 10;
        volumeBar.fillAmount = currentVolume / maxVolume;
    }

    public void HigherVolume()
    {
        currentVolume += 10;
        volumeBar.fillAmount = currentVolume / maxVolume;
    }


}
