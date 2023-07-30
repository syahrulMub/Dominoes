namespace Dominoes;

public class Player : IPlayer
{
    private int _id;
    private string? _name;
    int IPlayer.GetID()
    {
        return _id;
    }
    string? IPlayer.GetName()
    {
        return _name;
    }
    bool IPlayer.SetID(int id)
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
    bool IPlayer.SetName(string? name)
    {
        if (name?.Length >= 2)
        {
            _name = name;
            return true;
        }
        else
        {
            return false;
        }
    }
}
