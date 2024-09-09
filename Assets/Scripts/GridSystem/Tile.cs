using UnityEngine;
public class Tile
{
    public Vector2 position { get; private set; }
    public bool isOccupied { get; private set; }

    public Tile(Vector2 position)
    {
        this.position = position;
        this.isOccupied = false;
    }

    public void SetOccupied(bool occupied)
    {
        this.isOccupied = occupied;
    }
}
