using System.Collections.Generic;
using System;
using Dominoes;

class Program
{
    public static void Main()
    {
        GameRunner game1 = new GameRunner();
        IPlayer player1 = new Player();
        player1.SetID(912);
        player1.SetName("syahrul");

        IPlayer player2 = new Player();
        player2.SetID(811);
        player2.SetName("syahril");

        Boneyard boneyard = new Boneyard(6);
        game1.AddBondyard(boneyard);
        game1.AddPlayer(player1);
        game1.AddPlayer(player2);

        List<List<int>>? availableBoneyard = boneyard.tilesOnBoneyard;

        game1.GenerateTiles(player1, 2);
        game1.GenerateTiles(player2, 2);

        List<Tile> player1Tiles = game1.GetPlayerTiles(player1);
        List<Tile> player2Tiles = game1.GetPlayerTiles(player2);

        Display.DisplayPlayerTiles(player1Tiles);

        Console.WriteLine();

        Display.DisplayPlayerTiles(player2Tiles);

        Console.WriteLine();

        Display.DisplayBoneyard(availableBoneyard);
        game1.StartGame();


        Console.ReadLine();
    }
}