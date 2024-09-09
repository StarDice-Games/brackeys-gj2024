using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource soundEffectSource;
    public AudioClip[] audioClips;         

    public void PlaySound(string clipName)
    {
        AudioClip clip = FindClipByName(clipName);
        if (clip != null)
        {
            soundEffectSource.clip = clip;
            soundEffectSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {clipName} not found!");
        }
    }

    public void StopSound()
    {
        if (soundEffectSource.isPlaying)
        {
            soundEffectSource.Stop();
        }
    }

    private AudioClip FindClipByName(string clipName)
    {
        foreach (var clip in audioClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }
}
