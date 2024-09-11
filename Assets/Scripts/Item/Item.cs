using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; }

    [SerializeField] SpriteRenderer spriteRenderer;

    bool isGrabbable;
    public bool IsGrabbable { get => isGrabbable; set => isGrabbable = value; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (itemSO.InteractionType == eInteractionType.Grabbable)
        {
            isGrabbable = true;
        }
    }

    public void Interact()
    {
        if (itemSO.InteractionType == eInteractionType.Static)
        {
            Debug.Log("Interact with a static Object");
            spriteRenderer.sprite = itemSO.InteractedSprite;
        }
    }
}
