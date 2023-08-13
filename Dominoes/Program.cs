using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using Dominoes;
using DisplayDominoes;

class Program
{
    [Obsolete]
    public static async Task Main()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var nlogConfigPath = Path.Combine(currentDirectory, "nlog.config");
        LogManager.LoadConfiguration(nlogConfigPath);


        var logger = LogManager.GetCurrentClassLogger();

        logger.Info("program info");
        GameRunner game1 = new GameRunner();

        game1.gameEnded += handleGameEnded;
        game1.gameEnded += PlayerWin;
        game1.gameEnded += PlayerLose;

        IPlayer player1 = new Player();
        player1.SetID(912);
        player1.SetName("syahrul");

        IPlayer player2 = new Player();
        player2.SetID(811);
        player2.SetName("benzema");

        IPlayer player3 = new Player();
        player3.SetID(411);
        player3.SetName("mesut");

        Boneyard boneyard = new Boneyard(6);
        IBoard board = new Board();
        board.SetBoardSize(12);

        game1.AddBondyard(boneyard);
        game1.AddBoard(board);
        game1.AddPlayer(player1);
        game1.AddPlayer(player2);
        game1.AddPlayer(player3);

        game1.GenerateTiles(player1, 7);
        game1.GenerateTiles(player2, 7);
        game1.GenerateTiles(player3, 7);
        Console.WriteLine("Set your game mode : \n1. Draw Mode\n2. Block Mode");
        int pickGameMode;
        do
        {
            pickGameMode = int.Parse(Console.ReadLine());
            if (pickGameMode != 1 || pickGameMode != 2)
            {
                Console.WriteLine("please enter pick 1 or 2");
            }
        } while (pickGameMode != 1 && pickGameMode != 2);
        if (pickGameMode == 1)
        {
            game1.SetGameMode(GameMode.drawMode);
        }
        else if (pickGameMode == 2)
        {
            game1.SetGameMode(GameMode.blockMode);
        }

        Console.WriteLine("=====Game Start=====");
        game1.SetCurrentPlayer(0);
        while (!game1.IsEnded())
        {
            game1.GetPlayerTiles(player1);
            game1.GetPlayerTiles(player2);
            game1.GetPlayerTiles(player3);
            Console.Clear();
            Console.Write("waiting for validate turn and create board ");
            await Task.Delay(1000);
            Console.Write(". ");
            await Task.Delay(1000);
            Console.Write(". ");
            await Task.Delay(1000);
            Console.WriteLine(". ");
            await Task.Run(() => Display.DrawBoard(board, game1.GetTileOnBoard(), game1.GetTileVerticalOnBoard()));
            Console.WriteLine("=========================================");
            Console.WriteLine($"Now is {game1.GetCurrentPlayer().GetName()} Turn");
            Console.WriteLine("=========================================\n");
            Display.DisplayPlayerTiles(game1.GetPlayerTiles(game1.GetCurrentPlayer()));
            if (!game1.ValidMove(game1.GetCurrentPlayer()))
            {
                if (boneyard.GetTilesOnBoneyard()?.Count != 0 && game1.GetGameMode() == GameMode.drawMode)
                {
                    Console.WriteLine("you did't have same card");
                    Console.WriteLine("please pick card on boneyard");
                    game1.GenerateTiles(game1.GetCurrentPlayer(), 1);
                    Console.Write("press any key to move next player");
                    Console.ReadKey();
                    game1.MoveToNextPlayer();
                }
                else if (boneyard.GetTilesOnBoneyard()?.Count == 0 || game1.GetGameMode() == GameMode.blockMode)
                {
                    Console.WriteLine("you did't have same card");
                    if (game1.GetGameMode() == GameMode.drawMode)
                    {
                        Console.WriteLine("All tiles in boneyard already taken");
                    }
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
                Console.WriteLine("3. buttom");
                Console.WriteLine("4. Top");
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
            Console.WriteLine("Game was Ended");
            Console.WriteLine("==============");
            int ranking = 1;
            foreach (var player in game1.GetLeaderBoard())
            {
                Console.WriteLine($"{ranking}. {player.Key.GetName()}\ttotal tile on hand : {player.Value}");
                ranking++;
            }
            Console.WriteLine("==============");
        }
        void PlayerWin(object? sender, EventArgs e)
        {
            GameStatus status = GameStatus.winTheGame;
            IPlayer playerWin = game1.GetLeaderBoard()[0].Key;
            Console.WriteLine($"\nCongratulation {playerWin.GetName()}!!! you {status}\n");
        }
        void PlayerLose(object? sender, EventArgs e)
        {
            GameStatus status = GameStatus.loseTheGame;
            for (int i = 1; i < game1.GetLeaderBoard().Count; i++)
            {
                Console.WriteLine($"{game1.GetLeaderBoard()[i].Key.GetName()}, you {status}");
            }
        }

    }
}