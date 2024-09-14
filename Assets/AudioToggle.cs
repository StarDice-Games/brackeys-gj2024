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
    }

    public void SetAudio()
    {
        audioIcon.sprite = toggle.isOn ? audioOnIcon : audioOffIcon;

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
