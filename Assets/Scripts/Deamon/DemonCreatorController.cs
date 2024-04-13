using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class DemonCreatorController : MonoBehaviour
{
    [SerializeField] private ConfirmLogic _confirmLogic;
    [SerializeField] private List<DeamonButton> _demonButtonList = new List<DeamonButton>();
    [SerializeField] private Demon _prefabDemon;
    [Header("DataSource")]
    [SerializeField] private JsonDataSo _dataSO;
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();
    
    private List<Demon> _demonList = new List<Demon>();
    private List<Demon> _activeList = new List<Demon>();
    private List<Demon> _dieDemonList = new List<Demon>();

    private DeamonButton _demonButton;

    public Action<Demon, Sprite> sendDeamonAction;


    private void OnEnable()
    {
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            _demonButtonList[i].selectedButtonEvent += HandleButtonChange;
        }
        _confirmLogic.confirmEvent += HandleConfirmEvent;
    }

    private void OnDisable()
    {
        _confirmLogic.confirmEvent -= HandleConfirmEvent;
        for (int i = 0; i < _demonList.Count; i++)
        {
            _demonList[i].canActive -= HandleActiveDemon;
        }
        for (int i = 0; i < _activeList.Count; i++)
        {
            _activeList[i].canActive -= HandleActiveDemon;
        }
        for (int i = 0; i < _dieDemonList.Count; i++)
        {
            _dieDemonList[i].canActive -= HandleActiveDemon;
        }
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            _demonButtonList[i].selectedButtonEvent -= HandleButtonChange;
        }
    }

    private Demon CreateDemon(int indexName)
    {
        int indexType = UnityEngine.Random.Range(0,_dataSO.typesList.Count);
        int indexCategory = UnityEngine.Random.Range(1,5);

        Category cat;
        switch (indexCategory)
        {
            case 1:
                cat = Category.cat1;
                break;
            case 2:
                cat = Category.cat2;
                break;
            case 3:
                cat = Category.cat3;
                break;
            case 4:
                cat = Category.cat4;
                break;
            default:
                cat = Category.cat1;
                break;
        }

        Demon temp = Instantiate(_prefabDemon);
        temp.StartDemon(_dataSO.namesList[indexName], _dataSO.typesList[indexType], cat, _spriteList[indexName]);
        temp.canActive += HandleActiveDemon;

        return temp;
    }

    [ContextMenu("SendDemons")]
    private void SendFirstDemons()
    {
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            Demon profile = CheckActiveDemon();
            profile.isActive = true;
            if (_demonList.Contains(profile))
            {
                _demonList.Remove(profile);
                _activeList.Add(profile);
            }
            _demonButtonList[i].ReciveDemon(profile, profile.face);
        }
    }

    private Demon CheckActiveDemon()
    {
        for (int i = 0; i < _demonList.Count; i++)
        {
            if (!_demonList[i].isActive)
            {
                return _demonList[i];
            }
        }
        Demon demon = null;
        return demon;
    }

    [ContextMenu("Create List")]
    private void CreateListDemons()
    {
        for (int i = 0; i < _dataSO.namesList.Count; i++)
        {
            _demonList.Add(CreateDemon(i));
        }
        Shuffle(_demonList);
    }

    private void HandleActiveDemon(Demon demon)
    {
        if (_activeList.Contains(demon))
        {
            _activeList.Remove(demon);
            _demonList.Add(demon);
        }
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            T temp = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = temp;
        }
    }

    private void HandleButtonChange(DeamonButton demonButton)
    {
        _demonButton = demonButton;
    }

    private void HandleConfirmEvent()
    {
        Demon profile = CheckActiveDemon();
        profile.isActive = true;
        if (_demonList.Contains(profile))
        {
            _demonList.Remove(profile);
            _activeList.Add(profile);
        }
        _demonButton.ReciveDemon(profile, profile.face);
    }
}