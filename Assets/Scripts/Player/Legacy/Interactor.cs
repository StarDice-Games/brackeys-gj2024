using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    PlayerController player;

    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float interactDistance = 3f;

    GameObject currentItem;

    private Item grabbedItem;
    public Item GrabbedItem { get => grabbedItem; }

    [SerializeField]
    private LayerMask layerMask;

    public event Action OnHoverInteraction;
    public event Action OnExitInteraction;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Debug.DrawRay(rayPoint.position, player.LastPosition * interactDistance, Color.blue, 0.1f);
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, player.LastPosition, interactDistance, layerMask);

        if (hitInfo.collider)
        {
            currentItem = hitInfo.collider.gameObject;

            if (currentItem.TryGetComponent(out Item item))
            {
                OnHoverInteraction?.Invoke();
            }
            else if (currentItem.TryGetComponent(out ItemPlace itemPlace) && grabbedItem)
            {
                OnHoverInteraction?.Invoke();
            }
        }
        else
        {
            if (currentItem)
            {
                currentItem = null;
                OnExitInteraction?.Invoke();
            }
        }

        Debug.Log(currentItem);
    }

    public void Interact()
    {
        if (currentItem)
        {
            Grab();

            PlaceItem();
        }
    }

    private void PlaceItem()
    {
        // Interact with an item place area
        if (currentItem.TryGetComponent(out ItemPlace itemPlace))
        {
            // check if Player has Item in hand
            if (grabbedItem)
            {
                if (itemPlace.CanAttachItem())
                {
                    itemPlace.CheckItemTypeMatch(grabbedItem);

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
                    }
                }
            }
        }
    }

    private void Grab()
    {
        if (grabbedItem == null)
        {
            // Interact with an item
            if (currentItem.TryGetComponent(out Item item))
            {
                // Check if the item is grabbable
                if (item.IsGrabbable)
                {
                    // Pick up item
                    grabbedItem = item;
                    grabbedItem.GetComponent<BoxCollider2D>().enabled = false;
                    grabbedItem.transform.position = grabPoint.position;
                    grabbedItem.transform.SetParent(transform);
                }
                else // Static item
                {
                    item.Interact();
                }
            }
        }
    }
}
