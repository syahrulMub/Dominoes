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

        IPlayer player3 = new Player();
        player3.SetID(411);
        player3.SetName("oziel");


        Boneyard boneyard = new Boneyard(5);
        Board board = new Board();
        board.SetBoard(50, 50);

        game1.AddBondyard(boneyard);
        game1.AddBoard(board);
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
        game1.GenerateTiles(player1, 4);
        game1.GenerateTiles(player2, 4);
        game1.GenerateTiles(player3, 4);

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
            Console.ReadKey();
        }
        // Display.DisplayBoard(game1.GetTileOnBoard());
        // Display.DisplayTilesOnBoard(game1.GetTileOnBoard());
        foreach (var player in game1.GetPlayers())
        {
            int countEnd = game1.PlayerTileCount(player);
            Console.WriteLine($"{player.GetName()} tiles on hand : {countEnd}");
        }
    }
}