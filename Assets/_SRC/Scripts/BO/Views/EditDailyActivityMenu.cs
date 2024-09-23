using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EditDailyActivityMenu : MenuView
{
    [SerializeField] UnityEvent OnAddActivity = new UnityEvent();

    [SerializeField] UnityEvent OnEditActivity = new UnityEvent();

    [SerializeField] UnityEvent OnDeleteDailyActivity = new UnityEvent();

    [SerializeField] UnityEvent OnBackToEditRoutineEdit = new UnityEvent();

    [SerializeField] DailyToEditInfoComponent dailyToEditInfoComponent;

    [SerializeField] AddActivityComponent addActivityComponent;

    [SerializeField] DeleteDailyActivityEditComponent deleteDailyActivityComponent;

    [SerializeField] BackToEditRoutineComponent backToEditRoutineComponent;

    [SerializeField] Transform scrollViewTransform;

    List<EditActivityComponent> editActivityComponents;

    ComponentManager componentManager;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<DailyToEditInfoComponent> configDailyToEdit = componentManager.ConfigureComponent(dailyToEditInfoComponent);

        dailyToEditInfoComponent = configDailyToEdit.Result;

        Task<List<EditActivityComponent>> loadEditActivityComponents = componentManager.CreateListOfComponents(editActivityComponents, scrollViewTransform, OnEditActivity);

        await loadEditActivityComponents;

        editActivityComponents = loadEditActivityComponents.Result;

        Task<AddActivityComponent> configureAAC = componentManager.ConfigureComponent(addActivityComponent, scrollViewTransform, OnAddActivity);

        await configureAAC;

        addActivityComponent = configureAAC.Result;

        Task<DeleteDailyActivityEditComponent> configureDeleteDailyActivityEditComponent = componentManager.ConfigureComponent(deleteDailyActivityComponent, OnDeleteDailyActivity);

        await configureDeleteDailyActivityEditComponent;

        deleteDailyActivityComponent = configureDeleteDailyActivityEditComponent.Result;

        Task<BackToEditRoutineComponent> configureBackButton = componentManager.ConfigureComponent(backToEditRoutineComponent, OnBackToEditRoutineEdit);

        await configureBackButton;

        backToEditRoutineComponent = configureBackButton.Result;

        return true;
    }
}
