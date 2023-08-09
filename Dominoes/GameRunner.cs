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
    private List<Tile> _verticalTileOnBoard;

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
        _verticalTileOnBoard = new List<Tile>();

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
        _verticalTileOnBoard = new List<Tile>();
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
    public bool AddBoard(Board board)
    {
        if (board != null)
        {
            _board = board;
            return true;
        }
        return false;
    }
    //method for create domino tiles
    public bool GenerateTiles(IPlayer player, int count)
    {
        if (_boneyard.tilesOnBoneyard.Count >= count && _playersResource != null)
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
    //checkapakah bounyard available
    public bool CheckBoneyardAvailable()
    {
        if (_boneyard.tilesOnBoneyard?.Count != 0)
        {
            return true;
        }
        return false;
    }
    public List<IPlayer> GetPlayers()
    {
        return _players;
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
    public void SetCurrentPlayer(int index)
    {
        _currentPlayer = _players[index];
    }

    //method for insert tile on board
    public bool MakeMove(Tile tile, int side)
    {
        List<Tile> currentPlayerTiles = _playersResource[_currentPlayer];
        if (currentPlayerTiles.Contains(tile))
        {
            if (_verticalTileOnBoard.Count == 0)
            {
                ValidateTopAndButtomSide();
            }
            //logic for first input
            if (FirstValidMove(tile))
            {
                currentPlayerTiles.Remove(tile);
                MoveToNextPlayer();
                return true;
            }
            //logic for nextmove
            else if (!FirstValidMove(tile) && _tileOnBoard != null)
            {
                //method for validside last index right
                if (side == 2)
                {
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
                    return false;
                }
                //method for validside 0 index left
                else if (side == 1)
                {
                    if (LeftValidSide(tile))
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
                else if (side == 3 && _verticalTileOnBoard.Count != 0)
                {
                    if (BottomValidSide(tile))
                    {
                        if (tile.GetTileSideA() == tile.GetTileSideB())
                        {
                            tile.SetTileOrientation(TileOrientation.horizontal);
                        }
                        else if (tile.GetTileSideA() != tile.GetTileSideB())
                        {
                            tile.SetTileOrientation(TileOrientation.vertical);
                        }
                        currentPlayerTiles.Remove(tile);
                        MoveToNextPlayer();
                        return true;
                    }
                    return false;
                }
                else if (side == 4 && _verticalTileOnBoard.Count != 0)
                {
                    if (TopValidSide(tile))
                    {
                        if (tile.GetTileSideA() == tile.GetTileSideB())
                        {
                            tile.SetTileOrientation(TileOrientation.horizontal);
                        }
                        else if (tile.GetTileSideA() != tile.GetTileSideB())
                        {
                            tile.SetTileOrientation(TileOrientation.vertical);
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
        return false;
    }
    private bool FirstValidMove(Tile tile)
    {
        if (_tileOnBoard.Count == 0)
        {
            if (tile.GetTileSideA() == tile.GetTileSideB())
            {
                tile.SetTilePosition(_board.GetBoardSize() / 2, _board.GetBoardSize() / 2);
                tile.SetTileOrientation(TileOrientation.vertical);
                _tileOnBoard.Add(tile);
                _validSideTiles.Add(tile.GetTileSideB());
                _validSideTiles.Add(tile.GetTileSideA());
            }
            else
            {
                tile.SetTilePosition(_board.GetBoardSize() / 2, _board.GetBoardSize() / 2);
                tile.SetTileOrientation(TileOrientation.horizontal);
                _tileOnBoard.Add(tile);
                _validSideTiles.Add(tile.GetTileSideA());
                _validSideTiles.Add(tile.GetTileSideB());
            }
            return true;
        }
        return false;
    }
    private bool LeftValidSide(Tile thisTile)
    {
        if (_tileOnBoard != null || _tileOnBoard.Count == 0)
        {
            if (thisTile.GetTileSideA() == _validSideTiles[0])
            {
                if (_tileOnBoard[0].GetTilePosition().GetPosX() > 1)
                {
                    thisTile.SetTilePosition(_tileOnBoard[0].GetTilePosition().GetPosX() - 1, _tileOnBoard[0].GetTilePosition().GetPosY());
                }
                else if (_tileOnBoard[0].GetTilePosition().GetPosX() == 1)
                {
                    thisTile.SetTilePosition(_tileOnBoard[0].GetTilePosition().GetPosX(), _tileOnBoard[0].GetTilePosition().GetPosY() + 1);
                }
                _validSideTiles[0] = thisTile.GetTileSideB();
                thisTile.FlipTiles();
                _tileOnBoard.Insert(0, thisTile);
                return true;
            }
            else if (thisTile.GetTileSideB() == _validSideTiles[0])
            {
                if (_tileOnBoard[0].GetTilePosition().GetPosX() > 1)
                {
                    thisTile.SetTilePosition(_tileOnBoard[0].GetTilePosition().GetPosX() - 1, _tileOnBoard[0].GetTilePosition().GetPosY());
                }
                else if (_tileOnBoard[0].GetTilePosition().GetPosX() == 1)
                {
                    thisTile.SetTilePosition(_tileOnBoard[0].GetTilePosition().GetPosX(), _tileOnBoard[0].GetTilePosition().GetPosY() + 1);
                }
                _validSideTiles[0] = thisTile.GetTileSideA();
                _tileOnBoard.Insert(0, thisTile);
                return true;
            }
        }
        return false;
    }
    private bool RightValidSide(Tile thisTile)
    {
        if (thisTile.GetTileSideA() == _validSideTiles[1])
        {
            if (_tileOnBoard[^1].GetTilePosition().GetPosX() < _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(_tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosX() + 1, _tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosY());
            }
            else if (_tileOnBoard[^1].GetTilePosition().GetPosX() == _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(_tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosX(), _tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosY() - 1);
            }
            _validSideTiles[1] = thisTile.GetTileSideB();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[1])
        {
            if (_tileOnBoard[^1].GetTilePosition().GetPosX() < _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(_tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosX() + 1, _tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosY());
            }
            else if (_tileOnBoard[^1].GetTilePosition().GetPosX() == _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(_tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosX(), _tileOnBoard[_tileOnBoard.Count - 1].GetTilePosition().GetPosY() - 1);
            }
            _validSideTiles[1] = thisTile.GetTileSideA();
            thisTile.FlipTiles();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        return false;
    }
    private bool ValidateTopAndButtomSide()
    {
        if (_verticalTileOnBoard.Count == 0 && _tileOnBoard.Count >= 3)
        {
            foreach (var tile in _tileOnBoard)
            {
                if (tile.GetTileOrientation() == TileOrientation.vertical)
                {
                    if (tile != _tileOnBoard[0] && tile != _tileOnBoard[_tileOnBoard.Count - 1])
                    {
                        int validTopSide = tile.GetTileSideA();
                        _validSideTiles.Add(validTopSide);
                        int validButtomSide = tile.GetTileSideB();
                        _validSideTiles.Add(validButtomSide);
                        _verticalTileOnBoard.Add(tile);
                        _tileOnBoard.Remove(tile);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool TopValidSide(Tile thisTile)
    {
        if (thisTile.GetTileSideA() == _validSideTiles[3])
        {
            thisTile.SetTilePosition(_verticalTileOnBoard[0].GetTilePosition().GetPosX(), _verticalTileOnBoard[_verticalTileOnBoard.Count - 1].GetTilePosition().GetPosY() + 1);
            _validSideTiles[3] = thisTile.GetTileSideB();
            // thisTile.FlipTiles();
            _verticalTileOnBoard.Add(thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[3])
        {
            thisTile.SetTilePosition(_verticalTileOnBoard[0].GetTilePosition().GetPosX(), _verticalTileOnBoard[_verticalTileOnBoard.Count - 1].GetTilePosition().GetPosY() + 1);
            _validSideTiles[3] = thisTile.GetTileSideA();
            _verticalTileOnBoard.Add(thisTile);
            return true;
        }
        return false;
    }
    private bool BottomValidSide(Tile thisTile)
    {
        if (thisTile.GetTileSideA() == _validSideTiles[2])
        {
            thisTile.SetTilePosition(_verticalTileOnBoard[0].GetTilePosition().GetPosX(), _verticalTileOnBoard[0].GetTilePosition().GetPosY() - 1);
            _validSideTiles[2] = thisTile.GetTileSideB();
            _verticalTileOnBoard.Insert(0, thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[2])
        {
            thisTile.SetTilePosition(_verticalTileOnBoard[0].GetTilePosition().GetPosX(), _verticalTileOnBoard[0].GetTilePosition().GetPosY() - 1);
            _validSideTiles[2] = thisTile.GetTileSideA();
            // thisTile.FlipTiles();
            _verticalTileOnBoard.Insert(0, thisTile);
            return true;
        }
        return false;
    }
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
    public Board GetBoard()
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
    public bool IsEnded()
    {
        // _currentPlayer = _players[0];
        if (_board == null || _players == null || _playersResource == null)
        {
            return false;
        }
        if (GameEndWithZeroTile())
        {
            return true;
        }
        if (_boneyard.tilesOnBoneyard.Count == 0 && _validSideTiles.Count >= 2)
        {
            if (GameEndWithNoSameTiles(_validSideTiles[0]) && GameEndWithNoSameTiles(_validSideTiles[1])
            && GameEndWithNoSameTiles(_validSideTiles[2]) && GameEndWithNoSameTiles(_validSideTiles[3]))
            {
                return true;
            }
            return false;
        }
        return false;
    }
    private bool GameEndWithZeroTile()
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
    //player hint move
    public bool ValidMove(IPlayer player)
    {
        foreach (var thisTile in _playersResource[player])
        {
            if (_tileOnBoard.Count == 0)
            {
                return true;
            }
            if (_verticalTileOnBoard.Count == 0)
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
            else if (thisTile.GetTileSideA() == _validSideTiles[2] || thisTile.GetTileSideB() == _validSideTiles[2])
            {
                return true;
            }
            else if (thisTile.GetTileSideA() == _validSideTiles[3] || thisTile.GetTileSideB() == _validSideTiles[3])
            {
                return true;
            }
        }
        return false;
    }
    //menentukan end game
    public int PlayerTileCount(IPlayer player)
    {
        if (_playersResource[player] == null)
        {
            return 0;
        }
        int hasil = 0;
        foreach (var tile in _playersResource[player])
        {
            hasil += tile.GetTileSideA();
            hasil += tile.GetTileSideB();
        }
        return hasil;
    }
}