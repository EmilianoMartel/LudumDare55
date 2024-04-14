using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Dungeons
{
    public string Name;
    public int Danger;
}

public class Wizards
{
    public string Name;
    public int Power;
}

public class JsonReaders : MonoBehaviour
{
    private List<DemonName> _namesList;
    private List<DemonType> _typeList;
    private List<Dungeons> _dungeonsList;
    private List<Wizards> _wizardList;
    private List<Category> _categoryList;

    [Header("JsonFiles")]
    [SerializeField] private string NAMES_FILE = "Names.json";
    [SerializeField] private string TYPE_FILE = "Type.json";
    [SerializeField] private string DUNGEON_FILE = "Dungeon.json";
    [SerializeField] private string WIZARD_FILE = "WizardNames.json";
    [SerializeField] private string CATEGORY_FILE = "Category.json";
    [Header("DataSO")]
    [SerializeField] private JsonDataSo _dataSO;

    private static string _relativeFolder = "StreamingAssets";
    private string _filePathName;
    private string _filePathType;
    private string _filePathDungeon;
    private string _filePathWizard;
    private string _filePathCategory;

    private void Awake()
    {
        ListGenerator();
        _dataSO.namesList = _namesList;
        _dataSO.typesList = _typeList;
        _dataSO.dungeonList = _dungeonsList;
        _dataSO.wizardsNameList = _wizardList;
        _dataSO.categoryList = _categoryList;
    }

    private void ListGenerator()
    {
        _filePathName = Path.Combine(Application.dataPath, _relativeFolder, NAMES_FILE);
        _filePathType = Path.Combine(Application.dataPath, _relativeFolder, TYPE_FILE);
        _filePathDungeon = Path.Combine(Application.dataPath, _relativeFolder, DUNGEON_FILE);
        _filePathWizard = Path.Combine(Application.dataPath, _relativeFolder, WIZARD_FILE);
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
            if (File.Exists(_filePathDungeon))
            {
                _dungeonsList = JsonConvert.DeserializeObject<List<Dungeons>>(File.ReadAllText(_filePathDungeon));
            }
            if (File.Exists(_filePathWizard))
            {
                _wizardList = JsonConvert.DeserializeObject<List<Wizards>>(File.ReadAllText(_filePathWizard));
            }
            if (File.Exists(_filePathCategory))
            {
                _categoryList = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(_filePathCategory));
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
