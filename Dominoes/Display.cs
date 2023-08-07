namespace Dominoes;
using System.Collections.Generic;
public class Display
{
    public static void DisplayBoneyard(List<List<int>> boneyard)
    {
        foreach (var list in boneyard)
        {
            int sideA = list[0];
            int sideB = list[1];
            Console.Write($"{sideA}|{sideB} ");

        }
        Console.WriteLine();
    }
    public static void DisplayPlayerTiles(List<Tile> tiles)
    {
        foreach (var tile in tiles)

            Console.Write($"{tile.GetTileSideA()}|{tile.GetTileSideB()} ");
        Console.WriteLine();
    }

    public static void DisplayTilesOnBoard(List<Tile> tiles)
    {
        int i = 1;
        foreach (var tile in tiles)
        {
            Console.WriteLine($"{i}Tile value : {tile.GetTileSideA()}|{tile.GetTileSideB()} Tile Orentation : {tile.GetTileOrientation()} Position : {tile.GetTilePosition().GetPosX()}, {tile.GetTilePosition().GetPosY()}");
            i += 1;
        }

    }
    public static void DisplayBoard(List<Tile> tiles)
    {
        int size = 200;
        string[,] matrix = new string[size, size];
        for (int row = 0; row < size; row++)
        {
            for (int columb = 0; columb < size; columb++)
            {
                matrix[row, columb] = " ";
            }
        }
        foreach (var tile in tiles)
        {
            Position position = tile.GetTilePosition();
            int x = position.GetPosX();
            int y = position.GetPosY();
            if (tile.GetTileOrientation() == TileOrientation.horizontal)
            {

                matrix[y, x - 1] = $"{tile.GetTileSideA()}";
                matrix[y, x] = $"|";
                matrix[y, x + 1] = $"{tile.GetTileSideB()} ";
            }
            else if (tile.GetTileOrientation() == TileOrientation.vertical)
            {
                matrix[y + 1, x] = $"{tile.GetTileSideA()}";
                matrix[y, x] = $"-";
                matrix[y - 1, x] = $"{tile.GetTileSideB()} ";
            }
        }

        for (int row = 0; row < size; row++)
        {
            for (int columb = 0; columb < size; columb++)
            {
                Console.Write(matrix[row, columb]);
            }
            Console.WriteLine();
        }
    }
}

