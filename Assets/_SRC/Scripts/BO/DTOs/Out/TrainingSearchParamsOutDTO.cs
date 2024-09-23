using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSearchParamsOutDTO : OutDTO
{
    public TrainingSearchParamsOutDTO(Dictionary<string, string> info) : base(info)
    {
        this.fields = info;
    }

    public TrainingSearchParamsOutDTO(string category = "", string difficulty = "", string name = "", string desc = "") : base(new Dictionary<string, string>())
    {
        fields.Add("category", category);
        Debug.Log("category " + category);
        Debug.Log("difficulty " + difficulty);
        Debug.Log("name " + name);
        Debug.Log("desc " + desc);
        fields.Add("difficulty", difficulty);
        fields.Add("name", name);
        fields.Add("desc", desc);
    }
}
