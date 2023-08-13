using System.Linq;
namespace Dominoes;

public partial class GameRunner
{
    public event EventHandler gameEnded;


    public bool IsEnded()
    {
        if (_board == null || _players == null || _playersResource == null)
        {
            return false;
        }
        if (GameEndWithZeroTile())
        {
            gameEnded?.Invoke(this, EventArgs.Empty);
            return true;
        }
        if (_gameMode == GameMode.blockMode && _validSideTiles.Count >= 2)
        {
            if (GameEndWithNoSameTiles(_validSideTiles[0]) && GameEndWithNoSameTiles(_validSideTiles[1]))
            {
                if (_verticalTileOnBoard.Count != 0)
                {
                    if (GameEndWithNoSameTiles(_validSideTiles[2]) && GameEndWithNoSameTiles(_validSideTiles[3]))
                    {
                        gameEnded?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
                else
                {
                    gameEnded?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        }
        if (_boneyard.GetTilesOnBoneyard()?.Count == 0 && _validSideTiles.Count >= 2)
        {
            if (GameEndWithNoSameTiles(_validSideTiles[0]) && GameEndWithNoSameTiles(_validSideTiles[1]))
            {
                if (_verticalTileOnBoard.Count != 0)
                {
                    if (GameEndWithNoSameTiles(_validSideTiles[2]) && GameEndWithNoSameTiles(_validSideTiles[3]))
                    {
                        gameEnded?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
                else
                {
                    gameEnded?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        }
        return false;
    }
    public List<KeyValuePair<IPlayer, int>> GetLeaderBoard()
    {
        List<KeyValuePair<IPlayer, int>> leaderBoard = new();

        foreach (var player in _playersResource.Keys)
        {
            int tileCount = PlayerTileCount(player);
            leaderBoard.Add(new KeyValuePair<IPlayer, int>(player, tileCount));
        }
        leaderBoard.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        return leaderBoard;
    }
    public int PlayerTileCount(IPlayer player)
    {
        if (_playersResource.TryGetValue(player, out List<Tile>? playerTiles))
        {
            int count = playerTiles.Sum(tile => tile.GetTileSideA() + tile.GetTileSideB());
            return count;
        }
        return 0;
    }
}
