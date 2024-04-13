using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonReaders : MonoBehaviour
{
    private List<DemonName> _namesList;
    private List<DemonType> _typeList;

    [Header("JsonFiles")]
    [SerializeField] private string NAMES_FILE = "Names.json";
    [SerializeField] private string TYPE_FILE = "Type.json";
    [Header("DataSO")]
    [SerializeField] private JsonDataSo _dataSO;

    private static string _relativeFolder = "StreamingAssets";
    private string _filePathName;
    private string _filePathType;

    private void Awake()
    {
        ListGenerator();
        _dataSO.namesList = _namesList;
        _dataSO.typesList = _typeList;
    }

    private void ListGenerator()
    {
        _filePathName = Path.Combine(Application.dataPath, _relativeFolder, NAMES_FILE);
        _filePathType = Path.Combine(Application.dataPath, _relativeFolder, TYPE_FILE);
        try
        {
            if (File.Exists(_filePathName))
            {
                _namesList = JsonConvert.DeserializeObject<List<DemonName>>(File.ReadAllText(_filePathName));
            }
            if (File.Exists(_filePathType))
            {
                _typeList = JsonConvert.DeserializeObject<List<DemonType>>(File.ReadAllText(_filePathType));
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
