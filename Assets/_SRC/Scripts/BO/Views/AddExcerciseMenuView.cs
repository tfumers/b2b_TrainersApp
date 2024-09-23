using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class AddExcerciseMenuView : MenuView
{
    [SerializeField] UnityEvent OnCreateTrainingButtonPressed = new UnityEvent();
    [SerializeField] UnityEvent OnCancelTrainingCreationButtonPressed = new UnityEvent();

    [SerializeField] AddTrainingInfoComponent addExcerciseInfoComponent;

    ComponentManager componentManager;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<AddTrainingInfoComponent> configureExcerciseInfo = componentManager.ConfigureComponent(addExcerciseInfoComponent, OnCancelTrainingCreationButtonPressed, OnCreateTrainingButtonPressed);

        await configureExcerciseInfo;

        addExcerciseInfoComponent = configureExcerciseInfo.Result;

        return true;
    }
}
