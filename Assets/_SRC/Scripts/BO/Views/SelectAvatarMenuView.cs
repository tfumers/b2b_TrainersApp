using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SelectAvatarMenuView : MenuView
{
    [SerializeField] UnityEvent<Avatar> OnSelectedAvatar = new UnityEvent<Avatar>();

    [SerializeField] UnityEvent OnSubmitButtonPressed = new UnityEvent();

    ComponentManager componentManager;

    public List<ClickableAvatarComponent> clickableAvatarComponents;

    [SerializeField] SubmitSelectedAvatarComponent submitSelectedAvatarComponent;

    [SerializeField] Transform pos;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return base.InitializeReferences();
    }

    public override async Task<bool> LoadView()
    {

        //Task<List<ClickableAvatarComponent>> prepareClickableAvatars = componentController.CreateSelectableAvatarComponent(pos, OnSelectedAvatar);
        //await prepareClickableAvatars;
        //clickableAvatarComponents = prepareClickableAvatars.Result;

        Task<List<ClickableAvatarComponent>> createClickableAvatarComponents = componentManager.CreateListOfComponents(clickableAvatarComponents, pos, OnSelectedAvatar);
        await createClickableAvatarComponents;
        clickableAvatarComponents = createClickableAvatarComponents.Result;

        Task<SubmitSelectedAvatarComponent> configureSubmitSelectedAvatarComponent = componentManager.ConfigureComponent(submitSelectedAvatarComponent, OnSubmitButtonPressed);
        await configureSubmitSelectedAvatarComponent;
        submitSelectedAvatarComponent = configureSubmitSelectedAvatarComponent.Result;

        return true;
    }
}
