using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    [SerializeField] eItemType itemType;
    [SerializeField] Transform anchorPoint;

    [SerializeField] int maxItems;
    [SerializeField] List<Item> currentItems;

    [SerializeField] Color greenColor, redColor;

    public eItemType ItemType { get => itemType; }

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
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Interactor interactor))
            {
                if (interactor.GrabbedItem)
                {
                    CheckItemTypeMatch(interactor.GrabbedItem);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideCheckItemSprite();
    }

    public void HideCheckItemSprite()
    {
        if (anchorPoint.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.color = new Color(0, 0, 0, 0f);
        }
    }

    public void CheckItemTypeMatch(Item item)
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
}
