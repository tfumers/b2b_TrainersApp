using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectedTrainingComponent : AppComponent<Training>
{
    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtDescription;
    [SerializeField] TMPro.TextMeshProUGUI txtCategories;
    [SerializeField] TMPro.TextMeshProUGUI txtDifficulty;
    [SerializeField] TMPro.TextMeshProUGUI txtEstCaloriesPerRep;
    [SerializeField] TMPro.TextMeshProUGUI txtEstTimePerRep;
    [SerializeField] TrainingImageContainerComponent trainingImageContainer;

    protected override void Prepare(Training model)
    {
        if (model.Id != 0)
        {
            txtName.text = model.Name;
            txtDescription.text = model.Description;
            txtCategories.text = "Categorías: " + model.GetCategoriesAsString();
            txtDifficulty.text = "Dificultad: " + model.Difficulty;
            txtEstCaloriesPerRep.text = "Calorias por rep: " + model.EstCaloriesPerRep.ToString() + " cal";
            txtEstTimePerRep.text = "Tiempo por rep:" + model.EstTimePerRep.ToString() + "";
            trainingImageContainer.LoadComponent(model.TrainingImage);
        }
        else
        {
            txtName.text = "Nombre del entrenamiento";
            txtDescription.text = "Descripción del entrenamiento";
            txtCategories.text = "Categorías: xx, yy, zz";
            txtDifficulty.text = "Dificultad: xxxx";
            txtEstCaloriesPerRep.text = "Calorias por rep: XXXX cal";
            txtEstTimePerRep.text = "Tiempo por rep: xxx s";
            trainingImageContainer.LoadComponent(model.TrainingImage);
        }
    }

}
