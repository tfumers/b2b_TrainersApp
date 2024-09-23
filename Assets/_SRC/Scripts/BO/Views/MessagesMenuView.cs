using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MessagesMenuView : MenuView, ICreateListOfAppComponentsInView<ClientMessageComponent>
{
    ComponentManager componentManager;

    List<ClientMessageComponent> clientMessageComponents;

    [SerializeField] Transform scrollViewTransform;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return base.InitializeReferences();
    }

    public override async Task<bool> LoadView()
    {
        Task<List<ClientMessageComponent>> loadAndPrepareComponents = GetAppComponents(clientMessageComponents, scrollViewTransform);

        await loadAndPrepareComponents;

        clientMessageComponents = loadAndPrepareComponents.Result;

        return true;
    }

    public async Task<List<ClientMessageComponent>> GetAppComponents(List<ClientMessageComponent> components, Transform transform)
    {

        Task<List<ClientMessageComponent>> createMessageComponents = componentManager.CreateListOfComponents(components, transform);

        await createMessageComponents;

        return createMessageComponents.Result;
    }
}
