using UnityEngine;

public enum eInteractionType
{
    Static,
    Grabbable,
    Cleanable
}

public enum eItemType
{
    None,
    BloodStain,
    Paint,
    Carpet,
    Couch,
    Door,
    Plant,
    DirtyClothes,
    Pillows,
    Dishes,
    Guest
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
