using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] Image audioIcon;
    [SerializeField] Sprite audioOnIcon, audioOffIcon;
    Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        audioIcon.sprite = PlayerPrefs.GetInt("audioToggle", 1) == 1 ? audioOnIcon : audioOffIcon;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SetAudio()
    {
        audioIcon.sprite = toggle.isOn ? audioOnIcon : audioOffIcon;

        PlayerPrefs.SetInt("audioToggle", toggle.isOn ? 1 : 0);

        if (toggle.isOn)
        {
            AudioController.Instance.AudioOn();
        }
        else
        {
            AudioController.Instance.AudioOff();
        }
    }
}
