using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoutineService : Service
{
    TrainingService trainingService;

    RoutineRepository routineRepository;

    public override void Initialize()
    {
        trainingService = B2BTrainer.Instance.serviceManager.trainingService;

        routineRepository = B2BTrainer.Instance.repositoryManager.routineRepository;
    }

    public async Task<ServiceResponse> CreateNewRoutine(NewRoutineOutDTO newRoutineOutDTO)
    {
        Task<RepositoryResponse<bool>> saveRoutine = routineRepository.CreateNew(newRoutineOutDTO);

        await saveRoutine;

        return new ServiceResponse(saveRoutine.Result.Returned, saveRoutine.Result.Message);
    }

    internal async Task<ServiceResponse<Routine>> GetRoutineById(long id)
    {
        string message = "";

        Routine routine;

        Task<RepositoryResponse<Routine>> getRoutine = routineRepository.FindById(id);

        await getRoutine;

        routine = getRoutine.Result.Returned;

        message += getRoutine.Result.Message;

        foreach (Daily da in routine.DailyActivities)
        {
            foreach(Activity act in da.Activities)
            {
                Task<ServiceResponse<Training>> getTraining = trainingService.GetTrainingById(act.TrainingId);

                await getTraining;

                if (getTraining.Result.Completed)
                {
                    act.Training = getTraining.Result.Returned;
                }
                else
                {
                    message += getTraining.Result.Message;
                    return new ServiceResponse<Routine>(false, message, routine);

                }
            }
        }

        if (getRoutine.Result.Returned != null)
        {
            return new ServiceResponse<Routine>(true, message, getRoutine.Result.Returned);
        }
        else
        {
            return new ServiceResponse<Routine>(false, message, new Routine());
        }
    }

    internal Task<ServiceResponse> EditRoutine(EditRoutineOutDTO editRoutineOutDTO)
    {
        throw new NotImplementedException();
    }
}
