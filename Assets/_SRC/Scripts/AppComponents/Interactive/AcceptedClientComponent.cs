using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedClientComponent : InteractiveAppComponent<TrainerClientRelation>
{

    [SerializeField] TMPro.TextMeshProUGUI txtClientName;
    [SerializeField] TMPro.TextMeshProUGUI txtRoutineStartDate;
    [SerializeField] TMPro.TextMeshProUGUI txtTotalDaysCompleted;
    [SerializeField] AvatarContainerComponent avatarContainer;
    [SerializeField] Image buttonImg;
    protected override void Prepare(TrainerClientRelation model)
    {
        txtClientName.text = model.Client.Firstname + " " + model.Client.Lastname;
        txtRoutineStartDate.text = "Inicio de entrenamiento: " + model.Routine.StartDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_SIMPLIFIED);
        txtTotalDaysCompleted.text =  model.Routine.GetCompletedTrainingDays().ToString() + "/" + model.Routine.NumberOfDays;

        if (model.Routine.GetCompletedRoutine())
        {
            SetRoutineCompleted();
        }

        avatarContainer.LoadComponent(model.Client.AvatarImage);
    }

    public void SetRoutineCompleted()
    {
        buttonImg.color = Color.green;
    }
}
