using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityButton : MonoBehaviour
{
    public Button Button;
    [SerializeField] Image image;

    [SerializeField]Sprite playIcon;
    [SerializeField]Sprite pauseIcon;
    [SerializeField]Sprite stopIcon;


    public void SetPlayButton()
    {
        image.sprite = playIcon;
    }

    public void SetPauseButton()
    {
        image.sprite = pauseIcon;
    }

    public void SetStopButton()
    {
        image.sprite = stopIcon;
    }
}
