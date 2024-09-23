using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTrainingInfoComponent : AppComponent<Training>
{
    [SerializeField] TMPro.TextMeshProUGUI txtTrainingName;
    [SerializeField] TrainingImageContainerComponent trainingImageContainer;
    [SerializeField] TMPro.TextMeshProUGUI txtDescription;
    [SerializeField] TMPro.TextMeshProUGUI txtCategories;
    [SerializeField] TMPro.TextMeshProUGUI txtDifficulty;
    [SerializeField] TMPro.TextMeshProUGUI txtEstTimePerRep;
    [SerializeField] TMPro.TextMeshProUGUI txtEstCalPerRep;
    [SerializeField] TrainingVideoContainerComponent trainingVideoContainer;
    //training Video container

    protected override void Prepare(Training model)
    {
        txtTrainingName.text = model.Name;

        trainingImageContainer.LoadComponent(model.TrainingImage);

        txtDescription.text = "Descripción: " + model.Description;

        txtCategories.text = "Categorías: " + model.Difficulty;

        txtDifficulty.text = "Dificultad: " + model.Difficulty;

        txtEstTimePerRep.text = "Tiempo estimado por repetición: " + model.EstTimePerRep.ToString() + "s";

        txtEstCalPerRep.text = "Calorias estimadas por repetición: " + model.EstCaloriesPerRep.ToString() + "cal";

        trainingVideoContainer.LoadComponent(model.TrainingVideo);

    }
}
