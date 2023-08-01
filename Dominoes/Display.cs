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
    }
    public static void DisplayPlayerTiles(List<Tile> tiles)
    {
        foreach (var tile in tiles)

            Console.Write($"{tile.GetTileSideA()}|{tile.GetTileSideB()} ");
    }
}

