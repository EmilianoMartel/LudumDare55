using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonReaders : MonoBehaviour
{
    private List<DemonName> _namesList;
    private List<DemonType> _typeList;
    private List<DemonCategory> _categoryList;

    [Header("JsonFiles")]
    [SerializeField] private string NAMES_FILE = "Names.json";
    [SerializeField] private string TYPE_FILE = "Type.json";
    [SerializeField] private string CATEGORY_FILE = "Category.json";
    [Header("DataSO")]
    [SerializeField] private JsonDataSo _dataSO;

    private static string _relativeFolder = "StreamingAssets";
    private string _filePathName;
    private string _filePathType;
    private string _filePathCategory;

    private void Awake()
    {
        ListGenerator();
        _dataSO.namesList = _namesList;
        _dataSO.typesList = _typeList;
        _dataSO.categoryList = _categoryList;
    }

    private void ListGenerator()
    {
        _filePathName = Path.Combine(Application.dataPath, _relativeFolder, NAMES_FILE);
        _filePathType = Path.Combine(Application.dataPath, _relativeFolder, TYPE_FILE);
        _filePathCategory = Path.Combine(Application.dataPath, _relativeFolder, CATEGORY_FILE);
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
            if (File.Exists(_filePathCategory))
            {
                _categoryList = JsonConvert.DeserializeObject<List<DemonCategory>>(File.ReadAllText(_filePathCategory));
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
