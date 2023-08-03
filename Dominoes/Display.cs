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
}

