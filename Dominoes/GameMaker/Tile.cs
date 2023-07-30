namespace Dominoes;

public class Tile : ITile
{
    private int _sideA;
    private int _sideB;
    private Position? _position;

    public Tile(int sideA, int sideB)
    {
        SetTileValue(sideA, sideB);
    }

    public int GetTilesideA()
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
    public Position? GetPosition()
    {
        return _position;
    }
    public override string ToString()
    {
        return $"{_sideA}|{_sideB}";
    }
}

// public enum TileOriantation
// {
//     vertical,
//     horizontal
// }

// public enum TilePoint
// {
//     BottomSide,
//     Middle,
//     TopSide
// }
