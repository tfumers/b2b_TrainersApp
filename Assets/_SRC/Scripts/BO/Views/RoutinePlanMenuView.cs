using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RoutinePlanMenuView : MenuView 
{
    public UnityEvent<Daily> OnDailyActivityClicked = new UnityEvent<Daily>();

    public Transform scrollViewComponentTransform;

    ComponentController componentController;
    RoutineController routineController;

    List<DailyComponent> dailyActivityComponents;

    public override Task<bool> InitializeReferences()
    {
        componentController = B2BTrainer.Instance.controllerManager.componentController;
        routineController = B2BTrainer.Instance.controllerManager.routineController;
        dailyActivityComponents = new List<DailyComponent>();
        /*
        if (!clientInfoController.ClientHasRoutine())
        {
            return Task.FromResult(false);
        }*/

        return Task.FromResult(true);
    }

    public override async Task<bool> LoadView()
    {
        Task<List<DailyComponent>> getDailyComponents = null;// componentController.CreateDailyActivityComponents(scrollViewComponentTransform, OnDailyActivityClicked);

        await getDailyComponents;

        dailyActivityComponents = getDailyComponents.Result;

        return true;
    }
}
