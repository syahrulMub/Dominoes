using System;
using System.Collections.Generic;
namespace Dominoes;

public partial class GameRunner
{

    //variabel that game runner needed
    private List<IPlayer> _players;
    private Dictionary<IPlayer, List<Tile>> _playersResource;
    private IBoard _board;
    private Boneyard _boneyard;
    private GameMode _gameMode;
    private IPlayer? _currentPlayer;
    private List<int> _validSideTiles;
    private List<Tile> _tileOnBoard;
    private List<Tile> _verticalTileOnBoard;

    //constructor game runner
    public GameRunner()
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _board = new Board();
        _boneyard = new Boneyard();
        _gameMode = GameMode.drawMode;
        _currentPlayer = null;
        _validSideTiles = new List<int>();
        _tileOnBoard = new List<Tile>();
        _verticalTileOnBoard = new List<Tile>();

    }
    public GameRunner(IPlayer player, List<Tile> tile)
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _playersResource.Add(player, tile);
        _board = new Board();
        _boneyard = new Boneyard();
        _gameMode = GameMode.drawMode;
        _validSideTiles = new List<int>();
        _tileOnBoard = new List<Tile>();
        _verticalTileOnBoard = new List<Tile>();
    }

    public bool AddPlayer(IPlayer player)
    {
        if (player != null)
        {
            _players?.Add(player);
            List<Tile> tilesPlayer = new List<Tile>();
            _playersResource.Add(player, tilesPlayer);
            return true;
        }
        return false;

    }
    public bool AddBondyard(Boneyard boneyard)
    {
        if (boneyard != null)
        {
            _boneyard = boneyard;
            return true;
        }
        return false;
    }
    public bool AddBoard(IBoard board)
    {
        if (board != null)
        {
            _board = board;
            return true;
        }
        return false;
    }
    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
    }
    public GameMode GetGameMode()
    {
        return _gameMode;
    }
    /// <summary>
    /// generating tile from bone yard if it available
    /// </summary>
    /// <param name="player">target generate tile to they hand</param>
    /// <param name="count">total tile will player pick</param>
    /// <returns></returns>
    public bool GenerateTiles(IPlayer player, int count)
    {
        if (_boneyard.GetTilesOnBoneyard()?.Count >= count && _playersResource != null)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (var Player in _playersResource.Keys)
                {
                    if (player == Player && count != 0)
                    {
                        List<int>? tileData = _boneyard.GetTileData();
                        int a = tileData[0];
                        int b = tileData[1];
                        _playersResource[player].Add(new Tile(a, b));
                        _boneyard.RemoveData(tileData);
                    }
                }
            }
            return true;
        }
        return false;
    }
    public bool CheckBoneyardAvailable()
    {
        if (_boneyard.GetTilesOnBoneyard()?.Count != 0)
        {
            return true;
        }
        return false;
    }
    public List<IPlayer> GetPlayers()
    {
        return _players;
    }

    public List<Tile> GetPlayerTiles(IPlayer player)
    {
        return _playersResource[player];
    }
    public IPlayer? GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void SetCurrentPlayer(int index)
    {
        _currentPlayer = _players[index];
    }

    //method for insert tile on board

    public void MoveToNextPlayer()
    {
        if (_currentPlayer != null)
        {
            int currentPLayerIndex = _players.IndexOf(_currentPlayer);
            if (currentPLayerIndex >= 0 && currentPLayerIndex < _players.Count - 1)
            {
                _currentPlayer = _players[currentPLayerIndex + 1];
            }
            else
            {
                _currentPlayer = _players[0];
            }
        }
    }
    public IBoard GetBoard()
    {
        return _board;
    }
    public List<Tile> GetTileOnBoard()
    {
        return _tileOnBoard;
    }
    public List<Tile> GetTileVerticalOnBoard()
    {
        return _verticalTileOnBoard;
    }
    public bool GameEndWithZeroTile()
    {
        foreach (var playerTile in _playersResource.Values)
        {
            if (playerTile.Count == 0)
            {
                return true;
            }
        }
        return false;
    }
    private bool GameEndWithNoSameTiles(int validTile)
    {
        bool thisPlayerValidTiles = false;
        foreach (var playerTile in _playersResource.Values)
        {
            foreach (var tile in playerTile)
            {
                if (tile.GetTileSideA() == validTile || tile.GetTileSideB() == validTile)
                {
                    thisPlayerValidTiles = true;
                }
            }
            if (thisPlayerValidTiles)
            {
                return false;
            }
        }
        if (!thisPlayerValidTiles)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// this method to validate all valid tiles that player can move
    /// if no valid tile in player tile game management will not give player a chance to move
    /// </summary>
    /// <param name="player">check all tile players</param>
    /// <returns>true if at least have one valid tile to place on board</returns> <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <returns>false if all tile did't have valid number with valid side</returns>
    public bool ValidMove(IPlayer player)
    {
        foreach (var thisTile in _playersResource[player])
        {
            if (_tileOnBoard.Count == 0)
            {
                return true;
            }
            else if (thisTile.GetTileSideA() == _validSideTiles[0] || thisTile.GetTileSideB() == _validSideTiles[0])
            {
                return true;
            }
            else if (thisTile.GetTileSideA() == _validSideTiles[1] || thisTile.GetTileSideB() == _validSideTiles[1])
            {
                return true;
            }
            else if (_verticalTileOnBoard.Count != 0)
            {
                if (thisTile.GetTileSideA() == _validSideTiles[2] || thisTile.GetTileSideB() == _validSideTiles[2])
                {
                    return true;
                }
                if (thisTile.GetTileSideA() == _validSideTiles[3] || thisTile.GetTileSideB() == _validSideTiles[3])
                {
                    return true;
                }
            }
        }
        return false;
    }
}