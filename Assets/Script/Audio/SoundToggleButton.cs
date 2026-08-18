using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundToggleButton : MonoBehaviour
{
    public Button button;
    public Image iconImage;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    private bool _isMuted = false;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (iconImage == null)
            iconImage = GetComponent<Image>();

        button.onClick.AddListener(ToggleSound);
        UpdateIcon();
    }

    public void ToggleSound()
    {
        _isMuted = !_isMuted;
        SoundManager.Instance.OffAudio(_isMuted);
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (iconImage != null && soundOnIcon != null && soundOffIcon != null)
            iconImage.sprite = _isMuted ? soundOffIcon : soundOnIcon;
    }
}
