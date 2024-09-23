using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityToEditStatusComponent : AppComponent<bool>
{
    [SerializeField] Image StatusColor;

    protected override void Prepare(bool model)
    {
        CheckStatus(model);
    }

    public void CheckStatus(bool status)
    {
        if (status)
        {
            SetReady();
        }
        else
        {
            SetNotReady();
        }
    }

    private void SetReady()
    {
        StatusColor.color = Color.green;
    }

    private void SetNotReady()
    {
        StatusColor.color = Color.red;
    }
}
