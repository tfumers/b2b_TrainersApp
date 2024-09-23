using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EditRoutineView : MenuView
{
    [SerializeField] UnityEvent OnClientInfoButtonClicked = new UnityEvent();

    [SerializeField] UnityEvent OnAddDailyButtonClicked = new UnityEvent();

    [SerializeField] UnityEvent OnEditDailyButtonClicked = new UnityEvent();

    [SerializeField] UnityEvent OnEndRoutineEditButtonClicked = new UnityEvent();

    [SerializeField] UnityEvent OnDeleteRoutineEditButtonClicked = new UnityEvent();

    [SerializeField] TrainerClientRelationInfoComponent trainerClientRelationInfoComponent;

    [SerializeField] AddDailyComponent addDailyComponent;

    [SerializeField] DeleteRoutineEditComponent deleteRoutineEditComponent;

    [SerializeField] BackToSelectPendingClient backToSelectPendingClient;

    [SerializeField] EndRoutineEditComponent endRoutineEditComponent;

    [SerializeField] Transform scrollViewTransform;

    List<EditDailyComponent> editDailyComponents = new List<EditDailyComponent>();

    ComponentManager componentManager;

    //RoutineToEditInfoComponent 
    //ClientInfoButton

    //AddDailyComponent

    //List<EditDailyComponent>

    //DeleteRoutineComponent
    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<TrainerClientRelationInfoComponent> configureTCRIC = componentManager.ConfigureComponent(trainerClientRelationInfoComponent, OnClientInfoButtonClicked);

        await configureTCRIC;

        trainerClientRelationInfoComponent = configureTCRIC.Result;

        Task<List<EditDailyComponent>> getEditDailyComponent = componentManager.CreateListOfComponents(editDailyComponents, scrollViewTransform, OnEditDailyButtonClicked);

        await getEditDailyComponent;

        editDailyComponents = getEditDailyComponent.Result;

        Task<AddDailyComponent> configureAddDailyComponent = componentManager.ConfigureComponent(addDailyComponent, scrollViewTransform, OnAddDailyButtonClicked);

        await configureAddDailyComponent;

        addDailyComponent = configureAddDailyComponent.Result;

        Task<DeleteRoutineEditComponent> configureDeleteRoutine = componentManager.ConfigureComponent(deleteRoutineEditComponent, OnDeleteRoutineEditButtonClicked);

        await configureDeleteRoutine;

        deleteRoutineEditComponent = configureDeleteRoutine.Result;

        Task<BackToSelectPendingClient> configureBackButton = componentManager.ConfigureComponent(backToSelectPendingClient, OnDeleteRoutineEditButtonClicked);

        await configureBackButton;

        backToSelectPendingClient = configureBackButton.Result;

        Task<EndRoutineEditComponent> configureEndRoutineEdit = componentManager.ConfigureComponent(endRoutineEditComponent, OnEndRoutineEditButtonClicked);

        await configureEndRoutineEdit;

        endRoutineEditComponent = configureEndRoutineEdit.Result;

        return true;
    }
}
