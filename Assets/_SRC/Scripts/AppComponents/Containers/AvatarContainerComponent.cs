using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarContainerComponent : AppComponent<Avatar>
{
    [SerializeField] RawImage avatarImage;

    protected override void Prepare(Avatar model)
    {
        avatarImage.texture = model.Image;
    }
}
