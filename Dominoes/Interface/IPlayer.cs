namespace Dominoes;

public interface IPlayer
{
    bool SetName(string? name);
    bool SetID(int id);
    string? GetName();
    int GetID();
}