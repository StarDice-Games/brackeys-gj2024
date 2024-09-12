using UnityEngine;

public enum GameState
{
    Normal,
    Transforming,
    HydeMode
}

public class GameController : MonoBehaviour
{
    public GameState currentState { get; private set; }

    void Start()
    {
        currentState = GameState.Normal;
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != newState)
        {
            OnStateExit(currentState);
            currentState = newState;
            OnStateEnter(newState);
        }
    }

    private void OnStateEnter(GameState state)
    {
        switch (state)
        {
            case GameState.Normal:
                Debug.Log("Entrato nello stato: Normal");
                break;
            case GameState.Transforming:
                Debug.Log("Entrato nello stato: Transforming");
                break;
            case GameState.HydeMode:
                Debug.Log("Entrato nello stato: HydeMode");
                break;
        }
    }

    private void OnStateExit(GameState state)
    {
        switch (state)
        {
            case GameState.Normal:
                Debug.Log("Uscito dallo stato: Normal");
                break;
            case GameState.Transforming:
                Debug.Log("Uscito dallo stato: Transforming");
                break;
            case GameState.HydeMode:
                Debug.Log("Uscito dallo stato: HydeMode");
                break;
        }
    }
}
