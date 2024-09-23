using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickableAvatarComponent : InteractiveAppComponent<Avatar>
{
    [SerializeField] AvatarContainerComponent avatarContainer;

    public RawImage bgColor;

    bool selected = false;

    Color selectedColor = Color.green;
    Color deselectedColor = Color.white;

    public bool Selected { get => selected;}

    protected override void Prepare(Avatar model)
    {
        avatarContainer.LoadComponent(model);

    }

    public bool CheckSelectedAvatar(Avatar avatar)
    {
        if (model == avatar)
        {
            Select();
            selected = true;
            return true;
        }
        else
        {
            Deselect();
            selected = false;
            return false;
        }
    }

    private void ChangeBgColor(Color color)
    {
        bgColor.color = color;
    }

    private void Select()
    {
        ChangeBgColor(selectedColor);
    }

    private void Deselect()
    {
        ChangeBgColor(deselectedColor);
    }
}
