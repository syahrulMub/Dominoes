using System.Collections.Generic;
using Dominoes;
using DisplayDominoes;

class Program
{
    public static void Main()
    {
        GameRunner game1 = new GameRunner();

        game1.gameEnded += handleGameEnded;

        IPlayer player1 = new Player();
        player1.SetID(912);
        player1.SetName("syahrul");

        IPlayer player2 = new Player();
        player2.SetID(811);
        player2.SetName("benzema");

        IPlayer player3 = new Player();
        player3.SetID(411);
        player3.SetName("ozil");


        Boneyard boneyard = new Boneyard(6);
        IBoard board = new Board();
        board.SetBoardSize(12);

        game1.AddBondyard(boneyard);
        game1.AddBoard(board);
        game1.AddPlayer(player1);
        game1.AddPlayer(player2);
        game1.AddPlayer(player3);

        game1.GenerateTiles(player1, 2);
        game1.GenerateTiles(player2, 2);
        game1.GenerateTiles(player3, 2);

        Console.WriteLine("=====Game Start=====");
        game1.SetCurrentPlayer(0);
        while (!game1.IsEnded())
        {
            game1.GetPlayerTiles(player1);
            game1.GetPlayerTiles(player2);
            game1.GetPlayerTiles(player3);
            Console.Clear();
            Display.DrawBoard(board, game1.GetTileOnBoard(), game1.GetTileVerticalOnBoard());
            Console.WriteLine("=========================================");
            Console.WriteLine($"Now is {game1.GetCurrentPlayer().GetName()} Turn");
            Console.WriteLine("=========================================\n");
            Display.DisplayPlayerTiles(game1.GetPlayerTiles(game1.GetCurrentPlayer()));
            if (!game1.ValidMove(game1.GetCurrentPlayer()))
            {
                if (boneyard.GetTilesOnBoneyard()?.Count != 0)
                {
                    Console.WriteLine("you did't have same card");
                    Console.WriteLine("please pick card on boneyard");
                    game1.GenerateTiles(game1.GetCurrentPlayer(), 1);
                    Console.Write("press any key to move next player");
                    Console.ReadKey();
                    game1.MoveToNextPlayer();

                }
                else if (boneyard.GetTilesOnBoneyard()?.Count == 0)
                {
                    Console.WriteLine("you did't have same card");
                    Console.WriteLine("All tiles in boneyard already taken");
                    Console.Write("press any key to move next player");
                    Console.ReadKey();
                    game1.MoveToNextPlayer();
                }
            }
            else if (game1.ValidMove(game1.GetCurrentPlayer()))
            {
                Console.Write("Enter the tile by index (from 0) to place your tile on board : ");
                int setTilesOnBoard;
                do
                {
                    setTilesOnBoard = int.Parse(Console.ReadLine());
                    if (setTilesOnBoard < 0 || setTilesOnBoard >= game1.GetPlayerTiles(game1.GetCurrentPlayer()).Count)
                    {
                        Console.WriteLine("invalid index, please input valid index");
                    }

                }
                while (setTilesOnBoard < 0 || setTilesOnBoard >= game1.GetPlayerTiles(game1.GetCurrentPlayer()).Count);

                Console.WriteLine("Choose placement direction:");
                Console.WriteLine("1. Left");
                Console.WriteLine("2. Right");
                Console.WriteLine("3. Top");
                Console.WriteLine("4. buttom");
                Console.Write("Enter your choice: ");
                int placementChoice = int.Parse(Console.ReadLine());

                Tile selectedTile = game1.GetPlayerTiles(game1.GetCurrentPlayer())[setTilesOnBoard];
                if (placementChoice == 1)
                {
                    game1.MakeMove(selectedTile, 1);

                }
                else if (placementChoice == 2)
                {
                    game1.MakeMove(selectedTile, 2);


                }
                else if (placementChoice == 3)
                {
                    game1.MakeMove(selectedTile, 3);

                }
                else if (placementChoice == 4)
                {
                    game1.MakeMove(selectedTile, 4);

                }
                else
                {
                    Console.WriteLine("invalid choice, please enter a valid option");
                }
                Console.ReadKey();
            }
        }
        void handleGameEnded(object? sender, EventArgs e)
        {
            Console.WriteLine("Game Has Ended");
            Console.WriteLine("==============");
            int ranking = 1;
            foreach (var player in game1.GetLeaderBoard())
            {
                Console.WriteLine($"{ranking}. {player.Key.GetName()}\ttotal tile on hand : {player.Value}");
                ranking++;
            }
            Console.WriteLine("==============");
        }
    }
}