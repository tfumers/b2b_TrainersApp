using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientInfoEmergentView : EmergentView
{
    ComponentManager componentManager;

    [SerializeField] CompleteClientInfoComponent completeClientInfoComponent;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<CompleteClientInfoComponent> configureComponent = componentManager.ConfigureComponent(completeClientInfoComponent);

        await configureComponent;

        completeClientInfoComponent = configureComponent.Result;

        return true;
    }
}
