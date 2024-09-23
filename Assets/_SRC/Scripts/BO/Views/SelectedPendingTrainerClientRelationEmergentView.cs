using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SelectedPendingTrainerClientRelationEmergentView : EmergentView
{
    [SerializeField] UnityEvent OnCreateRoutineButtonPressed = new UnityEvent();

    [SerializeField] CompleteClientInfoComponent pendingTrainerClientRelationInfo;

    [SerializeField] CreateRoutineButtonComponent createRoutineButtonComponent;

    ComponentManager componentManager;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<CompleteClientInfoComponent> configCompClientInfoComponent = componentManager.ConfigureComponent(pendingTrainerClientRelationInfo);

        await configCompClientInfoComponent;

        pendingTrainerClientRelationInfo = configCompClientInfoComponent.Result;

        Task<CreateRoutineButtonComponent> configureCRBC = componentManager.ConfigureComponent(createRoutineButtonComponent, OnCreateRoutineButtonPressed);

        await configureCRBC;

        createRoutineButtonComponent = configureCRBC.Result;

        return true;
    }
}
