using System;
using System.Collections.Generic;
namespace Dominoes;

public class GameRunner
{

    //variabel that game runner needed
    private List<IPlayer> _players;
    public Dictionary<IPlayer, List<Tile>> _playersResource;
    private Board _board;
    public Boneyard _boneyard;
    private IPlayer _currentPlayer;
    private List<int> _validSideTiles;
    private List<Tile> _tileOnBoard;

    //constructor game runner
    public GameRunner()
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _board = new Board();
        _boneyard = new Boneyard();
        _currentPlayer = _players[0];
        _validSideTiles = new List<int>();
        _tileOnBoard = new List<Tile>();
    }
    public GameRunner(IPlayer player, List<Tile> tile)
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _playersResource.Add(player, tile);
        _board = new Board();
        _boneyard = new Boneyard();
        _validSideTiles = new List<int>();
        _tileOnBoard = new List<Tile>();
        _currentPlayer = _players[0];

    }

    //method for add player to the game
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
    //method for create domino tiles
    public bool GenerateTiles(IPlayer player, int count)
    {
        if (_playersResource != null)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (var Player in _playersResource.Keys)
                {
                    if (player == Player && count != 0)
                    {
                        List<int> tileData = _boneyard.GetTileData();
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
    //checkapakah bounyard available
    public bool CheckBoneyardAvailable()
    {
        if (_boneyard.tilesOnBoneyard?.Count != 0)
        {
            return true;
        }
        return false;
    }
    //method for access every players tiles
    public List<Tile> GetPlayerTiles(IPlayer player)
    {
        return _playersResource[player];
    }
    public IPlayer? GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    //method for insert tile on board
    public bool MakeMove(Tile tile)
    {
        List<Tile> currentPlayerTiles = _playersResource[_currentPlayer];
        if (currentPlayerTiles.Contains(tile))
        {
            //logic for first input
            if (FirstValidMove(tile))
            {
                _tileOnBoard.Add(tile);
                _validSideTiles.Add(tile.GetTileSideA());
                _validSideTiles.Add(tile.GetTileSideB());
                currentPlayerTiles.Remove(tile);
                MoveToNextPlayer();
                return true;
            }
            //logic for nextmove
            else if (!FirstValidMove(tile))
            {
                IsValidMove(tile);
                MoveToNextPlayer();
                return true;
            }
        }
        return false;
    }

    private bool FirstValidMove(Tile tile)
    {
        if (_tileOnBoard.Count == 0)
        {
            if (tile.GetTileSideA() == tile.GetTileSideB())
            {
                tile.SetTileOrientation(TileOrientation.vertical);
                _tileOnBoard.Add(tile);
            }
            else
            {
                tile.SetTileOrientation(TileOrientation.horizontal);
                _tileOnBoard.Add(tile);
            }
            return true;
        }
        return false;
    }
    public void UpdateValidTileSide()
    {
        _validSideTiles.Clear();
        int firstSide = _tileOnBoard[0].GetTileSideA();
        int lastSide = _tileOnBoard[-1].GetTileSideB();

        foreach (var tile in _playersResource[_currentPlayer])
        {
            int sideA = tile.GetTileSideA();
            int sideB = tile.GetTileSideB();

            if (sideA == firstSide || sideB == firstSide)
            {
                _validSideTiles.Add(sideA);
            }
            if (sideA == lastSide || sideB == lastSide)
            {
                _validSideTiles.Add(sideB);
            }
        }
    }
    public bool IsValidMove(Tile tile)
    {
        UpdateValidTileSide();
        if (_validSideTiles.Count == 0)
        {
            return false;
        }

        if (_validSideTiles.Contains(tile.GetTileSideA()))
        {
            tile.SetTileOrientation(TileOrientation.horizontal);
            _tileOnBoard.Insert(0, tile);
        }
        else if (_validSideTiles.Contains(tile.GetTileSideB()))
        {
            tile.SetTileOrientation(TileOrientation.horizontal);
            _tileOnBoard.Add(tile);
        }
        _playersResource[_currentPlayer].Remove(tile);
        return true;
    }
    protected void MoveToNextPlayer()
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
    public Board GetBoard()
    {
        return _board;
    }
}