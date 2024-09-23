using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainingSearchParamComponent : AppComponent<string>
{
    [SerializeField] Toggle searchByName;
    [SerializeField] Toggle searchByCategory;
    [SerializeField] Toggle searchByDifficulty;
    [SerializeField] Toggle searchByDescription;
    public void SetButtonInteractable(bool status)
    {
        searchByName.interactable = status;
        searchByCategory.interactable = status;
        searchByDifficulty.interactable = status;
        searchByDescription.interactable = status;
    }

    protected override void Prepare(string model)
    {
        searchByName.onValueChanged.RemoveAllListeners();
        searchByCategory.onValueChanged.RemoveAllListeners();
        searchByDifficulty.onValueChanged.RemoveAllListeners();
        searchByDescription.onValueChanged.RemoveAllListeners();

        SetSelected(model);

        searchByName.onValueChanged.AddListener(delegate { SetSelected("Name"); });
        searchByCategory.onValueChanged.AddListener(delegate { SetSelected("Category"); });
        searchByDifficulty.onValueChanged.AddListener(delegate { SetSelected("Category"); });
        searchByDescription.onValueChanged.AddListener(delegate { SetSelected("Category"); });
    }

    private void SetSelected(string value)
    {
        Debug.Log("MODEL: " + value);
        switch (value)
        {
            case "Name":
            case "Category":
            case "Difficulty":
            case "Description":
                model = value;
                break;
            default:
                model = "Name";
                break;
        }
    }
}
