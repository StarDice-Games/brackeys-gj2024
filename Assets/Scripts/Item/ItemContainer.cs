using UnityEngine;

public class ItemContainer : Item
{
    [SerializeField] Transform anchorPoint;
    private Item currentAttachedItem;

    public Item CurrentAttachedItem { get => currentAttachedItem; }

    public void AttachItem(Item itemToAttach)
    {
        currentAttachedItem = itemToAttach;
        itemToAttach.transform.position = anchorPoint.transform.position;
    }
}
