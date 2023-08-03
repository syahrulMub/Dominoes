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

        // Display.DisplayPlayerTiles(player1Tiles);
        // Display.DisplayPlayerTiles(player2Tiles);
        List<List<int>>? availableBoneyard = boneyard.tilesOnBoneyard;
        Display.DisplayBoneyard(availableBoneyard);
        game1.GenerateTiles(player1, 12);
        game1.GenerateTiles(player2, 12);

        while (!game1.IsEnded())
        {

            List<Tile> player1Tiles = game1.GetPlayerTiles(player1);
            List<Tile> player2Tiles = game1.GetPlayerTiles(player2);
            bool player1ValidMove = false;
            foreach (var tile in player1Tiles)
            {
                if (game1.MakeMove(tile))
                {
                    player1ValidMove = true;
                    break;
                }
            }
            if (player1ValidMove == false)
            {
                {
                    game1.MoveToNextPlayer();
                }
            }
            bool player2ValidMove = false;
            foreach (var tile in player2Tiles)
            {
                if (game1.MakeMove(tile))
                {
                    player2ValidMove = true;
                    break;
                }
            }
            if (player2ValidMove == false)
            {
                game1.MoveToNextPlayer();
            }
        }
        Display.DisplayTilesOnBoard(game1.GetTileOnBoard());
    }
}