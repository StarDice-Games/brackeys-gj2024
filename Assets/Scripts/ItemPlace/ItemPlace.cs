using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour, IInteractable
{
    [SerializeField] eItemType itemType;
    [SerializeField] Transform anchorPoint;

    [SerializeField] int maxItems;
    [SerializeField] List<Item> currentItems;

    [SerializeField] Color itemHighlight;

    public eItemType ItemType { get => itemType; }
    public bool IsInteractable { get; set; }

    [SerializeField] AudioClip audioClip;

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
    }

    public void HideCheckItemSprite()
    {
        if (anchorPoint.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.color = new Color(0, 0, 0, 0f);
        }
    }

    public void HighlightPlace()
    {
        if (anchorPoint.TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.color = itemHighlight;
        }
    }

    public bool CheckItemTypeMatch(Item item)
    {
        return item.ItemSO.ItemType == ItemType;
    }

    public void Interact()
    {
        AudioController.Instance.PlaySound(audioClip.name, true, "sfx");
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

    public bool IsCompleted()
    {
        return false;
    }
}
