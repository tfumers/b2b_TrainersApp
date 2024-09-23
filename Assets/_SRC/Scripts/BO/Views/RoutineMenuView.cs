using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RoutineMenuView : MenuView
{
    public UnityEvent OnMoreInfoButtonClicked = new UnityEvent();

    public UnityEvent OnEditRoutineButtonClicked = new UnityEvent();

    public UnityEvent OnDailyActivityClicked = new UnityEvent();

    ComponentManager componentManager;

    //Componentes
    [SerializeField] TrainerClientRelationInfoComponent trainerClientRelationInfoComponent;

    List<DailyComponent> dailyComponents;

    [SerializeField] Transform dailyComponentScrollView;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<TrainerClientRelationInfoComponent> configureTCRIC_Task = componentManager.ConfigureComponent(trainerClientRelationInfoComponent, OnMoreInfoButtonClicked);

        await configureTCRIC_Task;

        trainerClientRelationInfoComponent = configureTCRIC_Task.Result;

        Task<List<DailyComponent>> configureAndLoadDC_Task = componentManager.CreateListOfComponents(dailyComponents, dailyComponentScrollView, OnDailyActivityClicked);

        dailyComponents = configureAndLoadDC_Task.Result;

        return true;
    }
}
