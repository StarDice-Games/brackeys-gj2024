using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0) &&
            (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0) &&
            SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(3)))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void OpenIntro()
    {
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
