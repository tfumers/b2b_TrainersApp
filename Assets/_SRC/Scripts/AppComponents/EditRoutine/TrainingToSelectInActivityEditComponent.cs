using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingToSelectInActivityEditComponent : InteractiveAppComponent<Training>
{
    [SerializeField] TMPro.TextMeshProUGUI txtId;
    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtCategory;
    [SerializeField] TMPro.TextMeshProUGUI txtDifficulty;
    [SerializeField] Image image;

    public void CheckSelected(Training received)
    {
        if (received == model)
        {
            SetSelected();
        }
        else
        {
            SetDeselected();
        }
    }

    protected override void Prepare(Training model)
    {
        if (model.Id != 0)
        {
            txtId.text = "Id:" + model.Id;
            txtName.text = model.Name;
            txtCategory.text = "Categorías: " + model.GetCategoriesAsString();
            txtDifficulty.text = "Dificultad: " + model.Difficulty;
            button.interactable = true;
        }
        else
        {
            txtId.text = "";
            txtName.text = "";
            txtCategory.text = "";
            txtDifficulty.text = "";
            button.interactable = false;
        }
    }

    private void SetSelected()
    {
        image.color = Color.green;
    }

    private void SetDeselected()
    {
        image.color = Color.white;
    }
}
