namespace Dominoes.Test;
using NUnit.Framework;
public class GameRunnerTest
{
    private GameRunner _gameRunner;
    [SetUp]
    public void Setup()
    {
        _gameRunner = new GameRunner();
    }

    [Test]
    public void FirstValidMoveTestVertical()
    {
        //arrange
        IPlayer player1 = new Player();
        _gameRunner.AddPlayer(player1);
        Tile tile = new Tile(1, 1);
        _gameRunner.GetPlayerTiles(player1).Add(tile);
        _gameRunner.SetCurrentPlayer(0);

        //act
        _gameRunner.MakeMove(tile, 6);
        List<Tile> listTile = _gameRunner.GetTileOnBoard();

        Tile firstTile = listTile[0];
        TileOrientation expect = TileOrientation.vertical;
        TileOrientation actual = firstTile.GetTileOrientation();

        //assert
        Assert.AreEqual(expect, actual);

    }
    [Test]
    public void SecondMoveTest()
    {
        //arrange
        IPlayer player1 = new Player();
        IPlayer player2 = new Player();
        _gameRunner.AddPlayer(player1);
        _gameRunner.AddPlayer(player2);
        Tile tile1 = new Tile(1, 1);
        Tile tile2 = new Tile(1, 3);
        Tile tile3 = new Tile(1, 5);
        _gameRunner.GetPlayerTiles(player1).Add(tile1);
        _gameRunner.GetPlayerTiles(player1).Add(tile2);
        _gameRunner.GetPlayerTiles(player2).Add(tile3);
        _gameRunner.SetCurrentPlayer(0);

        //act


    }
    [Test]
    public void TestGameEndWithZeroTile()
    {
        //arrange
        IPlayer playerWithZeroTile = new Player();
        IPlayer playerWithTile = new Player();
        Boneyard boneyard = new(4);
        _gameRunner.AddPlayer(playerWithZeroTile);
        _gameRunner.AddPlayer(playerWithTile);
        _gameRunner.AddBondyard(boneyard);
        _gameRunner.GenerateTiles(playerWithTile, 1);

        //act
        bool result = _gameRunner.GameEndWithZeroTile();

        //assert
        Assert.IsTrue(result);

    }
    [Test]
    public void TestBlockModeGame()
    {
        //arrage
        IPlayer player1 = new Player();
        IPlayer player2 = new Player();
        Boneyard boneyard = new(6);
        _gameRunner.AddPlayer(player1);
        _gameRunner.AddPlayer(player2);
        _gameRunner.AddBondyard(boneyard);



        Tile tile1 = new Tile(3, 4);
        Tile tile2 = new Tile(2, 5);
        Tile tile3 = new Tile(1, 1);


        //act
        _gameRunner.SetCurrentPlayer(0);
        _gameRunner.SetGameMode(GameMode.blockMode);

        _gameRunner.GetPlayerTiles(player1).Add(tile1);
        _gameRunner.GetPlayerTiles(player1).Add(tile2);
        _gameRunner.GetPlayerTiles(player2).Add(tile3);

        _gameRunner.MakeMove(tile1, 1);


        //assert
        Assert.IsTrue(_gameRunner.IsEnded());

    }
}