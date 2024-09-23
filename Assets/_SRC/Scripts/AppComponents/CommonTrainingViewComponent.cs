using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonTrainingViewComponent : InteractiveAppComponent<Training>
{
    [SerializeField] TMPro.TextMeshProUGUI txtId;
    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtCategory;
    [SerializeField] TMPro.TextMeshProUGUI txtDifficulty;

    protected override void Prepare(Training model)
    {
        txtId.text = "Id:" + model.Id;
        txtName.text = model.Name;
        txtCategory.text = "Categorías: " + model.GetCategoriesAsString();
        txtDifficulty.text = "Dificultad: " + model.Difficulty;
        button.interactable = true;
    }
}
