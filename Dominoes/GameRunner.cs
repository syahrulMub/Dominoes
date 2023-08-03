using System;
using System.Collections.Generic;
namespace Dominoes;

public class GameRunner
{

    //variabel that game runner needed
    private List<IPlayer> _players;
    private Dictionary<IPlayer, List<Tile>> _playersResource;
    private Board _board;
    private Boneyard _boneyard;
    private IPlayer? _currentPlayer;
    private List<int> _validSideTiles;
    private List<Tile> _tileOnBoard;

    //constructor game runner
    public GameRunner()
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _board = new Board();
        _currentPlayer = null;
        _boneyard = new Boneyard();
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
    //

    //method for insert tile on board
    public bool MakeMove(Tile tile)
    {
        List<Tile> currentPlayerTiles = _playersResource[_currentPlayer];
        if (currentPlayerTiles.Contains(tile))
        {
            //logic for first input
            if (FirstValidMove(tile))
            {
                currentPlayerTiles.Remove(tile);
                MoveToNextPlayer();
                return true;
            }
            //logic for nextmove
            else if (!FirstValidMove(tile))
            {
                //method for validside last index right
                if (RightValidSide(tile))
                {
                    if (tile.GetTileSideA() == tile.GetTileSideB())
                    {
                        tile.SetTileOrientation(TileOrientation.vertical);
                    }
                    else if (tile.GetTileSideA() != tile.GetTileSideB())
                    {
                        tile.SetTileOrientation(TileOrientation.horizontal);
                    }
                    currentPlayerTiles.Remove(tile);
                    MoveToNextPlayer();
                    return true;
                }
                //method for validside 0 index left
                else if (LeftValidSide(tile))
                {
                    if (tile.GetTileSideA() == tile.GetTileSideB())
                    {
                        tile.SetTileOrientation(TileOrientation.vertical);
                    }
                    else if (tile.GetTileSideA() != tile.GetTileSideB())
                    {
                        tile.SetTileOrientation(TileOrientation.horizontal);
                    }
                    currentPlayerTiles.Remove(tile);
                    MoveToNextPlayer();
                    return true;
                }
                return false;
            }
            return false;
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
                _validSideTiles.Add(tile.GetTileSideB());
                _validSideTiles.Add(tile.GetTileSideA());
            }
            else
            {
                tile.SetTileOrientation(TileOrientation.horizontal);
                _tileOnBoard.Add(tile);
                _validSideTiles.Add(tile.GetTileSideB());
                _validSideTiles.Add(tile.GetTileSideA());
            }
            return true;
        }
        return false;
    }
    private bool LeftValidSide(Tile thisTile)
    {
        if (thisTile.GetTileSideA() == _validSideTiles[0])
        {
            _validSideTiles[0] = thisTile.GetTileSideB();
            _tileOnBoard.Insert(0, thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[0])
        {
            _validSideTiles[0] = thisTile.GetTileSideA();
            _tileOnBoard.Insert(0, thisTile);
            return true;
        }
        return false;
    }
    private bool RightValidSide(Tile thisTile)
    {
        if (thisTile.GetTileSideA() == _validSideTiles[1])
        {
            _validSideTiles[1] = thisTile.GetTileSideB();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[1])
        {
            _validSideTiles[1] = thisTile.GetTileSideA();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        return false;
    }
    private bool TopValidSide(Tile Thistile)
    {
        throw new NotImplementedException();
    }
    private bool BottomValidSide(Tile Thistile)
    {
        throw new NotImplementedException();
    }
    public void MoveToNextPlayer()
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
    public List<Tile> GetTileOnBoard()
    {
        return _tileOnBoard;
    }
    public bool IsEnded()
    {
        if (_board == null || _players == null || _boneyard == null || _playersResource == null)
        {
            return false;
        }

        _currentPlayer = _players[0];
        foreach (var playerTile in _playersResource.Values)
        {
            if (playerTile.Count == 0)
            {
                return true;
            }
        }
        return false;
    }
}