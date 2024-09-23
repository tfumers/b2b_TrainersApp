using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingVideoContainerComponent : AppComponent<TrainingVideo>
{
    [SerializeField] Animator animator;

    protected override void Prepare(TrainingVideo model)
    {
        animator.runtimeAnimatorController = model.AnimatorController;
    }
}
