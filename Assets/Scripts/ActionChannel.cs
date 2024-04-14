using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bool channel", fileName = "BoolDataChannel")]
public class ActionChannel<Bool> : ScriptableObject 
{
    private Action<bool> _action = delegate { };

    public void Subscription(Action<bool> action)
    {
        _action += action;
    }

    public void UnSubscription(Action<bool> action)
    {
        _action -= action;
    }

    public void InvokeEvent(bool action)
    {
       _action?.Invoke(action);
    }
}
