namespace Dominoes;

public partial class GameRunner
{
    /// <summary>
    /// method where players will make move to the specified position they want with any tiles they have 
    /// if players place wrong valid side will be invalid move
    /// </summary>
    /// <param name="tile">any tile will be place on board when move valid</param>
    /// <param name="side">place direction they wont, in left, right, top, or buttom</param>
    /// <returns>true if valid move, false if invalid</returns>
    public bool MakeMove(Tile tile, int side)
    {
        List<Tile> currentPlayerTiles = _playersResource[_currentPlayer];
        if (_verticalTileOnBoard.Count == 0)
        {
            ValidateTopAndButtomSide();
        }
        if (FirstValidMove(tile))
        {
            currentPlayerTiles.Remove(tile);
            MoveToNextPlayer();
            return true;
        }
        //logic for nextmove
        else if (!FirstValidMove(tile) && _tileOnBoard != null)
        {
            if (side == 1)
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
            else if (side == 2)
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
                _validSideTiles.Add(tile.GetTileSideB());
                _validSideTiles.Add(tile.GetTileSideA());
            }
            else
            {
                tile.SetTileOrientation(TileOrientation.horizontal);
                _validSideTiles.Add(tile.GetTileSideA());
                _validSideTiles.Add(tile.GetTileSideB());
            }
            tile.SetTilePosition(_board.GetBoardSize() / 2, _board.GetBoardSize() / 2);
            _tileOnBoard.Add(tile);
            return true;
        }
        return false;
    }

    /// <summary>
    /// this 4 method is to set tile on left, right, top and left of dominoes game board
    /// validating with _validSIdeTile index 0, 1, 2 , 3
    /// if this tile side A or B have same number with every _validSIdeTile
    /// </summary>
    /// <param name="thisTile"></param>
    /// <returns>true if have same number</returns> <summary>
    /// 
    /// </summary>
    /// <param name="thisTile"></param>
    /// <returns>false if have not same number</returns>
    private bool LeftValidSide(Tile thisTile)
    {
        int leftTilePosX = _tileOnBoard[0].GetTilePosition().GetPosX();
        int leftTilePosY = _tileOnBoard[0].GetTilePosition().GetPosY();

        if (thisTile.GetTileSideA() == _validSideTiles[0])
        {
            if (leftTilePosX > 1)
            {
                thisTile.SetTilePosition(leftTilePosX - 1, leftTilePosY);
            }
            else if (leftTilePosX == 1)
            {
                thisTile.SetTilePosition(leftTilePosX, leftTilePosY + 1);
            }
            _validSideTiles[0] = thisTile.GetTileSideB();
            thisTile.FlipTiles();
            _tileOnBoard.Insert(0, thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[0])
        {
            if (leftTilePosX > 1)
            {
                thisTile.SetTilePosition(leftTilePosX - 1, leftTilePosY);
            }
            else if (leftTilePosX == 1)
            {
                thisTile.SetTilePosition(leftTilePosX, leftTilePosY + 1);
            }
            _validSideTiles[0] = thisTile.GetTileSideA();
            _tileOnBoard.Insert(0, thisTile);
            return true;
        }
        return false;
    }
    private bool RightValidSide(Tile thisTile)
    {
        int rightTilePosX = _tileOnBoard[^1].GetTilePosition().GetPosX();
        int rightTilePosY = _tileOnBoard[^1].GetTilePosition().GetPosY();

        if (thisTile.GetTileSideA() == _validSideTiles[1])
        {
            if (rightTilePosX < _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(rightTilePosX + 1, rightTilePosY);
            }
            else if (rightTilePosX == _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(rightTilePosX, rightTilePosY - 1);
            }
            _validSideTiles[1] = thisTile.GetTileSideB();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[1])
        {
            if (rightTilePosX < _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(rightTilePosX + 1, rightTilePosY);
            }
            else if (rightTilePosX == _board.GetBoardSize() - 1)
            {
                thisTile.SetTilePosition(rightTilePosX, rightTilePosY - 1);
            }
            _validSideTiles[1] = thisTile.GetTileSideA();
            thisTile.FlipTiles();
            _tileOnBoard.Add(thisTile);
            return true;
        }
        return false;
    }
    /// <summary>
    /// we will add two more validate side that can be insert tile in dominoes game
    /// player can move when any tiles in board have vertical orientation and left side and right side
    /// of this tile alredy exist
    /// </summary>
    /// <returns>this method will not valid when tiles on board less than 3 and vertical in leftes and rightes position</returns>
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
        int TopTilePosX = _verticalTileOnBoard[0].GetTilePosition().GetPosX();
        int TopTilePosY = _verticalTileOnBoard[^1].GetTilePosition().GetPosY();

        if (thisTile.GetTileSideA() == _validSideTiles[3])
        {
            thisTile.SetTilePosition(TopTilePosX, TopTilePosY - 1);
            _validSideTiles[3] = thisTile.GetTileSideB();
            // thisTile.FlipTiles();
            _verticalTileOnBoard.Add(thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[3])
        {
            thisTile.SetTilePosition(TopTilePosX, TopTilePosY - 1);
            _validSideTiles[3] = thisTile.GetTileSideA();
            _verticalTileOnBoard.Add(thisTile);
            return true;
        }
        return false;
    }
    private bool BottomValidSide(Tile thisTile)
    {
        int buttomTilePosX = _verticalTileOnBoard[0].GetTilePosition().GetPosX();
        int buttomTilePosY = _verticalTileOnBoard[0].GetTilePosition().GetPosY();
        if (thisTile.GetTileSideA() == _validSideTiles[2])
        {
            thisTile.SetTilePosition(buttomTilePosX, buttomTilePosY + 1);
            _validSideTiles[2] = thisTile.GetTileSideB();
            _verticalTileOnBoard.Insert(0, thisTile);
            return true;
        }
        else if (thisTile.GetTileSideB() == _validSideTiles[2])
        {
            thisTile.SetTilePosition(buttomTilePosX, buttomTilePosY + 1);
            _validSideTiles[2] = thisTile.GetTileSideA();
            // thisTile.FlipTiles();
            _verticalTileOnBoard.Insert(0, thisTile);
            return true;
        }
        return false;
    }

}