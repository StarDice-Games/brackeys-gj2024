using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public Vector2 gridSize { get; private set; }
    public List<Tile> tiles { get; private set; }

    public Grid(Vector2 gridSize)
    {
        this.gridSize = gridSize;
        tiles = new List<Tile>();

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 tilePosition = new Vector2(x, y);
                tiles.Add(new Tile(tilePosition));
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        return tiles.Find(tile => tile.position == position);
    }
}
