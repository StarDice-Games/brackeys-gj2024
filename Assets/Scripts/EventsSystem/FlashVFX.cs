using UnityEngine;

public class FlashVFX : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioClip audioClip;

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
        AudioController.Instance.PlaySound(audioClip.name, true, "sfx");
    }
}
