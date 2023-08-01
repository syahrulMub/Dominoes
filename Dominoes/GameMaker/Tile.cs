namespace Dominoes;

public class Tile : ITile
{
    private int _sideA;
    private int _sideB;
    private TileOrientation _orientation;

    public Tile(int sideA, int sideB)
    {
        SetTileValue(sideA, sideB);
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

}

public enum TileOrientation
{
    vertical,
    horizontal
}

public enum TilePoint
{
    BottomSide,
    Middle,
    TopSide
}
