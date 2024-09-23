using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MenuView
{
    [SerializeField] UnityEvent OnChangeAvatarClicked = new UnityEvent();

    ComponentManager componentManager;

    [SerializeField] PendingClientsCountComponent pendingClientsCountComponent;

    [SerializeField] ChangeTrainerAvatarComponent changeTrainerAvatarComponent;

    public override async Task<bool> InitializeReferences()
    {
        //clientInfoController = B2BTrainer.Instance.controllerManager.clientInfoController;
        componentManager = B2BTrainer.Instance.componentManager;
        return true;
    }

    public override async Task<bool> LoadView()
    {
        Task<PendingClientsCountComponent> configurePCCC_Task = componentManager.ConfigureComponent(pendingClientsCountComponent);

        await configurePCCC_Task;

        pendingClientsCountComponent = configurePCCC_Task.Result;

        Task<ChangeTrainerAvatarComponent> configureCTAC_Task = componentManager.ConfigureComponent(changeTrainerAvatarComponent, OnChangeAvatarClicked);

        await configureCTAC_Task;

        changeTrainerAvatarComponent = configureCTAC_Task.Result;

        //levelBarComponent.PrepareLevelBarComponent(getClientInfo.Result);

        return true;
    }
}
