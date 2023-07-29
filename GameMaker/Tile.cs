namespace Dominoes;

public class Tile
{
    private int _sideA;
    private int _sideB;

    public Tile(int sideA, int sideB)
    {
        SetTilesValue(sideA, sideB);
    }

    public int GetTilessideA()
    {
        return _sideA;
    }
    public int GetTilesSideB()
    {
        return _sideB;
    }
    public void SetTilesValue(int sideA, int sideB)
    {
        _sideA = sideA;
        _sideB = sideB;

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
