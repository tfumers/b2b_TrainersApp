using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class TrainingMenuView : MenuView
{
    [SerializeField] UnityEvent OnTrainingButtonPressed = new UnityEvent();

    [SerializeField] CommonTrainingSelector trainingSelector;

    [SerializeField] Transform scrollViewContentTransform;

    ComponentManager componentManager;

    List<CommonTrainingViewComponent> commonTrainingViewComponents;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public override async Task<bool> LoadView()
    {
        Task<CommonTrainingSelector> configureTrainingSelector = componentManager.ConfigureComponent(trainingSelector);

        await configureTrainingSelector;

        trainingSelector = configureTrainingSelector.Result;

        if (commonTrainingViewComponents == null)
        {
            commonTrainingViewComponents = new List<CommonTrainingViewComponent>();
        }

        if (commonTrainingViewComponents.Count > 0)
        {
            foreach (CommonTrainingViewComponent ctvc in commonTrainingViewComponents)
            {
                ctvc.RemoveAllListeners();
                Destroy(ctvc.gameObject);
            }

            commonTrainingViewComponents.Clear();
        }

        trainingSelector.Model.AddListener(async (toSearch, paramText) =>
        {
            Task<List<CommonTrainingViewComponent>> configureTrainingsToSelect = componentManager.CreateListOfComponents(commonTrainingViewComponents, scrollViewContentTransform, toSearch, paramText, OnTrainingButtonPressed);

            await configureTrainingsToSelect;

            commonTrainingViewComponents = configureTrainingsToSelect.Result;
        });

        return true;
    }
}
