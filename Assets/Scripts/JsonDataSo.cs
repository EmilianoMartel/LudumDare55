using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Data Source", menuName = "ListData")]
public class JsonDataSo : ScriptableObject
{
    private List<DemonName> _namesList = null;
    private List<DemonType> _typesList = null;
    private List<Dungeons> _dungeonList = null;
    private List<Wizards> _wizardNameList = null;
    private List<Category> _categoryList = null;

    public List<DemonName> namesList { get { return _namesList; } set{ _namesList = value; } }
    public List<DemonType> typesList { get { return _typesList; } set { _typesList = value;} }
    public List<Dungeons> dungeonList { get { return _dungeonList;} set { _dungeonList = value;} }
    public List<Wizards> wizardsNameList { get { return _wizardNameList; } set { _wizardNameList = value; } }
    public List<Category> categoryList { get { return _categoryList; } set { _categoryList = value;} }
}
