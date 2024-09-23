using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingSelectorForActivityEditEmergent : EmergentView
{
    ComponentManager componentManager;

    [SerializeField] TrainingSelectorComponent trainingSelectorComponent;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<TrainingSelectorComponent> configureSelectorComponent = componentManager.ConfigureComponent(trainingSelectorComponent);

        await configureSelectorComponent;

        trainingSelectorComponent = configureSelectorComponent.Result;

        return true;
    }
}
