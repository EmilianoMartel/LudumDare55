using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Data Source", menuName = "ListData")]
public class JsonDataSo : ScriptableObject
{
    private List<DemonName> _namesList;
    private List<DemonType> _typesList;
    private List<Dungeons> _dungeonList;
    private List<Wizards> _wizardNameList;

    public List<DemonName> namesList { get { return _namesList; } set{ _namesList = value; } }
    public List<DemonType> typesList { get { return _typesList; } set { _typesList = value;} }
    public List<Dungeons> dungeonList { get { return _dungeonList;} set { _dungeonList = value;} }
    public List<Wizards> wizardsNameList { get { return _wizardNameList; } set { _wizardNameList = value; } }
}
