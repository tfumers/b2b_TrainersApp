using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CurrentTrainerController : Controller
{
    CurrentTrainerService currentTrainerService;

    NotificationManager notificationManager;

    public override Task<bool> Initialize()
    {
        currentTrainerService = B2BTrainer.Instance.serviceManager.currentTrainerService;

        notificationManager = B2BTrainer.Instance.notificationManager;

        return Task.FromResult(true);
    }

    public async Task<bool> LoginTrainer(Dictionary<string, string> received)
    {
        Task<ServiceResponse> loginTrainer = currentTrainerService.LoginTrainer(new LoginInfoOutDTO(received));

        await loginTrainer;

        if (loginTrainer.Result.Completed == false)
        {
            notificationManager.LoginFailed(loginTrainer.Result.Message);
        }

        return loginTrainer.Result.Completed;
    }

    public async Task<bool> RegisterTrainer(Dictionary<string, string> received)
    {
        Task<ServiceResponse> registerTrainer = currentTrainerService.RegisterTrainer(new RegisterTrainerOutDTO(received));

        await registerTrainer;

        if (registerTrainer.Result.Completed == false)
        {
            notificationManager.RegisterFailed(registerTrainer.Result.Message);
        }

        return registerTrainer.Result.Completed;
    }

    public async Task<Trainer> GetCurrentTrainerInfo()
    {
        Task<ServiceResponse<Trainer>> getCurrentTrainer = currentTrainerService.GetCurrentTrainerInfo();

        await getCurrentTrainer;

        if (getCurrentTrainer.Result.Completed == false)
        {
            notificationManager.RegisterFailed(getCurrentTrainer.Result.Message);
        }

        return getCurrentTrainer.Result.Returned;
    }

    public async Task<bool> EditCurrentTrainerInfo(Dictionary<string, string> received)
    {
        Task<ServiceResponse> editCurrentTrainerInfo = currentTrainerService.UpdateCurrentTrainerInfo(new UpdateTrainerInfoOutDTO(received));

        await editCurrentTrainerInfo;

        if (editCurrentTrainerInfo.Result.Completed == false)
        {
            notificationManager.GenericError(editCurrentTrainerInfo.Result.Message);
        }

        return editCurrentTrainerInfo.Result.Completed;
    }

    public async Task<bool> UpdateTrainerAvatar(Dictionary<string, string> received)
    {
        Task<ServiceResponse> updateTrainerAvatar = currentTrainerService.UpdateTrainerAvatar(new UpdateAvatarOutDTO(received));

        await updateTrainerAvatar;

        if (updateTrainerAvatar.Result.Completed == false)
        {
            notificationManager.GenericError(updateTrainerAvatar.Result.Message);
        }

        return updateTrainerAvatar.Result.Completed;
    }

    //Componentes interactivos que estan en memoria del equipo de entrenador
    public TrainerClientRelation GetSelectedTrainerClientRelation()
    {
        ServiceResponse<TrainerClientRelation> getTrainerClientRelation = currentTrainerService.GetSelectedTrainerClientRelation();

        if (getTrainerClientRelation.Completed == false)
        {
            notificationManager.GenericError(getTrainerClientRelation.Message);
        }


        return getTrainerClientRelation.Returned;
    }

    public void SetSelectedTrainerClientRelation(TrainerClientRelation trainerClientRelation)
    {
        ServiceResponse setTrainerClientRelation = currentTrainerService.SetSelectedTrainerClientRelation(trainerClientRelation);

        if (setTrainerClientRelation.Completed == false)
        {
            notificationManager.GenericError(setTrainerClientRelation.Message);
        }
    }

    public Client GetSelectedClient()
    {
        ServiceResponse<Client> getClient = currentTrainerService.GetSelectedClient();

        if (getClient.Completed == false)
        {
            notificationManager.GenericError(getClient.Message);
        }

        return getClient.Returned;
    }

    public void SetSelectedClient(Client client)
    {
        ServiceResponse getClient = currentTrainerService.SetSelectedClient(client);

        if (getClient.Completed == false)
        {
            notificationManager.GenericError(getClient.Message);
        }
    }

    public Routine GetSelectedRoutine()
    {
        ServiceResponse<Routine> getRoutine = currentTrainerService.GetSelectedRoutine();

        if (getRoutine.Completed == false)
        {
            notificationManager.GenericError(getRoutine.Message);
        }

        return getRoutine.Returned;
    }

    public void SetSelectedRoutine(Routine routine)
    {
        ServiceResponse setRoutine = currentTrainerService.SetSelectedRoutine(routine);

        if (setRoutine.Completed == false)
        {
            notificationManager.GenericError(setRoutine.Message);
        }
    }

    public Daily GetSelectedDaily()
    {
        ServiceResponse<Daily> getDaily = currentTrainerService.GetSelectedDaily();

        if (getDaily.Completed == false)
        {
            notificationManager.GenericError(getDaily.Message);
        }

        return getDaily.Returned;
    }

    internal void SetSelectedDaily(Daily daily)
    {
        ServiceResponse setDaily = currentTrainerService.SetSelectedDaily(daily);

        if (setDaily.Completed == false)
        {
            notificationManager.GenericError(setDaily.Message);
        }
    }

    public Activity GetSelectedActivity()
    {
        ServiceResponse<Activity> getActivity = currentTrainerService.GetSelectedActivity();

        if (getActivity.Completed == false)
        {
            notificationManager.GenericError(getActivity.Message);
        }

        return getActivity.Returned;
    }

    internal void SetSelectedActivity(Activity activity)
    {
        ServiceResponse setActivity = currentTrainerService.SetSelectedActivity(activity);

        if (setActivity.Completed == false)
        {
            notificationManager.GenericError(setActivity.Message);
        }
    }

    public Training GetSelectedTraining()
    {
        ServiceResponse<Training> getTraining = currentTrainerService.GetSelectedTraining();

        if (getTraining.Completed == false)
        {
            notificationManager.GenericError(getTraining.Message);
        }

        return getTraining.Returned;
    }

    internal void SetSelectedTraining(Training training)
    {
        ServiceResponse setTraining = currentTrainerService.SetSelectedTraining(training);

        if (setTraining.Completed == false)
        {
            notificationManager.GenericError(setTraining.Message);
        }
    }

    public Avatar GetSelectedAvatarToUpdate()
    {
        ServiceResponse<Avatar> getAvatar = currentTrainerService.GetSelectedAvatarToUpdate();

        if (getAvatar.Completed == false)
        {
            notificationManager.GenericError(getAvatar.Message);
        }

        return getAvatar.Returned;
    }

    internal void SetSelectedAvatarToUpdate(Avatar avatar)
    {
        ServiceResponse setAvatar = currentTrainerService.SetSelectedAvatarToUpdate(avatar);

        Debug.Log("Selected avatar " + avatar.Id);

        if (setAvatar.Completed == false)
        {
            notificationManager.GenericError(setAvatar.Message);
        }
    }

    //components to edit

    public TrainerClientRelation GetTrainerClientRelationToEndEditRoutine()
    {
        ServiceResponse<TrainerClientRelation> getTCRtoEndEdit = currentTrainerService.GetTrainerClientRelationToEndEditRoutine();

        if (getTCRtoEndEdit.Completed == false)
        {
            notificationManager.GenericError(getTCRtoEndEdit.Message);
        }

        return getTCRtoEndEdit.Returned;
    }

    public void DeleteRoutineToEdit()
    {
        ServiceResponse clearRoutineToEdit = currentTrainerService.ClearRoutineToEdit();

        if (clearRoutineToEdit.Completed == false)
        {
            notificationManager.GenericError(clearRoutineToEdit.Message);
        }
    }

    public Routine GetRoutineToEdit()
    {
        ServiceResponse<Routine> getRoutineToEdit = currentTrainerService.GetRoutineToEdit();

        if (getRoutineToEdit.Completed == false)
        {
            notificationManager.GenericError(getRoutineToEdit.Message);
        }

        return getRoutineToEdit.Returned;
    }

    public void SetRoutineToEdit()
    {
        ServiceResponse setRoutine = currentTrainerService.SetRoutineToEdit();

        if (setRoutine.Completed == false)
        {
            notificationManager.GenericError(setRoutine.Message);
        }
    }

    public Task<Training> GetTrainingToEdit()
    {
        return Task.FromResult(new Training());
    }


    //Daily to edit

    public void AddDailyActivityToEdit()
    {
        ServiceResponse addDaily = currentTrainerService.AddDailyToEdit();

        if (addDaily.Completed == false)
        {
            notificationManager.GenericError(addDaily.Message);
        }
    }

    public Daily GetDailyToEdit()
    {
        ServiceResponse<Daily> getDaily = currentTrainerService.GetDailyToEdit();

        if (getDaily.Completed == false)
        {
            notificationManager.GenericError(getDaily.Message);
        }

        return getDaily.Returned;
    }

    public void SetDailyToEdit(Daily daily)
    {
        ServiceResponse setDaily = currentTrainerService.SetDailyToEdit(daily);

        if (setDaily.Completed == false)
        {
            notificationManager.GenericError(setDaily.Message);
        }
    }

    public void SaveDailyToEdit()
    {
        ServiceResponse setDaily = currentTrainerService.SaveDailyToEdit();

        if (setDaily.Completed == false)
        {
            notificationManager.GenericError(setDaily.Message);
        }
    }

    public void DeleteCurrentDailyToEdit()
    {
        ServiceResponse deleteDaily = currentTrainerService.DeleteCurrentDailyToEdit();

        if (deleteDaily.Completed == false)
        {
            notificationManager.GenericError(deleteDaily.Message);
        }
    }

    public void AddActivityToEdit()
    {
        ServiceResponse addActivity = currentTrainerService.AddActivityToEdit();

        if (addActivity.Completed == false)
        {
            notificationManager.GenericError(addActivity.Message);
        }
    }

    public Activity GetActivityToEdit()
    {
        ServiceResponse<Activity> getActivity = currentTrainerService.GetActivityToEdit();

        if (getActivity.Completed == false)
        {
            notificationManager.GenericError(getActivity.Message);
        }

        return getActivity.Returned;
    }

    public void SetActivityToEdit(Activity activity)
    {
        ServiceResponse setActivity = currentTrainerService.SetActivityToEdit(activity);

        if (setActivity.Completed == false)
        {
            notificationManager.GenericError(setActivity.Message);
        }
    }

    public void SaveActivityToEdit()
    {
        ServiceResponse saveActivity = currentTrainerService.SaveActivityToEdit();

        if (saveActivity.Completed == false)
        {
            notificationManager.GenericError(saveActivity.Message);
        }
    }

    public void DeleteCurrentActivityToEdit()
    {
        ServiceResponse deleteActivity = currentTrainerService.DeleteCurrentActivityToEdit();

        if (deleteActivity.Completed == false)
        {
            notificationManager.GenericError(deleteActivity.Message);
        }
    }

    public void SaveSelectedTrainingInActivityToEdit(Training training)
    {
        ServiceResponse saveActivity = currentTrainerService.SaveTrainingInActivityToEdit(training);

        if (saveActivity.Completed == false)
        {
            notificationManager.GenericError(saveActivity.Message);
        }
    }

    public void CancelTrainingToEdit()
    {
        ServiceResponse cancelTraining = currentTrainerService.CancelTrainingToEdit();

        if (cancelTraining.Completed == false)
        {
            notificationManager.GenericError(cancelTraining.Message);
        }
    }
}
