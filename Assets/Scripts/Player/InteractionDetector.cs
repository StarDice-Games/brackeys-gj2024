using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private CircleCollider2D interactionCollider;
    private IInteractable nearestInteractable = null;

    [SerializeField]
    private Transform grabPoint;
    private Item grabbedItem;

    private Transform currentObject;

    public Item GrabbedItem { get => grabbedItem; }

    public event Action OnHoverInteraction;
    public event Action OnExitInteraction;

    public void Interact()
    {
        if (nearestInteractable != null)
        {
            if (nearestInteractable.GetTransform().TryGetComponent(out Item item))
            {
                if (item.IsGrabbable)
                {
                    Grab(item);
                }
                else
                {
                    if (grabbedItem == null)
                    {
                        nearestInteractable.Interact();
                    }
                }
            }
            else
            {
                PlaceItem();
            }
        }
        OnExitInteraction?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Cerca di ottenere il componente IInteractable dagli oggetti all'interno del CircleCollider2D
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            // Se non ci sono oggetti interagibili o l'oggetto attuale è più vicino, lo aggiorniamo
            if (nearestInteractable == null || IsCloser(interactable))
            {
                // Se è stato trovato un nuovo oggetto più vicino, aggiorna i feedback
                if (nearestInteractable != interactable)
                {
                    if (nearestInteractable != null)
                    {
                        // Chiamato per far sapere all'ultimo oggetto che siamo usciti dal suo range
                        nearestInteractable.ExitInteract();
                        OnExitInteraction?.Invoke();
                    }

                    // Imposta il nuovo oggetto più vicino
                    nearestInteractable = interactable;
                    currentObject = nearestInteractable.GetTransform();

                    if (currentObject.TryGetComponent(out Item item))
                    {
                        if (grabbedItem == null)
                        {
                            nearestInteractable.HoverInteract();
                            OnHoverInteraction?.Invoke();
                        }
                    }
                    else if (currentObject.TryGetComponent(out ItemPlace itemPlace))
                    {
                        if (grabbedItem && itemPlace.CanAttachItem() && itemPlace.CheckItemTypeMatch(grabbedItem))
                        {
                            nearestInteractable.HoverInteract();
                            OnHoverInteraction?.Invoke();
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            // Se l'oggetto uscente è quello che stavamo tenendo d'occhio, resettiamo
            if (nearestInteractable == interactable)
            {
                nearestInteractable.ExitInteract();
                OnExitInteraction?.Invoke();
                nearestInteractable = null;
                currentObject = null;
            }
        }
    }

    private bool IsCloser(IInteractable newInteractable)
    {
        // Controlla la distanza tra il Player e il nuovo oggetto
        float currentDistance = Vector2.Distance(transform.position, nearestInteractable.GetTransform().position);
        float newDistance = Vector2.Distance(transform.position, newInteractable.GetTransform().position);

        return newDistance < currentDistance;
    }

    private void Grab(Item item)
    {
        if (grabbedItem == null)
        {
            // Pick up item
            grabbedItem = item;
            grabbedItem.GetComponent<BoxCollider2D>().enabled = false;
            grabbedItem.transform.position = grabPoint.position;
            grabbedItem.transform.SetParent(grabPoint);
        }
    }

    private void PlaceItem()
    {
        // Interact with an item place area
        if (currentObject.TryGetComponent(out ItemPlace itemPlace))
        {
            // check if Player has Item in hand
            if (grabbedItem)
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
                    }
                }
            }
        }
    }
}
