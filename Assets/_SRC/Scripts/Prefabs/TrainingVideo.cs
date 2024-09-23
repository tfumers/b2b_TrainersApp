using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingVideo
{
    int id;

    string name;

    RuntimeAnimatorController animatorController;

    public TrainingVideo(int id, string name, RuntimeAnimatorController animatorController)
    {
        this.id = id;
        this.name = name;
        this.animatorController = animatorController;
    }

    public int Id { get => id; }
    public string Name { get => name; }
    public RuntimeAnimatorController AnimatorController { get => animatorController; set => animatorController = value; }
}
