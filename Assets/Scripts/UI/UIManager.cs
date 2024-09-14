using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button startGameSceneBtn, quitGameBtn, openCreditsScene;

    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        startGameSceneBtn.onClick.AddListener(StartGameScene);
        quitGameBtn.onClick.AddListener(ExitGame);
        openCreditsScene.onClick.AddListener(OpenCreditsScene);
    }

    public void StartGameScene()
    {
        string sceneName = SceneManager.GetSceneByName("MainScene").name;
        SceneManager.LoadScene(sceneName);
    }

    public void OpenCreditsScene()
    {
        string sceneName = SceneManager.GetSceneByName("Credits").name;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
