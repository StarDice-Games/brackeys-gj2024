using UnityEngine;

public enum eItemType
{
    Static,
    Moveable
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName { get => itemName; }

    [SerializeField] SpriteRenderer sprite;
    public SpriteRenderer Sprite { get => sprite; }

    [SerializeField] eItemType itemType;
    internal eItemType ItemType { get => itemType; }
}
