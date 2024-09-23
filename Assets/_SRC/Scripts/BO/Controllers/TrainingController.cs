using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingController : Controller
{
    NotificationManager notificationManager;

    TrainingService trainingService;

    public override Task<bool> Initialize()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;

        trainingService = B2BTrainer.Instance.serviceManager.trainingService;

        return Task.FromResult(true);
    }

    public async Task<Training> GetTrainingById(long id)
    {
        Task<ServiceResponse<Training>> getTrainingById = trainingService.GetTrainingById(id);

        await getTrainingById;

        if (getTrainingById.Result.Completed == false)
        {
            notificationManager.LoginFailed(getTrainingById.Result.Message);
        }

        return getTrainingById.Result.Returned;
    }
    /*
    public async Task<List<Training>> GetTrainingsByParams(string[] args)
    {
        Task<ServiceResponse<List<Training>>> getTrainingsByParams = trainingService.GetTrainingsByParams(args);

        await getTrainingsByParams;

        if (getTrainingsByParams.Result.Completed == false)
        {
            notificationManager.LoginFailed(getTrainingsByParams.Result.Message);
        }

        return getTrainingsByParams.Result.Returned;
    }*/

    public async Task<List<Training>> GetTrainingsBySearchParams(string stringToSearch, string stringParam)
    {
        List<Training> trainings = new List<Training>();

        TrainingSearchParamsOutDTO searchParamsOutDTO;

        switch (stringParam)
        {
            case "Name":
                searchParamsOutDTO = new TrainingSearchParamsOutDTO("", "", stringToSearch, "");
                break;
            case "Category":
                searchParamsOutDTO = new TrainingSearchParamsOutDTO(stringToSearch, "", "", "");
                break;
            case "Difficulty":
                searchParamsOutDTO = new TrainingSearchParamsOutDTO("", stringToSearch, "", "");
                break;
            case "Description":
                searchParamsOutDTO = new TrainingSearchParamsOutDTO("", "", "", stringToSearch);
                break;
            default:
                searchParamsOutDTO = new TrainingSearchParamsOutDTO();
                break;
        }
    
        Task<ServiceResponse<List<Training>>> getTrainingsByParams = trainingService.GetTrainingsByParams(searchParamsOutDTO);

        await getTrainingsByParams;

        if (getTrainingsByParams.Result.Completed)
        {
            //Debug.Log("tRAINING CONTROLLER success");
            trainings = getTrainingsByParams.Result.Returned;

            if (trainings.Count == 0)
            {
                notificationManager.GenericNotification("No se encontró entrenamiento que cumpliera con los datos ingresados. Revise o intente nuevamente con otros valores.");
            }
        }
        else
        {
            //Debug.Log("tRAINING CONTROLLER failure");
            notificationManager.GenericError(getTrainingsByParams.Result.Message);
        }
                
        /*
        long defaultValue = 1;
        if(long.TryParse(stringToSearch, out defaultValue))
        {
            

            Task<Training> getTraining = GetTrainingById(long.Parse(stringToSearch));

            await getTraining;

            trainings.Add(getTraining.Result);
        }
        else
        {
            Task<Training> getTraining = GetTrainingById(defaultValue);

            await getTraining;

            trainings.Add(getTraining.Result);
        }
        */
        return trainings;
        /*Task<ServiceResponse<List<Training>>> getTrainingsByParams = trainingService.GetTrainingsByParams(args);

        await getTrainingsByParams;

        if (getTrainingsByParams.Result.Completed == false)
        {
            notificationManager.LoginFailed(getTrainingsByParams.Result.Message);
        }

        return getTrainingsByParams.Result.Returned;*/
    }

    public async Task<List<Training>> GetAllTrainings()
    {
        Task<ServiceResponse<List<Training>>> getAllTrainings = trainingService.GetAllTrainings();

        await getAllTrainings;

        if (getAllTrainings.Result.Completed == false)
        {
            notificationManager.LoginFailed(getAllTrainings.Result.Message);
        }

        return getAllTrainings.Result.Returned;
    }

    public async Task<bool> CreateNewTraining(Training received)
    {
        Task<ServiceResponse> createNewTrainingTask = trainingService.CreateNewTraining(new NewTrainingOutDTO(received));

        await createNewTrainingTask;

        if (createNewTrainingTask.Result.Completed == false)
        {
            notificationManager.GenericError(createNewTrainingTask.Result.Message);
            return false;
        }

        return true;
    }
}
