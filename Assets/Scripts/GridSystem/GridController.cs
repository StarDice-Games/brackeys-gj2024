using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2 gridSize;
    private Grid grid;

    private void Start()
    {
        grid = new Grid(gridSize);
    }

    private Vector2 SnapToGrid(Vector2 position)
    {
        float snappedX = Mathf.Round(position.x);
        float snappedY = Mathf.Round(position.y);
        return new Vector2(snappedX, snappedY);
    }

    /*private void PlaceItem(Item item, Vector2 gridPosition)
    {
        Tile tile = grid.GetTileAtPosition(gridPosition);
        if (tile != null && !tile.isOccupied)
        {
            tile.SetOccupied(true);
            item.gridPosition = gridPosition;

            // Aggiungi qui logica per visualizzare l'oggetto nella scena
            Debug.Log($"Object {item.name} placed to {gridPosition}");
        }
        else
        {
            Debug.LogWarning("No tile found!");
        }
    }*/

    private void RemoveItem(Vector2 gridPosition)
    {
        Tile tile = grid.GetTileAtPosition(gridPosition);
        if (tile != null && tile.isOccupied)
        {
            tile.SetOccupied(false);
            Debug.Log($"Object removed from {gridPosition}");
        }
        else
        {
            Debug.LogWarning($"No object in {gridPosition}");
        }
    }

    private void UpdateGrid()
    {
        
    }
}
