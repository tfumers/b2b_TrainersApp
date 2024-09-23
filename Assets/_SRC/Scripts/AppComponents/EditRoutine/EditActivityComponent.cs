using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditActivityComponent : InteractiveAppComponent<Activity>
{
    [SerializeField] TMPro.TextMeshProUGUI txtOrderNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingName;
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingTypeAndValue;


    protected override void Prepare(Activity model)
    {
        string trainingTypeText;

        txtOrderNumber.text = model.OrderNumber.ToString();

        if (model.Training.Id != 0)
        {
            txtTrainingName.text = model.Training.Name;
        }
        else
        {
            txtTrainingName.text = "Nombre de entrenamiento";
        }

        switch (model.ActTypeId)
        {
            case Constant.ACTIVITY_TYPE_REPS:
                trainingTypeText = model.TypeValue + " reps";
                break;
            case Constant.ACTIVITY_TYPE_TIMER:
                trainingTypeText = model.TypeValue + " s";
                break;
            default:
                trainingTypeText = "x reps";
                break;
        }

        txtTrainingTypeAndValue.text = trainingTypeText;
    }
}
