using UnityEngine;

public class FlashVFX : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimation()
    {
        animator.SetTrigger("PlayFlash");
    }

    void OnThunder()
    {
        Debug.Log("Play thunder SFX");
    }
}
