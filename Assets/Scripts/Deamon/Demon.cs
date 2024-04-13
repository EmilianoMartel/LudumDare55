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
    private string _type;
    public string type { get { return _type; } set { _type = value; } }
}

public enum Category
{
    cat1,
    cat2,
    cat3,
    cat4
}

public class Demon : MonoBehaviour
{
    private string _name;
    private string _type;
    private Sprite _face;
    private Category _category;
    private bool _isActive = false;
    private float _timeCD = 5;

    public Sprite face { get { return _face; }}
    public bool isActive { get { return _isActive; } set { _isActive = value; } }

    public Action<Demon> canActive;

    public void StartDemon(DemonName name, DemonType type, Category category, Sprite face)
    {
        _name = name.name;
        _type = type.type;
        _category = category;
        _face = face;
    }

    public string ShowName()
    {
        return _name;
    }

    public string ShowType()
    {
        return _type;
    }

    public string ShowCategory()
    {
        switch (_category)
        {
            case Category.cat1:
                return "one";
            case Category.cat2:
                return "Two";
            case Category.cat3:
                return "Trhee";
            case Category.cat4:
                return "Four";
            default:
                return "none";
        }
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