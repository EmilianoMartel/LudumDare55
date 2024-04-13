using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DemonCreatorController : MonoBehaviour
{
    [SerializeField] private List<DeamonButton> _demonButtonList = new List<DeamonButton>();
    [Header("DataSource")]
    [SerializeField] private JsonDataSo _dataSO;
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();

    private Demon _demon;

    public Action<Demon, Sprite> sendDeamonAction;

    [ContextMenu("CreateDemon")]
    private Demon CreateDemon()
    {
        int indexName = UnityEngine.Random.Range(0,_dataSO.namesList.Count);
        int indexType = UnityEngine.Random.Range(0,_dataSO.typesList.Count);
        int indexCategory = UnityEngine.Random.Range(0,_dataSO.categoryList.Count);

        _demon = new Demon();
        
        _demon.StartDemon(_dataSO.namesList[indexName], _dataSO.typesList[indexType], _dataSO.categoryList[indexCategory]);

        return _demon;
    }

    [ContextMenu("Show")]
    private void ShowDemon()
    {
        Debug.Log($"Name: {_demon.ShowName()}");
        Debug.Log($"Type: {_demon.ShowType()}");
        Debug.Log($"Category: {_demon.ShowCategory()}");
    }

    [ContextMenu("SendDemons")]
    private void SendDemons()
    {
        for (int i = 0; i < _demonButtonList.Count; i++)
        {
            int index = UnityEngine.Random.Range(0, _spriteList.Count);
            _demonButtonList[i].ReciveDemon(CreateDemon(), _spriteList[index]);
        }
    }
}