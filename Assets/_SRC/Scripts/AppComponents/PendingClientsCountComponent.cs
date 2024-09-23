using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendingClientsCountComponent : AppComponent<int>
{
    [SerializeField] TMPro.TextMeshProUGUI txtCount;
    [SerializeField] Image bgImage;
    [SerializeField] CanvasGroup canvasGroup;

    protected override void Prepare(int model)
    {
        txtCount.text = model.ToString();

        if(model > 0)
        {
            this.gameObject.SetActive(true);
            this.canvasGroup.alpha = 1;
        }
        else
        {
            this.gameObject.SetActive(false);
            this.canvasGroup.alpha = 0;
        }
    }
}
