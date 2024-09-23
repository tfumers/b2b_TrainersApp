using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActTypeSelectorComponent : AppComponent<int>
{
    public UnityEvent<int> OnSelectorChanged = new UnityEvent<int>();

    [SerializeField] Button SelectorRep;
    [SerializeField] Button SelectorTimer;

    protected override void Prepare(int model)
    {
        SelectorRep.onClick.RemoveAllListeners();
        SelectorTimer.onClick.RemoveAllListeners();

        SelectorRep.onClick.AddListener(() => OnSelectorChanged.Invoke(Constant.ACTIVITY_TYPE_REPS));
        SelectorTimer.onClick.AddListener(() => OnSelectorChanged.Invoke(Constant.ACTIVITY_TYPE_TIMER));
    }
}
