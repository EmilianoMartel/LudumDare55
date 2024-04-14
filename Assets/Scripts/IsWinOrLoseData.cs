using System;
using UnityEngine;

[CreateAssetMenu(menuName = "WinData", fileName = "WinData")]
public class IsWinOrLoseData : ScriptableObject 
{
    private bool _bool;

    public void SetBool(bool isWinning)
    {
        _bool = isWinning;
    }

    public bool GetBool()
    {
        return _bool;
    }
}
