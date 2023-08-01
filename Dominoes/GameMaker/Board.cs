namespace Dominoes;

public class Board : IBoard
{
    private int _sizeX;
    private int _sizeY;
    private List<Tile>? _tilesOnBoard;
    public int GetSizeX()
    {
        return _sizeX;
    }
    public int GetSizeY()
    {
        return _sizeY;
    }
    bool IBoard.SetBoard(int sizeX, int sizeY)
    {
        if (sizeX > 0 && sizeY > 0)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            return true;
        }
        return false;
    }
    public List<Tile>? getTilesOnBoard()
    {
        return _tilesOnBoard;
    }
}
