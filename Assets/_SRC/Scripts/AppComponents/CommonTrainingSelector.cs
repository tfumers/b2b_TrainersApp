using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonTrainingSelector : AppComponent<UnityEvent<string, string>>
{
    [SerializeField] Button searchButton;

    [SerializeField] TMPro.TMP_InputField inputField;

    [SerializeField] TrainingSearchParamComponent searchParamComponent;

    protected override void Prepare(UnityEvent<string, string> model)
    {
        searchButton.onClick.RemoveAllListeners();

        searchParamComponent.LoadComponent("");

        searchButton.onClick.AddListener(() => InvokeEvent(inputField.text, searchParamComponent.Model));
    }

    private void InvokeEvent(string searchText, string  searchParam)
    {
        model.Invoke(searchText, searchParam);
    }
}
