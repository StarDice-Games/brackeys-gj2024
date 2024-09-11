using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float interactDistance = 3f;

    private Item grabbedItem;
    public Item GrabbedItem { get => grabbedItem; }

    [SerializeField]
    private LayerMask layerMask;

    public void Interact(Vector2 vectorDirection)
    {
        Debug.DrawRay(rayPoint.position, vectorDirection * interactDistance, Color.green, 3f);
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, vectorDirection, interactDistance, layerMask);

        if (hitInfo.collider != null)
        {
            // Interact with an item
            if (hitInfo.collider.TryGetComponent(out Item item))
            {
                // if Grabbable
                // Pick up item
                // Check if Player has no Item in hand
                if (grabbedItem == null)
                {
                    // Check if the item is grabbable
                    if (item.IsGrabbable)
                    {
                        grabbedItem = item;
                        grabbedItem.transform.position = grabPoint.position;
                        grabbedItem.transform.SetParent(transform);
                    }
                }

                // if Static
                if (item)
                {
                    item.Interact();
                }
            }

            // Interact with an item place area
            if (hitInfo.collider.TryGetComponent(out ItemPlace itemPlace))
            {
                if (grabbedItem != null)
                {
                    if (itemPlace.CanAttachItem())
                    {
                        // Check if the Item is in the right place
                        if (itemPlace.ItemType == grabbedItem.ItemSO.ItemType)
                        {
                            // Attach grabbed item to the anchor point
                            itemPlace.AddItem(grabbedItem);

                            // Can't grab the item anymore
                            grabbedItem.IsGrabbable = false;

                            // Remove placed item from Player
                            grabbedItem.transform.SetParent(null);
                            grabbedItem = null;

                            return;
                        }
                    }
                }
            }
        }
    }
}
