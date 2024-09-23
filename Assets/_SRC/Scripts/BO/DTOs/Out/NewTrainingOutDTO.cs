using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrainingOutDTO : OutDTO
{
    public NewTrainingOutDTO(Training training) : base(new Dictionary<string, string>())
    {
        fields.Add("id", training.Id.ToString());
        fields.Add("name", training.Name);
        fields.Add("description", training.Description);
        fields.Add("difficulty", training.Difficulty);
        for(int i = 0; i < training.Categories.Length; i++)
        {
            fields.Add("categories["+ i +"]", training.Categories[i]);
        }
        fields.Add("videoUrl", training.VideoUrl);
        Debug.Log("videoUrl desde el DTO: " + training.VideoUrl);
        fields.Add("imageUrl", training.ImageUrl);
        fields.Add("estTimePerRep", training.EstTimePerRep.ToString());
        fields.Add("estCaloriesPerRep", training.EstCaloriesPerRep.ToString());
    }

    public NewTrainingOutDTO(Dictionary<string, string> info) : base(info)
    {
        fields = info;
    }


}
