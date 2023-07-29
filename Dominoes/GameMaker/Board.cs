namespace Dominoes;

public class Board : IBoard
{
    private int _sizeX;
    private int _sizeY;
    public int GetSizeX()
    {
        return _sizeX;
    }

    public int GetSizeY()
    {
        return _sizeY;
    }

    public bool SetBoard(int sizeX, int sizeY)
    {
        if (sizeX > 0 && sizeY > 0)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            return true;
        }
        return false;
    }
}
