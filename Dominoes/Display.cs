namespace Dominoes;
using System.Collections.Generic;
public class Display
{
    public static void DisplayPlayerTiles(List<Tile> tiles)
    {
        int tileIndex = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            Console.Write($"({i}) {tiles[tileIndex].GetTileSideA()}|{tiles[tileIndex].GetTileSideB()} ");
            tileIndex++;
        }
        Console.WriteLine();
    }
    public static void DrawBoard(Board board, List<Tile> tilesHorizontal, List<Tile> tilesVertical)
    {
        int cellSize = 5;
        int boardSize = board.GetBoardSize();

        Console.WriteLine($"Setting board boundary condition: {boardSize}");
        Console.WriteLine(new string('-', (cellSize + 1) * boardSize + 1));

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                bool tileFound = false;

                foreach (var tile in tilesHorizontal)
                {
                    int x = tile.GetTilePosition().GetPosX();
                    int y = tile.GetTilePosition().GetPosY();

                    if (x == j && y == i)
                    {
                        tileFound = true;

                        if (tile.GetTileOrientation() == TileOrientation.horizontal)
                        {
                            Console.Write($" {tile.GetTileSideA()}|{tile.GetTileSideB()} ");
                        }
                        else if (tile.GetTileOrientation() == TileOrientation.vertical)
                        {
                            Console.Write($" {tile.GetTileSideA()}/{tile.GetTileSideB()} ");
                        }
                        break;
                    }
                }

                if (!tileFound)
                {
                    foreach (var tile in tilesVertical)
                    {
                        int a = tile.GetTilePosition().GetPosX();
                        int b = tile.GetTilePosition().GetPosY();

                        if (a == j && b == i)
                        {
                            tileFound = true;

                            if (tile.GetTileOrientation() == TileOrientation.horizontal)
                            {
                                Console.Write($" {tile.GetTileSideA()}|{tile.GetTileSideB()} ");
                            }
                            else if (tile.GetTileOrientation() == TileOrientation.vertical)
                            {
                                Console.Write($" {tile.GetTileSideA()}/{tile.GetTileSideB()} ");
                            }
                            break;
                        }
                    }
                }

                if (!tileFound)
                {
                    Console.Write(new string(' ', cellSize));
                }

                Console.Write("|");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', (cellSize + 1) * boardSize + 1));
        }
        Console.WriteLine();
    }

}


