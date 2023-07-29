namespace Dominoes;

public class Player : IPlayer
{
    private string _name;
    private int _id;

    public Player(string name, int id)
    {
        _name = name;
        _id = id;
    }

    public bool SetID(int id)
    {
        if (id != 0)
        {
            _id = id;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SetName(string name)
    {
        if (name != null)
        {
            _name = name;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetID()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

}
