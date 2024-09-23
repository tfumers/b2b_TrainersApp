using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrainerAvatarComponent : InteractiveAppComponent<Avatar>
{
    [SerializeField] AvatarContainerComponent avatarContainerComponent;

    protected override void Prepare(Avatar model)
    {
        avatarContainerComponent.LoadComponent(model);
    }
}
