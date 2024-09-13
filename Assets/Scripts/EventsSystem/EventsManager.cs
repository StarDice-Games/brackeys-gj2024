using System;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    [SerializeField] Fader fader;
    [SerializeField] GlobalVolumeManager globalVolumeManager;
    [Header("Fade")]
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    [Header("Volume")]
    [SerializeField] float changeVolumeTime;

    public UnityEvent OnStartGame, OnMainTaskCompleted, OnDoorOpen, OnSecondPhase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnStartGame?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            OnSecondPhase?.Invoke();
        }
    }

    public void StartGlobalVolumePhase2()
    {
        globalVolumeManager.FadeIn(changeVolumeTime);
    }
}
