using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RegisterMenuView : MenuView
{
    public UnityEvent OnRegisterSuccess = new UnityEvent();

    ComponentManager componentManager;

    [SerializeField] RegisterNewTrainerButtonComponent registerNewTrainerButtonComponent;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;
        return Task.FromResult(true);
    }

    public override async Task<bool> LoadView()
    {
        Task<RegisterNewTrainerButtonComponent> configureRegisterComponent = componentManager.ConfigureComponent(registerNewTrainerButtonComponent, OnRegisterSuccess);

        await configureRegisterComponent;

        registerNewTrainerButtonComponent = configureRegisterComponent.Result;

        return true;
    }
}
