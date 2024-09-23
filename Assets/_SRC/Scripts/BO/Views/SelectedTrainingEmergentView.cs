using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SelectedTrainingEmergentView : EmergentView
{
    ComponentManager componentManager;

    [SerializeField] UnityEvent onEditTrainingButtonClick = new UnityEvent();

    [SerializeField] CurrentTrainingInfoComponent currentTrainingInfoComponent;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<CurrentTrainingInfoComponent> configureCTIC_Task = componentManager.ConfigureComponent(currentTrainingInfoComponent);

        await configureCTIC_Task;

        currentTrainingInfoComponent = configureCTIC_Task.Result;

        return true;
    }
}
