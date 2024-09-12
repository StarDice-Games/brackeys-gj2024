using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; }

    [SerializeField] SpriteRenderer spriteRenderer;

    private bool isGrabbable;
    public bool IsGrabbable { get => isGrabbable; set => isGrabbable = value; }

    private bool completed = false;

    [HideInInspector]
    public Task associatedTask;

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
        switch (itemSO.InteractionType)
        {
            case eInteractionType.Static:
                InteractWithStaticObject();
                break;

            case eInteractionType.Grabbable:
                GrabObject();
                break;

            case eInteractionType.Cleanable:
                CleanObject();
                break;
        }

        if (associatedTask != null)
        {
            associatedTask.CheckCompletion();
        }
    }

    private void InteractWithStaticObject()
    {
        if (!completed)
        {
            Debug.Log("Interacting with a static object.");
            spriteRenderer.sprite = itemSO.InteractedSprite;
            completed = true;
        }
    }

    private void GrabObject()
    {
        if (!completed)
        {
            Debug.Log("Grabbing the object.");
            completed = true;
        }
    }

    private void CleanObject()
    {
        if (!completed)
        {
            Debug.Log("Cleaning the object.");
            completed = true;
        }
    }

    public bool IsCompleted()
    {
        return completed;
    }
}
