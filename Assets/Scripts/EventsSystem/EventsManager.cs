using System;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    [SerializeField] Fader fader;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

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
            fader.FadeOut(fadeOutTime);
        }
    }
}
