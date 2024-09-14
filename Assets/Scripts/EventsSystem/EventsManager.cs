using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    [Header("Fader")]
    [SerializeField] Fader fader;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    [Header("Volume")]
    [SerializeField] GlobalVolumeManager globalVolumeManager;
    [SerializeField] float changeVolumeTime;

    [Header("Player")]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject monsterController;

    [Header("Guests")]
    [SerializeField] List<GameObject> guests;
    [SerializeField] List<Transform> guestSpawnPoints;

    [SerializeField] float timeBetweenEvents = 5f;

    public UnityEvent OnStartGame, OnMainTaskCompleted, OnDoorOpen, OnGuestsEnter, OnSecondPhase, OnEndGame;

    public static EventsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OnStartGame?.Invoke();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(StartingSecondPhase());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(StartingEndGame());

        }

        if (!monsterController.activeInHierarchy)
        {
            monsterController.transform.position = playerController.transform.position;
        }
    }

    IEnumerator StartingSecondPhase()
    {
        OnDoorOpen?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnGuestsEnter?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnSecondPhase?.Invoke();
    }

    IEnumerator StartingEndGame()
    {
        ScreenFadeOut(3f);
        yield return new WaitForSeconds(timeBetweenEvents);
        SwapPlayerToMonster(false);
        yield return new WaitForSeconds(timeBetweenEvents);
        ScreenFadeIn(2f);
    }

    public void EnableAgents()
    {
        foreach (var guest in guests)
        {
            guest.GetComponent<Agent>().enabled = true;
        }
    }

    public void DisableAgents()
    {
        foreach (var guest in guests)
        {
            guest.GetComponent<Agent>().enabled = false;
        }
    }

    public void SpawnGuests()
    {
        if (guests.Count > 0 && guestSpawnPoints.Count > 0)
        {
            for (int i = 0; i < guests.Count; i++)
            {
                guests[i].transform.position = guestSpawnPoints[i].position;
            }
        }
    }

    public void ApplyVolumeSecondPhase()
    {
        globalVolumeManager.FadeIn(changeVolumeTime);
    }

    public void ScreenFadeIn(float time)
    {
        fader.FadeIn(time);
    }

    public void ScreenFadeOut(float time)
    {
        fader.FadeOut(time);
    }

    public void EnablePlayerController()
    {
        playerController.enabled = true;
    }

    public void DisablePlayerController()
    {
        playerController.enabled = false;
    }

    public void SwapPlayerToMonster(bool isMonster)
    {
        playerController.gameObject.SetActive(!isMonster);
        monsterController.SetActive(isMonster);
    }
}