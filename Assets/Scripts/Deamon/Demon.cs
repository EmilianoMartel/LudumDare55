using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DemonName
{
    private string _name;
    public string name { get { return _name; } set { _name = value; } }
}

public class DemonType
{
    public string Modifier = "null";
    public int Effect = 1;
    public string In = "null";
}

public class Category
{
    public string Name = "UselessDemon";
    public int Level = 0;
    public int Rank = 0;
}

public class Demon : MonoBehaviour
{ 
    [SerializeField] private string _name;
    private DemonType _type = new DemonType();
    [SerializeField] private Sprite _face;
    [SerializeField] private Category _category = new Category();
    private bool _isActive = false;
    private float _timeCD = 5;

    public Sprite face { get { return _face; }}
    public bool isActive { get { return _isActive; } set { _isActive = value; } }

    public Action<Demon> canActive;

    public void StartDemon(DemonName name, DemonType type, Category category, Sprite face)
    {
        _name = name.name;
        _type = type;
        _category = category;
        _face = face;
    }

    public string ShowName()
    {
        return _name;
    }

    public DemonType ShowType()
    {
        return _type;
    }

    public string ShowCategoryName()
    {
        return _category.Name;
    }

    public int ShowCategoryPower()
    {
        return _category.Level;
    }

    public void HandleActiveCD()
    {
        StartCoroutine(CouldDown());
    }

    private IEnumerator CouldDown()
    {
        _isActive = true;
        yield return new WaitForSeconds(_timeCD);
        _isActive = false;
        canActive?.Invoke(this);
    }
}