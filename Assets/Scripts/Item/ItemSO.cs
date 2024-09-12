using UnityEngine;

public enum eInteractionType
{
    Static,
    Grabbable
}

public enum eItemType
{
    None,
    Dishes,
    Laundry,
    Blood
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName { get => itemName; }

    [SerializeField] Sprite interactedSprite;
    public Sprite InteractedSprite { get => interactedSprite; }

    [SerializeField] eItemType itemType;
    public eItemType ItemType { get => itemType; }

    [SerializeField] eInteractionType interactionType;
    public eInteractionType InteractionType { get => interactionType; }
}
