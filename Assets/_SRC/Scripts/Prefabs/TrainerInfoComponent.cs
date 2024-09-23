using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainerInfoComponent : InteractiveAppComponent<Trainer>
{
    public RawImage background;
    public RawImage avatar;

    protected override void Prepare(Trainer model)
    {
        
    }
}
