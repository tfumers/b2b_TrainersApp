using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActivityComponent : InteractiveAppComponent<Activity>
{
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingName;
    [SerializeField] TMPro.TextMeshProUGUI txtActTypeValue;
    [SerializeField] TMPro.TextMeshProUGUI txtActType;
    [SerializeField] TrainingImageContainerComponent trainingImageContainer;
    [SerializeField] Image imgBg;

    protected override void Prepare(Activity activity)
    {
        txtTrainingName.text = activity.Training.Name;
        if (activity.ActTypeId==Constant.ACTIVITY_TYPE_REPS)
        {
            txtActType.text = "REPS";
            txtActTypeValue.text = activity.TypeValue.ToString();
        }
        else
        {
            txtActType.text = "s";
            txtActTypeValue.text = activity.TypeValue.ToString();
        }

        trainingImageContainer.LoadComponent(activity.Training.TrainingImage);

        if (activity.Completed)
        {
            SetCompletedColor();
        }

        //imgTraining.sprite = activity.TrainingId.Image();
        //Acá hay lugar para un método que establezca una imagen para el entrenamiento
    }

    public void SetCompletedColor()
    {
        imgBg.color = new Color32(30, 135, 61, 255);
    }
}
