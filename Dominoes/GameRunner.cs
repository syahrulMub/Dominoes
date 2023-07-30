using System;
using System.Collections.Generic;
namespace Dominoes;

public class GameRunner
{

    //variabel that game runner needed
    public Dictionary<IPlayer, List<Tile>> _playersResource;
    public Boneyard _boneyard;

    //constructor game runner
    public GameRunner()
    {
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _boneyard = new Boneyard();
    }

    //constructor for adding game maker data
    public GameRunner(IPlayer player, List<Tile> tiles)
    {
        _playersResource = new Dictionary<IPlayer, List<Tile>>();
        _playersResource.Add(player, tiles);
        _boneyard = new Boneyard();
    }

    //method for create player
    public bool AddPlayer(IPlayer player)
    {
        if (player != null)
        {
            List<Tile> tilesPlayer = new();
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
    //method for shuffle tiles domino

    //checkapakah bounyard available

    //method for deal domino tiles to every player 

    //method for access every players tiles
    public List<Tile> GetPlayerTiles(IPlayer player)
    {
        return _playersResource[player];
    }

    //method for start game
    public void StartGame()
    {

    }
}

