namespace Dominoes;

public partial class GameRunner
{
    public event EventHandler? gameEnded;

    /// <summary>
    /// IsEnded method will return true if all condition of the game same as logic game end
    /// </summary>
    /// <returns></returns> 
    /// <summary>
    /// IsEnded also trigged event when game is ended and tell to subscriber that game was Ended
    /// </summary>
    /// <returns></returns>
    public bool IsEnded()
    {
        if (_board == null || _players == null || _playersResource == null)
        {
            return false;
        }
        if (GameEndWithZeroTile())
        {
            logger.Info("game end with zero tile on one player");
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
                        logger.Info("block mode : game end with no valid vertical side");
                        gameEnded?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
                else
                {
                    logger.Info(" block mode : game end with vertical vile available but no player have valid tile");
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
                    logger.Info("draw mode : game end with no valid vertical side");
                    if (GameEndWithNoSameTiles(_validSideTiles[2]) && GameEndWithNoSameTiles(_validSideTiles[3]))
                    {
                        gameEnded?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
                else
                {
                    logger.Info(" draw mode : game end with vertical vile available but no player have valid tile");
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
    private int PlayerTileCount(IPlayer player)
    {
        if (_playersResource.TryGetValue(player, out List<Tile>? playerTiles))
        {
            int count = playerTiles.Sum(tile => tile.GetTileSideA() + tile.GetTileSideB());
            return count;
        }
        return 0;
    }
}
