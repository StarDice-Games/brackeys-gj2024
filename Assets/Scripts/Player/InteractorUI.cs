using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorUI : MonoBehaviour
{
    [SerializeField] InteractionDetector interactor;
    [SerializeField] GameObject interactionPopup;

    private void OnEnable()
    {
        interactor.OnHoverInteraction += ShowPopup;
        interactor.OnExitInteraction += HidePopup;
    }

    private void OnDisable()
    {
        interactor.OnHoverInteraction -= ShowPopup;
        interactor.OnExitInteraction -= HidePopup;
    }

    private void ShowPopup()
    {
        interactionPopup.SetActive(true);
    }

    private void HidePopup()
    {
        interactionPopup.SetActive(false);
    }
}
