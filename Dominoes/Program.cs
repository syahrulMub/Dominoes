using System.Collections.Generic;
using System;
namespace Dominoes;

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
        player2.SetName("benzema");

        IPlayer player3 = new Player();
        player3.SetID(411);
        player3.SetName("oziel");


        Boneyard boneyard = new Boneyard(7);
        game1.AddBondyard(boneyard);
        game1.AddPlayer(player1);
        game1.AddPlayer(player2);
        game1.AddPlayer(player3);

        // Display.DisplayPlayerTiles(player1Tiles);
        // Display.DisplayPlayerTiles(player2Tiles);
        Console.WriteLine(boneyard.tilesOnBoneyard.Count);
        if (boneyard.tilesOnBoneyard != null)
        {
            List<List<int>> availableBoneyard = boneyard.tilesOnBoneyard;
            Display.DisplayBoneyard(availableBoneyard);
        }
        game1.GenerateTiles(player1, 12);
        game1.GenerateTiles(player2, 12);
        game1.GenerateTiles(player3, 12);

        Console.WriteLine("====Game Start====");

        while (!game1.IsEnded())
        {
            List<Tile> player1Tiles = game1.GetPlayerTiles(player1);
            List<Tile> player2Tiles = game1.GetPlayerTiles(player2);
            List<Tile> player3Tiles = game1.GetPlayerTiles(player3);
            bool player1ValidMove = false;
            Console.WriteLine($"This is {player1.GetName()} Turn");
            foreach (var tile in player1Tiles)
            {
                if (game1.MakeMove(tile))
                {
                    player1ValidMove = true;
                    Console.WriteLine($"{player1.GetName()} Place {tile.GetTileSideA()}|{tile.GetTileSideB()} on Board");
                    break;
                }
            }
            if (player1ValidMove == false)
            {
                if (boneyard.tilesOnBoneyard.Count != 0)
                {
                    Console.WriteLine($"{player1.GetName()} Pick card on Boneyard");
                    game1.GenerateTiles(player1, 1);
                    game1.MoveToNextPlayer();
                }
                else
                {
                    Console.WriteLine($"{player1.GetName()} Skip The turn");
                    game1.MoveToNextPlayer();
                }
            }
            bool player2ValidMove = false;
            Console.WriteLine($"This is {player2.GetName()} Turn");
            foreach (var tile in player2Tiles)
            {
                if (game1.MakeMove(tile))
                {
                    player2ValidMove = true;
                    Console.WriteLine($"{player2.GetName()} Place {tile.GetTileSideA()}|{tile.GetTileSideB()} on Board");
                    break;
                }
            }
            if (player2ValidMove == false)
            {
                if (boneyard.tilesOnBoneyard.Count != 0)
                {
                    Console.WriteLine($"{player2.GetName()} Pick card on Boneyard");
                    game1.GenerateTiles(player2, 1);
                    game1.MoveToNextPlayer();
                }
                else
                {
                    Console.WriteLine($"{player2.GetName()} Skip The turn");
                    game1.MoveToNextPlayer();
                }
            }

            bool player3ValidMove = false;
            Console.WriteLine($"This is {player3.GetName()} Turn");
            foreach (var tile in player3Tiles)
            {
                if (game1.MakeMove(tile))
                {
                    player3ValidMove = true;
                    Console.WriteLine($"{player3.GetName()} Place {tile.GetTileSideA()}|{tile.GetTileSideB()} on Board");
                    break;
                }
            }
            if (player3ValidMove == false)
            {
                if (boneyard.tilesOnBoneyard.Count != 0)
                {
                    Console.WriteLine($"{player3.GetName()} Pick card on Boneyard");
                    game1.GenerateTiles(player3, 1);
                    game1.MoveToNextPlayer();
                }
                else
                {
                    Console.WriteLine($"{player3.GetName()} Skip The turn");
                    game1.MoveToNextPlayer();
                }
            }
            Display.DisplayPlayerTiles(player1Tiles);
            Display.DisplayPlayerTiles(player2Tiles);
            Display.DisplayPlayerTiles(player3Tiles);
            foreach (var validNumber in game1._validSideTiles)
            {
                Console.WriteLine(validNumber);
            }
        }
        // Display.DisplayBoard(game1.GetTileOnBoard());
        // Display.DisplayTilesOnBoard(game1.GetTileOnBoard());
    }
}