using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionComponent : AppComponent<string>
{
    [SerializeField] TMPro.TextMeshProUGUI txtTitle;
    [SerializeField] TMPro.TextMeshProUGUI txtDescription;

    protected override void Prepare(string model)
    {
        //Debug.Log("Texto cargado: " + model + " en título: " + txtTitle.text);
        txtDescription.text = model;
    }
}
