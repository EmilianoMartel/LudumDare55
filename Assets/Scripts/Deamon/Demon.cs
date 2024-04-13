public class DemonName
{
    private string _name;
    public string name { get { return _name;}set { _name = value;} }
}

public class DemonType
{
    private string _type;
    public string type { get { return _type;}set { _type = value;} }
}

public class DemonCategory
{
    private string _category;
    public string category { get { return _category;}set { _category = value;} }
}

public class Demon
{
    private string _name;
    private string _type;
    private string _category;

    public void StartDemon(DemonName name,DemonType type, DemonCategory category)
    {
        _name = name.name;
        _type = type.type;
        _category = category.category;
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
        return _category;
    }
}
