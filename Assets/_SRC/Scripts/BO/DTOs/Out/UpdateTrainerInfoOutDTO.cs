using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTrainerInfoOutDTO : OutDTO
{
    public UpdateTrainerInfoOutDTO(Dictionary<string, string> info) : base(info)
    {
    }

    public override WWWForm ToForm()
    {
        throw new System.NotImplementedException();
    }
}
