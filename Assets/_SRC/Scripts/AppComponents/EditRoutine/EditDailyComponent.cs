using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EditDailyComponent : InteractiveAppComponent<Daily>
{
    [SerializeField] TMPro.TextMeshProUGUI txtOrderNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtProposedDate;
    [SerializeField] TMPro.TextMeshProUGUI txtNumberOfActivities;
    [SerializeField] TMPro.TextMeshProUGUI txtEstimatedTime;

    protected override void Prepare(Daily model)
    {
        txtOrderNumber.text = model.OrderNumber.ToString();

        if (model.ProposedDate != null)
        {
            txtProposedDate.text = model.ProposedDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_COMPLETED, new CultureInfo("es-ES"));
        }
        else
        {
            txtProposedDate.text = "Seleccione una fecha para verla aquí";
        }

        txtNumberOfActivities.text = "Actividades: " + model.Activities.Count.ToString();

        int estTime = 0;
        foreach (Activity act in model.Activities)
        {
            estTime += act.ElapsedTime;
        }


        txtEstimatedTime.text = "Tiempo estimado: " + estTime;
    }
}
