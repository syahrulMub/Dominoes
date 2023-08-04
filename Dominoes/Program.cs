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
        player2.SetName("benzema");

        Boneyard boneyard = new Boneyard(6);
        game1.AddBondyard(boneyard);
        game1.AddPlayer(player1);
        game1.AddPlayer(player2);

        // Display.DisplayPlayerTiles(player1Tiles);
        // Display.DisplayPlayerTiles(player2Tiles);
        List<List<int>>? availableBoneyard = boneyard.tilesOnBoneyard;
        Display.DisplayBoneyard(availableBoneyard);
        game1.GenerateTiles(player1, 7);
        game1.GenerateTiles(player2, 7);

        Console.WriteLine("===Game Start===");
        while (!game1.IsEnded())
        {
            List<Tile> player1Tiles = game1.GetPlayerTiles(player1);
            List<Tile> player2Tiles = game1.GetPlayerTiles(player2);
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
                if (boneyard.tilesOnBoneyard.Count > 0)
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
                if (boneyard.tilesOnBoneyard.Count > 0)
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
        Console.ReadKey();
        }
        Display.DisplayTilesOnBoard(game1.GetTileOnBoard());
    }
}