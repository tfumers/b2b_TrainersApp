using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyToEditInfoComponent : AppComponent<Daily>
{
    [SerializeField] TMPro.TMP_InputField inputFieldProposedDate;
    [SerializeField] TMPro.TextMeshProUGUI txtEstablishedDate;

    protected override void Prepare(Daily model)
    {
        inputFieldProposedDate.onEndEdit.RemoveAllListeners();

        if (model.ProposedDate != null)
        {
            inputFieldProposedDate.text = model.ProposedDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_SIMPLIFIED);
            txtEstablishedDate.text = model.ProposedDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_DAY_N_MONTH);
        }

        if (model.Completed)
        {
            inputFieldProposedDate.interactable = false;
        }
        else
        {
            inputFieldProposedDate.interactable = true;
            inputFieldProposedDate.onEndEdit.AddListener((dateInString) => { 
                model.SetProposedDate(dateInString);
                txtEstablishedDate.text = model.ProposedDate.ToString(Constant.DEFAULT_APP_DATE_FORMAT_DAY_N_MONTH);
            });
        }
    }
}
