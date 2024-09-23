using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class PendingClientsMenuView : MenuView
{
    [SerializeField] UnityEvent OnPendingNewClientClicked = new UnityEvent();

    ComponentManager componentManager;

    [SerializeField] Transform scrollViewTransform;

    List<PendingNewClientComponent> pendingNewClientComponents = new List<PendingNewClientComponent>();

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public async override Task<bool> LoadView()
    {
        Task<List<PendingNewClientComponent>> getPendingNewClients = componentManager.CreateListOfComponents(pendingNewClientComponents, scrollViewTransform, OnPendingNewClientClicked);

        await getPendingNewClients;

        pendingNewClientComponents = getPendingNewClients.Result;

        return true;
    }
}
