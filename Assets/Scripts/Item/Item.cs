using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; }

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    bool isGrabbable;
    public bool IsGrabbable { get => isGrabbable; set => isGrabbable = value; }
    public bool IsInteractable { get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
            boxCollider2D.enabled = false;
        }
    }

    public void HoverInteract()
    {
        Debug.Log("Hover interaction " + gameObject.name);
    }

    public void ExitInteract()
    {
        Debug.Log("Exit interaction " + gameObject.name);
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
