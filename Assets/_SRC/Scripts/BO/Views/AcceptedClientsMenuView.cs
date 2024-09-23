using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class AcceptedClientsMenuView : MenuView, ICreateListOfInteractiveComponentsInView<AcceptedClientComponent>
{
    ComponentManager componentManager; 

    List<AcceptedClientComponent> acceptedClientComponents;

    [SerializeField] Transform scrollViewTransform;

    public UnityEvent OnAcceptedClientComponentClicked = new UnityEvent();

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public override async Task<bool> LoadView()
    {
        Task<List<AcceptedClientComponent>> getAcceptedClients = GetAppComponents(acceptedClientComponents, scrollViewTransform, OnAcceptedClientComponentClicked);

        await getAcceptedClients;

        acceptedClientComponents = getAcceptedClients.Result;

        return true;
    }

    public async Task<List<AcceptedClientComponent>> GetAppComponents(List<AcceptedClientComponent> components, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        Task<List<AcceptedClientComponent>> getAcceptedClientComponent = componentManager.CreateListOfComponents(components, scrollViewTransform, onClickedComponentEvent);

        await getAcceptedClientComponent;

        return getAcceptedClientComponent.Result;
    }
}
