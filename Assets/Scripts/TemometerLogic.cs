using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemometerLogic : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void HandleDamage(float damage)
    {
        _image.fillAmount = damage;
    }

    public void ResetAmount()
    {
        _image.fillAmount = 0;
    }
}
