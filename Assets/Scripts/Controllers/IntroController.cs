using UnityEngine;

public class IntroController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneController.instance.StartGame();
        }
    }
}
