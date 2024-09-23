using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerController : Controller
{
    NotificationManager notificationManager;

    TrainerService trainerService;

    public override Task<bool> Initialize()
    {
        return Task.FromResult(true);
    }

    public async Task<List<Trainer>> GetAllTrainers()
    {
        Task<ServiceResponse<List<Trainer>>> getAllTrainers = trainerService.GetAllTrainers();

        await getAllTrainers;

        if (getAllTrainers.Result.Completed == false)
        {
            notificationManager.GenericError(getAllTrainers.Result.Message);
        }

        return getAllTrainers.Result.Returned;
    }

    public async Task<Trainer> GetTrainerById(long id)
    {
        Task<ServiceResponse<Trainer>> getTrainer = trainerService.GetTrainerById(id);

        await getTrainer;

        if (getTrainer.Result.Completed == false)
        {
            notificationManager.GenericError(getTrainer.Result.Message);
        }

        return getTrainer.Result.Returned;
    }

    //Código Viejo

    WebConnectionManager webConnectionManager;

    private Trainer selectedTrainer = null;

    private bool loadedTrainers = false;

    public bool LoadedTrainers { get => loadedTrainers; set => loadedTrainers = value; }

    public void Start()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;
        webConnectionManager = B2BTrainer.Instance.webConnectionManager;
    }

    public async Task<List<Trainer>> GetTrainers()
    {
        List<Trainer> trainers = new List<Trainer>();
        //
        Task<ResponseDTO> getTrainersResponse = webConnectionManager.GetTrainers();

        await getTrainersResponse;

        try{
            trainers = DataAdapter.ResponseToTrainerList(getTrainersResponse.Result);
        }
        catch(Exception e)
        {
            notificationManager.GenericError(e.Message);
        }

        return trainers;
    }

    public void SelectTrainer(Trainer trainer)
    {
        if (trainer != null)
        {
            Debug.Log("Trainer elegido, con id: " + trainer.Id );
            selectedTrainer = trainer;
        }
    }

    public Trainer GetSelectedTrainer()
    {
        if (selectedTrainer != null)
        {
            return selectedTrainer;
        }
        else
        {
            notificationManager.GenericError("TRAINER No cargado");
            return null;
        }
    }

    public async Task<bool> CreateNewTrainerClientRelation()
    {
        EditRoutineOutDTO newRelationOutDto = new EditRoutineOutDTO(new Dictionary<string, string>());

        Task<bool> postNewClientRelation = webConnectionManager.PostNewClientTrainerRelation(newRelationOutDto);

        await postNewClientRelation;

        if (postNewClientRelation.Result)
        {
            await UpdateClientInfo();
        }

        return postNewClientRelation.Result;
    }

    private async Task UpdateClientInfo()
    {
        Task<Client> updateClientInfo = null; //clientInfoController.GetCurrentClientInfo();
        await updateClientInfo;
    }
}
