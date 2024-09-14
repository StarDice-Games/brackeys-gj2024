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
    [SerializeField] PlayerController monsterController;

    [Header("Guests")]
    [SerializeField] List<GameObject> guests;
    [SerializeField] List<Transform> guestSpawnPoints;

    [SerializeField] float timeBetweenEvents = 5f;

    [Header("Audio")]
    [SerializeField] AudioClip cozyMainTrack;
    [SerializeField] AudioClip monsterMainTrack;
    [SerializeField] AudioClip stormTrack;
    [SerializeField] AudioClip closedDoorSFX;

    [Header("MainDoor")]
    [SerializeField] Item mainDoor;

    [Header("Tasks")]
    [SerializeField] GameObject taskUI;
    [SerializeField] GameObject welcomeGuestsTask, haveDinnerTask;
    [SerializeField] GameObject initialTaskContainer;

    public UnityEvent OnStartGame, OnInitialTasksCompleted, OnDoorOpen, OnGuestsEnter, OnDoorClosed, OnSecondPhase, OnStartEndGame, OnMiddleEndGame, OnEndGame;
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

        if (!monsterController.gameObject.activeInHierarchy)
        {
            monsterController.transform.position = playerController.transform.position;
            monsterController.transform.localScale = playerController.transform.localScale;
        }
        else
        {
            playerController.transform.position = monsterController.transform.position;
            playerController.transform.localScale = monsterController.transform.localScale;
        }
    }

    public IEnumerator StartingSecondPhase()
    {
        OnDoorOpen?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnGuestsEnter?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnDoorClosed?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnSecondPhase?.Invoke();
    }

    public IEnumerator StartingEndGame()
    {
        OnStartEndGame?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnMiddleEndGame?.Invoke();
        yield return new WaitForSeconds(timeBetweenEvents);
        OnEndGame?.Invoke();
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

    public void ApplyVolumeFirstPhase()
    {
        globalVolumeManager.FadeOut(changeVolumeTime);
    }

    public void ScreenFadeIn(float time)
    {
        fader.FadeIn(time);
    }

    public void ScreenFadeOut(float time)
    {
        fader.FadeOut(time);
    }

    public void TogglePlayerController(bool isActive)
    {
        playerController.enabled = isActive;
    }

    public void ToggleMonsterController(bool isActive)
    {
        monsterController.enabled = isActive;
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
        monsterController.gameObject.SetActive(isMonster);
    }

    public void PlayCozyMainTrack()
    {
        AudioController.Instance.PlaySound(cozyMainTrack.name, false, "music", 0.05f);
    }

    public void PlayMonsterMainTrack()
    {
        AudioController.Instance.PlaySound(monsterMainTrack.name, false, "music", 0.2f);
    }

    public void PlayStormTrack()
    {
        AudioController.Instance.PlaySound(stormTrack.name, false, "ambient", 0.2f);
    }

    public void StopStormTrack()
    {
        AudioController.Instance.StopSound("ambient");
    }

    public void PlayOnDoorClosed()
    {
        AudioController.Instance.PlaySound(closedDoorSFX.name, true, "sfx");
    }

    public void ToggleMainDoor(bool isOpen)
    {
        mainDoor.gameObject.SetActive(isOpen);
    }

    public void ToggleInitialTaskContainerTask(bool isActive)
    {
        initialTaskContainer.SetActive(isActive);
    }

    public void ToggleWelcomeGuestsTask(bool isActive)
    {
        welcomeGuestsTask.SetActive(isActive);
    }

    public void ToggleHaveDinnerTask(bool isActive)
    {
        haveDinnerTask.SetActive(isActive);
    }

    public void ToggleTaskUI(bool isActive)
    {
        taskUI.SetActive(isActive);
    }
}