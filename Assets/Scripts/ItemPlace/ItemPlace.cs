using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour, IInteractable
{
    [SerializeField] eItemType itemType;
    [SerializeField] Transform anchorPoint;

    [SerializeField] int maxItems;
    [SerializeField] List<Item> currentItems;

    [SerializeField] Color greenColor, redColor;

    public eItemType ItemType { get => itemType; }
    public bool IsInteractable { get; set; }

    public bool CanAttachItem()
    {
        return currentItems.Count < maxItems;
    }

    public void AddItem(Item item)
    {
        currentItems.Add(item);
        item.transform.position = anchorPoint.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!CanAttachItem())
        {
            HideCheckItemSprite();
            return;
        }

        if (collision.CompareTag("InteractionDetector"))
        {
            if (collision.transform.parent.parent.TryGetComponent(out InteractionDetector interactorDetector))
            {
                if (interactorDetector.GrabbedItem)
                {
                    UpdateVisual(interactorDetector.GrabbedItem);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            HideCheckItemSprite();
        }
    }

    public void HideCheckItemSprite()
    {
        if (anchorPoint.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.color = new Color(0, 0, 0, 0f);
        }
    }

    public void UpdateVisual(Item item)
    {
        if (anchorPoint.TryGetComponent(out SpriteRenderer sprite))
        {
            if (item.ItemSO.ItemType == ItemType)
            {
                sprite.color = greenColor;
            }
            else
            {
                sprite.color = redColor;
            }
        }
    }

    public bool CheckItemTypeMatch(Item item)
    {
        return item.ItemSO.ItemType == ItemType;
    }

    public void Interact()
    {
    }

    public void HoverInteract()
    {
    }

    public void ExitInteract()
    {
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }


}
