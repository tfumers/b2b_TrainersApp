using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CurrentTrainerService : Service
{
    WebConnector webConnector;

    LocalFilesService localFilesService;

    TrainerRepository trainerRepository;

    //Objetos contenidos en memoria
    TrainerClientRelation selectedTrainerClientRelation;

    Client selectedClient;

    Routine selectedRoutine;

    Daily selectedDaily;

    Activity selectedActivity;

    Training selectedTraining;

    Avatar selectedAvatarToUpdate;

    Routine routineToEdit;

    Daily dailyToEdit;

    Activity activityToEdit;

    public override void Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;

        localFilesService = B2BTrainer.Instance.serviceManager.localFilesService;

        trainerRepository = B2BTrainer.Instance.repositoryManager.trainerRepository;
    }

    public async Task<ServiceResponse> LoginTrainer(LoginInfoOutDTO loginInfoOutDTO)
    {
        Task<ConnectorInResponse> loginTask = webConnector.PostConnection(new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_LOGIN, loginInfoOutDTO));

        await loginTask;

        if (webConnector.HttpStatusOk(loginTask.Result.HttpStatus))
        {
            return new ServiceResponse(true, "Login success");
        }
        else
        {
            return new ServiceResponse(false, loginTask.Result.Message);
        }
    }

    public async Task<ServiceResponse> RegisterTrainer(RegisterTrainerOutDTO registerTrainerOutDTO)
    {
        Task<ConnectorInResponse> registerTask = webConnector.PostConnection(new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_REGISTER, registerTrainerOutDTO));

        await registerTask;

        if (webConnector.HttpStatusOk(registerTask.Result.HttpStatus))
        {
            return new ServiceResponse(true, "SER: Register success.");
        }
        else
        {
            return new ServiceResponse(false, registerTask.Result.Message);
        }
    }

    public async Task<ServiceResponse<Trainer>> GetCurrentTrainerInfo()
    {
        Task<RepositoryResponse<Trainer>> getCurrentTrainer = trainerRepository.FindCurrent();

        Trainer currentTrainer = new Trainer();

        await getCurrentTrainer;

        Task<ServiceResponse<Avatar>> getAvatar;

        getAvatar = localFilesService.GetAvatarById(getCurrentTrainer.Result.Returned.Avatar);

        await getAvatar;

        if (getCurrentTrainer.Result.Returned.Id != 0)
        {
            currentTrainer = getCurrentTrainer.Result.Returned;
            currentTrainer.AvatarImage = getAvatar.Result.Returned;

            return new ServiceResponse<Trainer>(true, "SER: Get Trainer success.", currentTrainer);
        }
        else
        {
            currentTrainer.AvatarImage = getAvatar.Result.Returned;
            return new ServiceResponse<Trainer>(false, getCurrentTrainer.Result.Message, currentTrainer);
        }
    }

    public async Task<ServiceResponse> UpdateCurrentTrainerInfo(UpdateTrainerInfoOutDTO updateTrainerInfoOutDTO)
    {
        Task<ConnectorInResponse> updateTrainerInfoTask = webConnector.PostConnection(new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_UPDATE_TRAINER_INFO, updateTrainerInfoOutDTO));

        await updateTrainerInfoTask;

        if (webConnector.HttpStatusOk(updateTrainerInfoTask.Result.HttpStatus))
        {
            return new ServiceResponse(true, "SER: Update success.");
        }
        else
        {
            return new ServiceResponse(false, updateTrainerInfoTask.Result.Message);
        }
    }

    public async Task<ServiceResponse> UpdateTrainerAvatar(UpdateAvatarOutDTO updateAvatarOutDTO)
    {
        Task<ConnectorInResponse> updateTrainerAvatarTask = webConnector.PostConnection(new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_UPDATE_TRAINER_AVATAR, updateAvatarOutDTO));

        await updateTrainerAvatarTask;

        if (webConnector.HttpStatusOk(updateTrainerAvatarTask.Result.HttpStatus))
        {
            return new ServiceResponse(true, "SER: Update success.");
        }
        else
        {
            return new ServiceResponse(false, updateTrainerAvatarTask.Result.Message);
        }
    }

    public ServiceResponse<TrainerClientRelation> GetSelectedTrainerClientRelation()
    {
        if (selectedTrainerClientRelation == null)
        {
            return new ServiceResponse<TrainerClientRelation>(false, "SER: there is no selection loaded.", new TrainerClientRelation());
        }
        else
        {
            return new ServiceResponse<TrainerClientRelation>(true, "SER: selection success.", selectedTrainerClientRelation);
        }
    }

    public ServiceResponse SetSelectedTrainerClientRelation(TrainerClientRelation trainerClientRelation)
    {
        selectedTrainerClientRelation = trainerClientRelation;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Client> GetSelectedClient()
    {
        if (selectedClient == null)
        {
            return new ServiceResponse<Client>(false, "there is no selection loaded", new Client());
        }
        else
        {
            return new ServiceResponse<Client>(true, "selection success", selectedClient);
        }
    }

    public ServiceResponse SetSelectedClient(Client client)
    {
        selectedClient = client;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Routine> GetSelectedRoutine()
    {
        if (selectedRoutine == null)
        {
            return new ServiceResponse<Routine>(false, "SER: there is no selection loaded.", new Routine());
        }
        else
        {
            return new ServiceResponse<Routine>(true, "SER: selection success.", selectedRoutine);
        }
    }

    public ServiceResponse SetSelectedRoutine(Routine routine)
    {
        selectedRoutine = routine;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Daily> GetSelectedDaily()
    {
        if (selectedDaily == null)
        {
            return new ServiceResponse<Daily>(false, "SER: there is no selection loaded.", new Daily());
        }
        else
        {
            return new ServiceResponse<Daily>(true, "SER: selection success.", selectedDaily);
        }
    }

    public ServiceResponse SetSelectedDaily(Daily daily)
    {
        selectedDaily = daily;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Activity> GetSelectedActivity()
    {
        if (selectedActivity == null)
        {
            return new ServiceResponse<Activity>(false, "SER: there is no selection loaded.", new Activity());
        }
        else
        {
            return new ServiceResponse<Activity>(true, "SER: selection success.", selectedActivity);
        }
    }

    public ServiceResponse SetSelectedActivity(Activity activity)
    {
        selectedActivity = activity;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Training> GetSelectedTraining()
    {
        if(selectedTraining == null)
        {
            return new ServiceResponse<Training>(false, "SER: there is no selection loaded.", new Training());
        }
        else
        {
            return new ServiceResponse<Training>(true, "SER: selection success.", selectedTraining);
        }
    }


    public ServiceResponse SetSelectedTraining(Training training)
    {
        selectedTraining = training;

        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse<Avatar> GetSelectedAvatarToUpdate()
    {
        if (selectedAvatarToUpdate == null)
        {
            return new ServiceResponse<Avatar>(true, "SER: there is no selection loaded.", new Avatar());
        }
        else
        {
            return new ServiceResponse<Avatar>(true, "SER: selection success.", selectedAvatarToUpdate);
        }
    }

    public ServiceResponse SetSelectedAvatarToUpdate(Avatar avatar)
    {
        selectedAvatarToUpdate = avatar;

        return new ServiceResponse(true, "SER: selection success.");
    }

    //TO EDIT

    public ServiceResponse<TrainerClientRelation> GetTrainerClientRelationToEndEditRoutine()
    {
        if (selectedTrainerClientRelation == null)
        {
            return new ServiceResponse<TrainerClientRelation>(false, "SER: there is no TCR selected.", null);
        }

        //Agregar condiciones para validad que la rutina está completa
        /*
         * Si la rutina tiene actividades diarias no completadas, enviar un falso con SER: the routine is not completed
         * 
         * 
         * 
        */
        //Enviar, en sí, la rutina
        if (selectedTrainerClientRelation.Routine != null)
        {
            selectedTrainerClientRelation.Routine = routineToEdit;

            return new ServiceResponse<TrainerClientRelation>(true, "SER: routine to edit selected.", selectedTrainerClientRelation);
        }
        else
        {
            return new ServiceResponse<TrainerClientRelation>(false, "SER: Error creating the routine", null);
        }
    }

    public ServiceResponse ClearRoutineToEdit()
    {
        selectedTrainerClientRelation.Routine = new Routine();

        return new ServiceResponse(true, "SER: clear success.");
    }

    public ServiceResponse<Routine> GetRoutineToEdit()
    {
        if (selectedTrainerClientRelation == null)
        {
            return new ServiceResponse<Routine>(false, "SER: there is no TCR selected.", null);
        }

        if (selectedTrainerClientRelation.Routine == null)
        {
            return new ServiceResponse<Routine>(true, "SER: new routine to edit created.", new Routine());
        }
        else
        {
            return new ServiceResponse<Routine>(true, "SER: routine to edit selected.", selectedTrainerClientRelation.Routine);
        }
    }


    public ServiceResponse SetRoutineToEdit(Routine routine = null)
    {
        if (selectedTrainerClientRelation == null)
        {
            return new ServiceResponse(false, "SER: there is no TCR selected.");
        }

        if (selectedTrainerClientRelation.Routine == null)
        {

            selectedTrainerClientRelation.Routine = new Routine();

        }

        routineToEdit = selectedTrainerClientRelation.Routine;
        return new ServiceResponse(true, "SER: selection success.");
    }

    public ServiceResponse AddDailyToEdit()
    {
        if (routineToEdit==null)
        {
            return new ServiceResponse(false, "SER: no routine.");
        }

        if (routineToEdit.DailyActivities == null)
        {
            routineToEdit.DailyActivities = new List<Daily>();
        }

        Daily newDaily = new Daily();
        newDaily.OrderNumber = routineToEdit.DailyActivities.Count + 1;
        newDaily.ProposedDate = DateTime.Today.AddDays(10 + newDaily.OrderNumber);
        routineToEdit.AddDailyActivity(newDaily);
        selectedTrainerClientRelation.Routine = routineToEdit;

        SaveDailyToEdit();

        return new ServiceResponse(true, "SER: added daily to edit.");
    }


    public ServiceResponse<Daily> GetDailyToEdit()
    {
        if (selectedTrainerClientRelation == null)
        {
            return new ServiceResponse<Daily>(false, "SER: there is no selection loaded.", new Daily());
        }
        else
        {
            return new ServiceResponse<Daily>(true, "SER: selection success.", dailyToEdit);
        }
    }

    public ServiceResponse SetDailyToEdit(Daily daily)
    {
        if (routineToEdit.DailyActivities.Contains(daily))
        {
            dailyToEdit = daily;
            return new ServiceResponse(true, "SER: selection success.");
        }
        else
        {
            return new ServiceResponse(false, "SER: selection failed.");
        }
    }

    internal ServiceResponse CancelTrainingToEdit()
    {
        throw new NotImplementedException();
    }

    public ServiceResponse SaveDailyToEdit()
    {
        try{
            routineToEdit.DailyActivities.Sort((da1, da2) => (da1.ProposedDate.CompareTo(da2.ProposedDate)));

            for(int i = 0; i < routineToEdit.DailyActivities.Count; i++)
            {
                routineToEdit.DailyActivities[i].OrderNumber = i + 1;

            }

            SaveChangesInRoutine();

            return new ServiceResponse(true, "SER: save success.");
        }
        catch(Exception e)
        {
            return new ServiceResponse(false, "SER: save failed.");
        }
    }

    private void SaveChangesInRoutine()
    {
        if (routineToEdit.DailyActivities.Count > 0)
        {
            routineToEdit.StartDate = routineToEdit.DailyActivities[0].ProposedDate;
        }

        
        routineToEdit.NumberOfDays = routineToEdit.DailyActivities.Count;
    }

    public ServiceResponse DeleteCurrentDailyToEdit()
    {
        if (routineToEdit.DailyActivities.Contains(dailyToEdit))
        {
            routineToEdit.DailyActivities.Remove(dailyToEdit);
            dailyToEdit = null;
            return new ServiceResponse(true, "SER: selection success.");
        }
        else
        {
            return new ServiceResponse(false, "SER: selection failed.");
        }
    }

    internal ServiceResponse AddActivityToEdit()
    {
        if (dailyToEdit.Activities == null)
        {
            return new ServiceResponse(false, "SER: no daily.");
        }

        if (dailyToEdit.Activities == null)
        {
            dailyToEdit.Activities = new List<Activity>();
        }

        Activity newActivity = new Activity();
        newActivity.OrderNumber = dailyToEdit.Activities.Count + 1;

        dailyToEdit.Activities.Add(newActivity);

        SaveActivityToEdit();

        return new ServiceResponse(true, "SER: added activity to edit.");
    }


    public ServiceResponse<Activity> GetActivityToEdit()
    {
        if (activityToEdit == null)
        {
            return new ServiceResponse<Activity>(false, "SER: there is no selection loaded.", new Activity());
        }
        else
        {
            return new ServiceResponse<Activity>(true, "SER: selection success.", activityToEdit);
        }
    }

    public ServiceResponse SetActivityToEdit(Activity activity)
    {
        if (dailyToEdit.Activities.Contains(activity))
        {
            activityToEdit = activity;
            return new ServiceResponse(true, "SER: selection success.");
        }
        else
        {
            return new ServiceResponse(false, "SER: selection failed.");
        }
    }

    public ServiceResponse DeleteCurrentActivityToEdit()
    {
        if (dailyToEdit.Activities.Contains(activityToEdit))
        {
            dailyToEdit.Activities.Remove(activityToEdit);
            activityToEdit = null;
            return new ServiceResponse(true, "SER: selection success.");
        }
        else
        {
            return new ServiceResponse(false, "SER: selection failed.");
        }
    }
    public ServiceResponse SaveActivityToEdit()
    {
        try
        {
            dailyToEdit.Activities.Sort((act1, act2) => (act1.OrderNumber.CompareTo(act2.OrderNumber)));
            for(int i = 0; i < dailyToEdit.Activities.Count; i++)
            {
                dailyToEdit.Activities[i].OrderNumber = i + 1;
            }


            return new ServiceResponse(true, "SER: save success.");
        }
        catch (Exception e)
        {
            return new ServiceResponse(false, "SER: save failed.");
        }

    }

    internal ServiceResponse SaveTrainingInActivityToEdit(Training training)
    {
        if (activityToEdit != null)
        {
            activityToEdit.Training = training;

            return new ServiceResponse(true, "SER: save success.");
        }
        else
        {
            return new ServiceResponse(false, "SER: save failed.");
        }
    }
}
