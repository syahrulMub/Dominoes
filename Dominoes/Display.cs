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
            Console.WriteLine($"{i}Tile value : {tile.GetTileSideA()}|{tile.GetTileSideB()} Tile Orentation : {tile.GetTileOrientation()}");
            i += 1;
        }

    }
    public static void DisplayBoard(List<Tile> tiles)
    {
        int size = 30;
        string[,] matrix = new string[size, size];
        for (int row = 0; row < size; row++)
        {
            for (int columb = 0; columb < size; columb++)
            {
                matrix[row, columb] = " ";
            }
        }
        int ax = 0;
        int ay = 0;
        int px = 0;
        int py = 1;
        int bx = 0;
        int by = 2;

        if (tiles[0].GetTileOrientation() == TileOrientation.horizontal)
        {
            matrix[size / 2 + ax, size / 2 + ay] = $"{tiles[0].GetTileSideA()}";
            matrix[size / 2 + px, size / 2 + py] = "|";
            matrix[size / 2 + bx, size / 2 + by] = $"{tiles[0].GetTileSideB()}";
        }
        else if (tiles[0].GetTileOrientation() == TileOrientation.vertical)
        {
            matrix[size / 2 + ax, size / 2 + ay] = $"{tiles[0].GetTileSideA()}";
            matrix[size / 2 + px, size / 2 + py] = "|";
            matrix[size / 2 + bx, size / 2 + by] = $"{tiles[0].GetTileSideB()}";
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

