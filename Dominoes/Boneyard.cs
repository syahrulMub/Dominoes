using System.Collections.Generic;
namespace Dominoes;

public class Boneyard
{
    private List<List<int>>? _tilesOnBoneyard;
    private int _totalSide;

    public Boneyard(int totalSide)
    {
        _tilesOnBoneyard = new List<List<int>>();
        _totalSide = totalSide;
        CreateDominoTiles();
        ShuffleTiles();
    }
    public Boneyard()
    {

    }
    protected void CreateDominoTiles()
    {
        for (int sideA = 0; sideA <= _totalSide; sideA++)
        {
            for (int sideB = sideA; sideB <= _totalSide; sideB++)
            {
                List<int> tile = new List<int> { sideB, sideA };
                _tilesOnBoneyard?.Add(tile);
            }
        }
    }
    public bool ShuffleTiles()
    {
        if (_tilesOnBoneyard?.Count >= 2)
        {
            Random rondom = new Random();
            int n = _tilesOnBoneyard.Count;
            while (n > 1)
            {
                n--;
                int randomIndex = rondom.Next(n + 1);
                List<int> value = _tilesOnBoneyard[randomIndex];
                _tilesOnBoneyard[randomIndex] = _tilesOnBoneyard[n];
                _tilesOnBoneyard[n] = value;
            }
            return true;
        }
        return false;
    }
    public List<List<int>>? GetTilesOnBoneyard()
    {
        return _tilesOnBoneyard;
    }
    public List<int>? GetTileData()
    {
        if (_tilesOnBoneyard?.Count > 0)
        {
            List<int> data = _tilesOnBoneyard[0];
            return data;
        }
        else
        {
            return null;
        }
    }
    public bool RemoveData(List<int> data)
    {
        if (_tilesOnBoneyard != null)
        {
            _tilesOnBoneyard.Remove(data);
            return false;
        }
        return false;
    }

}