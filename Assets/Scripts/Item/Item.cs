using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; }

    [SerializeField] SpriteRenderer spriteRenderer;

    private bool isGrabbable;
    public bool IsGrabbable { get => isGrabbable; set => isGrabbable = value; }
    public bool IsInteractable { get; set; }

    private bool completed = false;

    [SerializeField] AudioClip audioClip;

    [HideInInspector]
    public Task associatedTask;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        IsInteractable = true;

        if (itemSO.InteractionType == eInteractionType.Grabbable)
        {
            isGrabbable = true;
        }
    }

    public void Interact()
    {
        IsInteractable = false;

        AudioController.Instance.PlaySound(audioClip.name, true, "sfx");

        if (itemSO.ItemType == eItemType.Door)
        {
            ToggleSprite(true);
            StartCoroutine(EventsManager.Instance.StartingSecondPhase());
        }

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

        if (GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        // static object still detected interactable, disable Script to avoid this behaviour
        this.enabled = false;
    }

    private void InteractWithStaticObject()
    {
        if (!completed)
        {
            Debug.Log("Interacting with a static object.");
            spriteRenderer.sprite = itemSO.InteractedSprite;
            completed = true;
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void GrabObject() // Not used
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

    public void HoverInteract()
    {

    }

    public void ExitInteract()
    {

    }

    public void ToggleSprite(bool isActive)
    {
        spriteRenderer.enabled = isActive;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
