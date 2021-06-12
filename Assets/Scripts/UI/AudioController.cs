using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _audioOff;
    [SerializeField] private Sprite _audioOn;
    [SerializeField] private Image _image;

    private bool _isOn = true;

    private void Awake()
    {
        _button.onClick.AddListener(ClickAudio);
    }

    private void ClickAudio()
    {
        _isOn = !_isOn;

        if (_isOn)
        {
            AudioManager.Instance.Unmute();
            _image.sprite = _audioOn;
        }
        else
        {
            AudioManager.Instance.Mute();
            _image.sprite = _audioOff;
        }
    }
}
