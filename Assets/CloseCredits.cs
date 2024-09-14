using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseCredits : MonoBehaviour
{
    [SerializeField] Button closeCreditsBtn;

    private void Start()
    {
        closeCreditsBtn.onClick.AddListener(BackToTitleScreen);
    }

    private void BackToTitleScreen()
    {
        string sceneName = SceneManager.GetSceneByName("TitleScreen").name;
        SceneManager.LoadScene(sceneName);
    }
}
