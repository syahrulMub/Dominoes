namespace Dominoes;

public class Tile : ITile
{
    private int _sideA;
    private int _sideB;
    private TileOrientation _orientation;
    private Position _tilePosition;

    public Tile(int sideA, int sideB)
    {
        SetTileValue(sideA, sideB);
        _tilePosition = new();
    }

    public int GetTileSideA()
    {
        return _sideA;
    }
    public int GetTileSideB()
    {
        return _sideB;
    }
    public bool SetTileValue(int sideA, int sideB)
    {
        if (sideA >= 0 && sideB >= 0)
        {
            _sideB = sideB;
            _sideA = sideA;
            return true;
        }
        return false;
    }
    public bool FlipTiles()
    {
        if (_sideA >= 0 && _sideB >= 0)
        {
            int temp = _sideA;
            _sideA = _sideB;
            _sideB = temp;
            return true;
        }
        return false;
    }
    public override string ToString()
    {
        return $"{_sideA}|{_sideB}";
    }
    public bool SetTileOrientation(TileOrientation orientation)
    {
        if (TileOrientation.horizontal == orientation || TileOrientation.vertical == orientation)
        {
            _orientation = orientation;
            return true;
        }
        return false;
    }
    public TileOrientation GetTileOrientation()
    {
        return _orientation;
    }
    public Position? GetTilePosition()
    {
        return _tilePosition;
    }
    public void SetTilePosition(int x, int y)
    {
        _tilePosition.SetPosition(x, y);
    }
}