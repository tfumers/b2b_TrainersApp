using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentActivityInfoComponent : InteractiveAppComponent<Activity>
{
    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtActType;
    [SerializeField] TMPro.TextMeshProUGUI txtActTypeValue;
    [SerializeField] TMPro.TextMeshProUGUI txtDescription;
    [SerializeField] TMPro.TextMeshProUGUI txtCompletion;
    [SerializeField] TMPro.TextMeshProUGUI txtEstTimeOrElapsedTime;

    protected override void Prepare(Activity model)
    {
        txtName.text = model.Training.Name;

        if (model.ActTypeId == Constant.ACTIVITY_TYPE_REPS)
        {
            txtActType.text = "Repeticiones: ";
            txtActTypeValue.text = model.TypeValue.ToString();
        }
        else
        {
            txtActType.text = "Tiempo: ";
            txtActTypeValue.text = model.TypeValue.ToString() + "s";
        }

        txtDescription.text = "Descripción: " + model.Training.Description;

        if (model.Completed)
        {
            txtCompletion.text = "¿Completada? Si";
            txtEstTimeOrElapsedTime.text = "Tiempo empleado: " + model.ElapsedTime +"s";
        }
        else
        {
            txtCompletion.text = "¿Completada? No";

            if (model.ActTypeId == Constant.ACTIVITY_TYPE_REPS)
            {
                txtEstTimeOrElapsedTime.text = "Tiempo estimado: " + (model.Training.EstTimePerRep * model.TypeValue) + "s";
            }
            else
            {
                txtActType.text = "Tiempo: ";
                txtEstTimeOrElapsedTime.text = "Tiempo estimado: " + model.TypeValue.ToString() + "s";
            }
        }
    }
}
