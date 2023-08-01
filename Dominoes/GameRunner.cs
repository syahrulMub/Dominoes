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
    private IPlayer? _currentPlayer;

    //constructor game runner
    public GameRunner()
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _board = new Board();
        _boneyard = new Boneyard();
        _currentPlayer = null;
    }
    public GameRunner(IPlayer player, List<Tile> tile)
    {
        _players = new List<IPlayer>();
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _playersResource.Add(player, tile);
        _board = new Board();
        _boneyard = new Boneyard();

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
            //logic for tile on board
            _board.SetTilesOnBoard(tile);
            currentPlayerTiles.Remove(tile);
            //logic for input parameter
            if (validMOve(tile))
            {

            }
            MoveToNextPlayer();
            return true;
        }
        return false;
    }

    private bool validMOve(Tile tile)
    {
        Tile? validTile = null;
        if (_board.getTilesOnBoard() == null)
        {
            if (tile.GetTileSideA() == tile.GetTileSideB())
            {
                TileOriantation validTileVertical = TileOriantation.vertical;
                validTile = validTileVertical;
            }
            return true;
        }
        for (TilePoint tilePoint =  )
        else if (validTile?.GetTileSideA() == tile.GetTileSideA() || validTile?.GetTileSideB() == tile.GetTileSideB())
                {
                    validTile = tile;
                }
    }
    private bool validMOvePoint(Tile tile)
    {
        if (TileOriantation tile = TileOriantation.horizontal)
        {

        }
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

