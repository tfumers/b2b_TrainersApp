using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerService : Service
{
    public override void Initialize()
    {

    }

    public Task<ServiceResponse<List<Trainer>>> GetAllTrainers()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<Trainer>> GetTrainerById(long id)
    {
        throw new NotImplementedException();
    }
}
