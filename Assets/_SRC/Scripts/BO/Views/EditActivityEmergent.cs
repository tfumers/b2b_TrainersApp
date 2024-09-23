using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EditActivityEmergent : EmergentView
{
    [SerializeField] UnityEvent OnBackToEditDailyActivity = new UnityEvent();

    [SerializeField] UnityEvent OnSearchTrainingToAdd = new UnityEvent();

    [SerializeField] UnityEvent OnDeleteActivity = new UnityEvent();

    //[SerializeField] Training selector

    [SerializeField] ActivityEditInfoComponent activityEditInfoComponent;

    [SerializeField] DeleteActivityEditComponent deleteActivityEditComponent;

    [SerializeField] BackToEditDailyActivity backToEditDailyActivity;

    ComponentManager componentManager;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<ActivityEditInfoComponent> configureEditInfoComponent = componentManager.ConfigureComponent(activityEditInfoComponent, OnSearchTrainingToAdd);

        await configureEditInfoComponent;

        activityEditInfoComponent = configureEditInfoComponent.Result;


        Task<DeleteActivityEditComponent> configDeleteActEditComponent = componentManager.ConfigureComponent(deleteActivityEditComponent, OnDeleteActivity);

        await configDeleteActEditComponent;

        deleteActivityEditComponent = configDeleteActEditComponent.Result;


        Task<BackToEditDailyActivity> configBackButton = componentManager.ConfigureComponent(backToEditDailyActivity, OnBackToEditDailyActivity);

        await configBackButton;

        backToEditDailyActivity = configBackButton.Result;

        return true;
    }
}
