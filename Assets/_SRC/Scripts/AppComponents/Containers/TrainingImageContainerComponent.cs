using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingImageContainerComponent : AppComponent<TrainingImage>
{
    [SerializeField] Image imgTraining;

    protected override void Prepare(TrainingImage model)
    {
        if (model != null)
        {
            imgTraining.sprite = model.Image;
        }
        else
        {
            imgTraining.sprite = null;
        }
    }
}
