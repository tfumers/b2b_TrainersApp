using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyActivityMenuView : MenuView
{

    ComponentManager componentManager;

    [SerializeField] UnityEvent OnActivityButtonClicked = new UnityEvent();

    [SerializeField] Transform activityScrollViewTransform;

    List<ActivityComponent> activityComponents;

    public override Task<bool> InitializeReferences()
    {
        componentManager = B2BTrainer.Instance.componentManager;

        return Task.FromResult(true);
    }

    public async override Task<bool> LoadView()
    {
        Task<List<ActivityComponent>> createActivityComponents_Task = componentManager.CreateListOfComponents(activityComponents, activityScrollViewTransform, OnActivityButtonClicked);

        await createActivityComponents_Task;

        activityComponents = createActivityComponents_Task.Result;

        return true;
    }
}
