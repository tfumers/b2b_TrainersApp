using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TrainingSelectorComponent : InteractiveAppComponent<Training>
{
    public UnityEvent<Training> OnTrainingSelected = new UnityEvent<Training>();

    public Transform scrollViewTransform;

    [SerializeField] TMPro.TMP_InputField inputTextToSearch;

    [SerializeField] TrainingSearchParamComponent trainingSearchParamComponent;

    List<TrainingToSelectInActivityEditComponent> trainings = new List<TrainingToSelectInActivityEditComponent>();

    public List<TrainingToSelectInActivityEditComponent> Trainings { get => trainings;}

    public void ClearAndAddTrainings(List<TrainingToSelectInActivityEditComponent> trainingsToAdd = null)
    {
        ClearListOfTrainingsAndRemoveListeners();

        if (trainingsToAdd != null)
        {
            trainings = trainingsToAdd;
        }
        else
        {
            trainings = new List<TrainingToSelectInActivityEditComponent>();
        }
    }

    protected override void Prepare(Training model)
    {
        trainingSearchParamComponent.LoadComponent(GetParamToSearch());
    }

    public void SetButtonsInteractable(bool status)
    {
        button.interactable = status;
        trainingSearchParamComponent.SetButtonInteractable(status);
    }

    public string GetTextToSearch()
    {
        return inputTextToSearch.text;
    }

    public string GetParamToSearch()
    {
        if (model == null)
        {
            return "";
        }
        else
        {
            return trainingSearchParamComponent.Model;
        }
    }

    private void ClearListOfTrainingsAndRemoveListeners()
    {
        if (trainings == null)
        {
            trainings = new List<TrainingToSelectInActivityEditComponent>();
        }    

        if (trainings.Count>0)
        {
            foreach (TrainingToSelectInActivityEditComponent training in trainings)
            {
                training.RemoveAllListeners();
                Destroy(training.gameObject);
            }

            trainings.Clear();
        }
    }
    public override void RemoveAllListeners()
    {
        OnTrainingSelected.RemoveAllListeners();
        base.RemoveAllListeners();
    }


}
