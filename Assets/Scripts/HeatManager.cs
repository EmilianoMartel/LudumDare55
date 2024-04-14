using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatManager : MonoBehaviour
{

    [SerializeField] public float maxHeat;
    public float currentHeat;
    [SerializeField] public Image thermoBar;


    private void Update()
    {
        //testing
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentHeat += 12;
        }

        Heater();
    }

    public void Heater()
    {
        thermoBar.fillAmount = (currentHeat / maxHeat);
        if(currentHeat > maxHeat)
        {
            currentHeat = maxHeat;
        }
    }
}
