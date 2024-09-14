using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    private AudioSource musicSource;
    private AudioSource ambientSource;
    private AudioSource sfxSource;

    [SerializeField] private float fadeDuration = 1f;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> audioClipDictionary;

    float startingMusicVolume;
    float startingAmbientVolume;
    float startingSFXVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            InitializeAudioClipDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startingMusicVolume = musicSource.volume;
        startingAmbientVolume = ambientSource.volume;
        startingSFXVolume = sfxSource.volume;
    }

    private void InitializeAudioSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void InitializeAudioClipDictionary()
    {
        audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClips)
        {
            if (!audioClipDictionary.ContainsKey(clip.name))
            {
                audioClipDictionary.Add(clip.name, clip);
            }
        }
    }

    public void PlaySound(string clipName, bool oneShot = true, string sourceType = "sfx", float volume = 1)
    {
        if (audioClipDictionary.ContainsKey(clipName))
        {
            AudioClip clip = audioClipDictionary[clipName];
            AudioSource source = GetAudioSourceByType(sourceType);

            if (oneShot)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                source.clip = clip;
                source.Play();
                source.volume = volume;
            }
        }
        else
        {
            Debug.LogWarning($"Clip {clipName} not found in AudioController.");
        }
    }

    public void StopSound(string sourceType = "sfx")
    {
        AudioSource source = GetAudioSourceByType(sourceType);
        source.Stop();
    }

    private AudioSource GetAudioSourceByType(string sourceType)
    {
        switch (sourceType.ToLower())
        {
            case "music":
                return musicSource;
            case "ambient":
                return ambientSource;
            case "sfx":
            default:
                return sfxSource;
        }
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }

    private IEnumerator FadeIn(AudioSource source)
    {
        source.volume = 0;
        source.Play();

        while (source.volume < 1)
        {
            source.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    public void FadeOutSound(string sourceType)
    {
        AudioSource source = GetAudioSourceByType(sourceType);
        StartCoroutine(FadeOut(source));
    }

    public void FadeInSound(string sourceType)
    {
        AudioSource source = GetAudioSourceByType(sourceType);
        StartCoroutine(FadeIn(source));
    }

    public void AudioOff()
    {
        ambientSource.volume = 0;
        musicSource.volume = 0;
        sfxSource.volume = 0;
    }

    public void AudioOn()
    {
        musicSource.volume = startingMusicVolume;
        ambientSource.volume = startingAmbientVolume;
        sfxSource.volume = startingSFXVolume;
    }
}
