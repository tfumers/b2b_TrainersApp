using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DailyComponent : InteractiveAppComponent<Daily>
{
    [SerializeField] TMPro.TextMeshProUGUI txtDay;
    [SerializeField] TMPro.TextMeshProUGUI txtDate;

    [SerializeField] UnityEngine.UI.RawImage imageBg;
    protected override void Prepare(Daily model)
    {
        txtDate.text = model.ProposedDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_DAY_OF_THE_WEEK, new CultureInfo("es-ES"));

        if (model.Id == 0)
        {
            button.interactable = false;
            txtDay.text = "Dia Off";
        }
        else
        {
            txtDay.text = "Dia " + model.DayNumber.ToString();
        }

        if (model.Completed)
        {
            imageBg.color = new Color32(30, 135, 61, 255);
        }
        
    }
}
