using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeManager : MonoBehaviour
{
    [SerializeField]
    Volume GlobalVolumeSecondPhase;

    Coroutine currentActiveFade = null;

    public Coroutine FadeIn(float time)
    {
        return Fade(1, time);
    }

    public Coroutine FadeOut(float time)
    {
        return Fade(0, time);
    }

    public Coroutine Fade(float target, float time)
    {
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }
        currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float target, float time)
    {
        while (!Mathf.Approximately(GlobalVolumeSecondPhase.weight, target))
        {
            GlobalVolumeSecondPhase.weight = Mathf.MoveTowards(GlobalVolumeSecondPhase.weight, target, Time.unscaledDeltaTime / time);
            yield return null;
        }
    }
}
