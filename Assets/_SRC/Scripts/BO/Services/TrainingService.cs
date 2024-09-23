using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingService : Service
{
    TrainingRepository trainingRepository;

    LocalFilesService localFilesService;

    public override void Initialize()
    {
        localFilesService = B2BTrainer.Instance.serviceManager.localFilesService;
        trainingRepository = B2BTrainer.Instance.repositoryManager.trainingRepository;
    }

    public Task<ServiceResponse<List<Training>>> GetAllTrainings()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<Training>> GetTrainingById(long id)
    {
        bool completed;
        string message = "";

        Task<RepositoryResponse<Training>> getTrainingFromRepository = trainingRepository.FindById(id);

        await getTrainingFromRepository;

        if (getTrainingFromRepository.Result.Returned != null)
        {
            Training returnedTraining = getTrainingFromRepository.Result.Returned;

            Task<ServiceResponse<TrainingImage>> getTrainingImage = localFilesService.GetTrainignImageByName(returnedTraining.ImageUrl);

            await getTrainingImage;

            if (getTrainingImage.Result.Completed)
            {
                returnedTraining.TrainingImage = getTrainingImage.Result.Returned;
            }

            Task<ServiceResponse<TrainingVideo>> getTrainingVideo = localFilesService.GetTrainignVideoByName(returnedTraining.VideoUrl);

            await getTrainingVideo;

            if (getTrainingVideo.Result.Completed)
            {
                returnedTraining.TrainingVideo = getTrainingVideo.Result.Returned;
            }

            completed = true;

            return new ServiceResponse<Training>(completed, message, returnedTraining);
        }
        else
        {
            completed = false;
            return new ServiceResponse<Training>(completed, message, new Training());
            
        }
    }

    internal async Task<ServiceResponse<List<Training>>> GetTrainingsByParams(TrainingSearchParamsOutDTO trainingSearchParamsOutDTO)
    {
        bool completed;
        string message = "";

        Task<RepositoryResponse<List<Training>>> getTrainingsByParams = trainingRepository.GetByParams(trainingSearchParamsOutDTO);

        await getTrainingsByParams;

        if (getTrainingsByParams.Result.Returned != null)
        {
            Debug.Log("El training llegó al service");

            List<Training> returnedTrainings = getTrainingsByParams.Result.Returned;

            foreach(Training tr in returnedTrainings)
            {
                Task<ServiceResponse<TrainingImage>> getTrainingImage = localFilesService.GetTrainignImageByName(tr.ImageUrl);

                await getTrainingImage;

                if (getTrainingImage.Result.Completed)
                {
                    tr.TrainingImage = getTrainingImage.Result.Returned;
                }

                Task<ServiceResponse<TrainingVideo>> getTrainingVideo = localFilesService.GetTrainignVideoByName(tr.VideoUrl);

                await getTrainingVideo;

                if (getTrainingVideo.Result.Completed)
                {
                    tr.TrainingVideo = getTrainingVideo.Result.Returned;
                }
            }

            completed = true;

            return new ServiceResponse<List<Training>>(completed, message, returnedTrainings);
        }
        else
        {
            Debug.Log("El training no llegó al service");
            completed = false;
            return new ServiceResponse<List<Training>>(completed, message, null);

        }
    }

    public async Task<ServiceResponse> CreateNewTraining(NewTrainingOutDTO newTrainingOutDTO)
    {
        Task<RepositoryResponse<bool>> saveTraining = trainingRepository.CreateNew(newTrainingOutDTO);

        await saveTraining;

        return new ServiceResponse(saveTraining.Result.Returned, saveTraining.Result.Message);
    }
}
