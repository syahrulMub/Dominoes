using System.Collections.Generic;
namespace Dominoes;

public class Boneyard
{
    public List<List<int>>? tilesOnBoneyard;
    private int _totalSide;

    public Boneyard(int totalSide)
    {
        tilesOnBoneyard = new List<List<int>>();
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
                tilesOnBoneyard?.Add(tile);
            }
        }
    }
    public bool ShuffleTiles()
    {
        if (tilesOnBoneyard?.Count >= 2)
        {
            Random rondom = new Random();
            int n = tilesOnBoneyard.Count;
            while (n > 1)
            {
                n--;
                int randomIndex = rondom.Next(n + 1);
                List<int> value = tilesOnBoneyard[randomIndex];
                tilesOnBoneyard[randomIndex] = tilesOnBoneyard[n];
                tilesOnBoneyard[n] = value;
            }
            return true;
        }
        return false;
    }
    public List<int>? GetTileData()
    {
        if (tilesOnBoneyard?.Count > 0)
        {
            List<int> data = tilesOnBoneyard[0];
            return data;
        }
        else
        {
            return null;
        }
    }
    public bool RemoveData(List<int> data)
    {
        if (tilesOnBoneyard != null)
        {
            if (tilesOnBoneyard.Contains(data))
            {
                tilesOnBoneyard.Remove(data);
                return true;
            }
            return false;
        }
        return false;
    }

}