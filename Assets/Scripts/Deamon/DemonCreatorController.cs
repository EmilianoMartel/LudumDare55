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
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ConfirmLogic _confirmLogic;
    [SerializeField] private TimerRequest _timerRequest;
    [SerializeField] private WizardRequestLogic _wizardRequestLogic;
    [SerializeField] private SendDemonButton _sendDemon;
    [SerializeField] private List<DeamonButton> _demonButtonList = new List<DeamonButton>();
    [SerializeField] private Demon _prefabDemon;
    [SerializeField] private Demon _emptyDemonPrefab;
    [Header("DataSource")]
    [SerializeField] private JsonDataSo _dataSO;
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();
    
    private List<Demon> _demonList = new List<Demon>();
    private List<Demon> _activeList = new List<Demon>();

    private DeamonButton _demonButton;
    private Demon _emptyDemon;

    public Action<Demon, Sprite> sendDeamonAction;
    public Action<Demon> electedDemonEvent = delegate { };
    public Action<Demon> finalTimeEvent = delegate { };
    public Action allDemonsDieEvent = delegate { };

    private int _actualTier;

    private void OnEnable()
    {
        _gameManager.startGameEvent += StartGame;
        _timerRequest.endTimerEvent += HandleTimerRequest;
        _wizardRequestLogic.noneRequestEvent += HandleNoneRequest;
        _wizardRequestLogic.reactiveRequestEvent += HandleReactiveRequest;
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            _demonButtonList[i].selectedButtonEvent += HandleButtonChange;
        }
        _confirmLogic.confirmEvent += HandleConfirmEvent;
    }

    private void OnDisable()
    {
        _gameManager.startGameEvent -= StartGame;
        _confirmLogic.confirmEvent -= HandleConfirmEvent;
        _timerRequest.endTimerEvent -= HandleTimerRequest;

        _wizardRequestLogic.noneRequestEvent -= HandleNoneRequest;
        _wizardRequestLogic.reactiveRequestEvent -= HandleReactiveRequest;
        for (int i = 0; i < _demonList.Count; i++)
        {
            _demonList[i].canActive -= HandleActiveDemon;
        }
        for (int i = 0; i < _activeList.Count; i++)
        {
            _activeList[i].canActive -= HandleActiveDemon;
        }
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            _demonButtonList[i].selectedButtonEvent -= HandleButtonChange;
        }
    }

    private Demon CreateDemon(int indexName)
    {
        Category cat = CheckCategory();

        Demon temp = Instantiate(_prefabDemon);
        temp.StartDemon(_dataSO.namesList[indexName], _dataSO.typesList[indexName], cat, _spriteList[indexName]);
        temp.canActive += HandleActiveDemon;

        return temp;
    }

    private Category CheckCategory()
    {
        int tier = UnityEngine.Random.Range(1, 4);
        while (tier == _actualTier)
        {
            tier = UnityEngine.Random.Range(1, 4);
        }
        List<Category> _random = new List<Category>();
        for (int i = 0; i < _dataSO.categoryList.Count; i++)
        {
            if (_dataSO.categoryList[i].Rank == tier)
            {
                _random.Add(_dataSO.categoryList[i]);
            }
        }
        int index = UnityEngine.Random.Range(0, _random.Count);
        _actualTier = tier;
        return _random[index];
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
        if (_demonList[0] != null)
            return _demonList[0];
        
        Demon demon = _emptyDemon;
        return demon;
    }

    [ContextMenu("Create List")]
    private void CreateListDemons()
    {
        Shuffle(_dataSO.typesList);
        for (int i = 0; i < _dataSO.namesList.Count; i++)
        {
            _demonList.Add(CreateDemon(i));
        }
        _emptyDemon = Instantiate(_emptyDemonPrefab);
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
        electedDemonEvent?.Invoke(_demonButton.demon);
    }

    private void HandleConfirmEvent()
    {
        Demon profile = CheckActiveDemon();
        Debug.Log("New demon " + profile.ShowName());
        if (_demonList.Contains(profile))
        {
            _demonList.Remove(profile);
            _activeList.Add(profile);
        }
        _demonButton.ReciveDemon(profile, profile.face);
    }

    private void StartGame()
    {
        CreateListDemons();
        SendFirstDemons();
    }

    private void HandleTimerRequest()
    {
        finalTimeEvent?.Invoke(_emptyDemon);
        Debug.Log("Empty demon " + _emptyDemon);
    }

    private void HandleNoneRequest()
    {
        _sendDemon.gameObject.SetActive(false);
    }

    private void HandleReactiveRequest()
    {
        _sendDemon.gameObject.SetActive(true);
    }
}