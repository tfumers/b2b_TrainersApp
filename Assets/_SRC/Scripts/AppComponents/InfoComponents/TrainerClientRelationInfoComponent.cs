using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerClientRelationInfoComponent : InteractiveAppComponent<TrainerClientRelation>
{
    [SerializeField] TMPro.TextMeshProUGUI txtRoutineNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername;
    [SerializeField] TMPro.TextMeshProUGUI txtStartDate;
    [SerializeField] TMPro.TextMeshProUGUI txtNumberOfDays;

    [SerializeField] AvatarContainerComponent avatarContainer;

    protected override void Prepare(TrainerClientRelation model)
    {
        avatarContainer.LoadComponent(model.Client.AvatarImage);

        txtUsername.text = "@" + model.Client.Username;

        if (model.Routine.Id != 0)
        {
            txtRoutineNumber.text = "Rutina N°" + model.Routine.Id;
            txtStartDate.text = "Fecha inicio: " + model.Routine.StartDate.ToString();
            txtNumberOfDays.text = "Días de entrenamiento:" + model.Routine.NumberOfDays.ToString();
        }
        else
        {
            txtRoutineNumber.text = "Nueva Rutina";
            
            if (model.Routine.DailyActivities.Count > 0)
            {
                txtStartDate.text = "Fecha inicio: " + model.Routine.StartDate.ToString();
                txtNumberOfDays.text = "Días de entrenamiento: " + model.Routine.NumberOfDays.ToString();
            }
            else
            {
                txtStartDate.text = "Fecha inicio: --/--/----";
                txtNumberOfDays.text = "Días de entrenamiento: -";
            }

            
        }




        


    }
}
