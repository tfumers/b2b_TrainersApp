using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientService : Service
{
    TrainerClientRelationService trainerClientRelationService;

    RoutineService routineService;

    LocalFilesService localFilesService;

    ClientRepository clientRepository;

    public override void Initialize()
    {
        //Services
        trainerClientRelationService = B2BTrainer.Instance.serviceManager.trainerClientRelationService;
        routineService = B2BTrainer.Instance.serviceManager.routineService;
        localFilesService = B2BTrainer.Instance.serviceManager.localFilesService;

        //Repositories
        clientRepository = B2BTrainer.Instance.repositoryManager.clientRepository;
    }

    public async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerAcceptedClients()
    {
        string message = "";

        List<TrainerClientRelation> trainerClientRelations;

        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerClientRelationAccepted = trainerClientRelationService.GetTrainerClientRelationAcceptedClients();

        await getTrainerClientRelationAccepted;

        trainerClientRelations = getTrainerClientRelationAccepted.Result.Returned;

        message += getTrainerClientRelationAccepted.Result.Message;

        foreach (TrainerClientRelation tcr in trainerClientRelations)
        {
            Task<ServiceResponse<Client>> getClient = GetClientById(tcr.Client.Id);

            await getClient;

            if (getClient.Result.Completed)
            {
                tcr.Client = getClient.Result.Returned;
            }
            else
            {
                message += getClient.Result.Message;
                break;
            }

            Task<ServiceResponse<Routine>> getRoutine = routineService.GetRoutineById(tcr.Routine.Id);

            await getRoutine;

            if (getRoutine.Result.Completed)
            {
                tcr.Routine = getRoutine.Result.Returned;
            }
            else
            {
                message += getRoutine.Result.Message;
                break;
            }
            
        }

        return new ServiceResponse<List<TrainerClientRelation>>(getTrainerClientRelationAccepted.Result.Completed, message, trainerClientRelations);
    }

    public async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerCancelledClients()
    {
        string message = "";

        List<TrainerClientRelation> trainerClientRelations;

        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerClientRelationCancelled = trainerClientRelationService.GetTrainerClientRelationCancelledClients();

        await getTrainerClientRelationCancelled;

        trainerClientRelations = getTrainerClientRelationCancelled.Result.Returned;

        message += getTrainerClientRelationCancelled.Result.Message;

        foreach (TrainerClientRelation tcr in trainerClientRelations)
        {
            Task<ServiceResponse<Client>> getClient = GetClientById(tcr.Client.Id);

            await getClient;

            if (getClient.Result.Completed)
            {
                tcr.Client = getClient.Result.Returned;
            }
            else
            {
                message += getClient.Result.Message;
                break;
            }

        }

        return new ServiceResponse<List<TrainerClientRelation>>(getTrainerClientRelationCancelled.Result.Completed, message, trainerClientRelations);
    }

    public async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerPendingClients()
    {
        string message = "";

        List<TrainerClientRelation> trainerClientRelations;

        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerClientRelationPending = trainerClientRelationService.GetTrainerClientRelationPendingClients();

        await getTrainerClientRelationPending;

        trainerClientRelations = getTrainerClientRelationPending.Result.Returned;

        message += getTrainerClientRelationPending.Result.Message;

        foreach (TrainerClientRelation tcr in trainerClientRelations)
        {
            Task<ServiceResponse<Client>> getClient = GetClientById(tcr.Client.Id);

            await getClient;

            if (getClient.Result.Completed)
            {
                tcr.Client = getClient.Result.Returned;
            }
            else
            {
                message += getClient.Result.Message;
                break;
            }

        }

        return new ServiceResponse<List<TrainerClientRelation>>(getTrainerClientRelationPending.Result.Completed, message, trainerClientRelations);
    }

    public async Task<ServiceResponse<int>> GetTrainerPendingClientsCount()
    {
        string message = "";

        List<TrainerClientRelation> trainerClientRelations;

        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerClientRelationPending = trainerClientRelationService.GetTrainerClientRelationPendingClients();

        await getTrainerClientRelationPending;

        trainerClientRelations = getTrainerClientRelationPending.Result.Returned;

        message += getTrainerClientRelationPending.Result.Message;

        return new ServiceResponse<int>(getTrainerClientRelationPending.Result.Completed, message, trainerClientRelations.Count);
    }

    public async Task<ServiceResponse<Client>> GetClientById(long id)
    {
        bool completed;
        string message = "";
        Client returnedClient = new Client();

        Task<RepositoryResponse<Client>> getClientFromRepository = clientRepository.FindById(id);

        await getClientFromRepository;

        if (getClientFromRepository.Result.Returned != null)
        {
            returnedClient = getClientFromRepository.Result.Returned;

            Task<ServiceResponse<Avatar>> getAvatar = localFilesService.GetAvatarById(returnedClient.Avatar);

            await getAvatar;

            returnedClient.AvatarImage = getAvatar.Result.Returned;

            completed = true;
        }
        else
        {
            completed = false;
        }


        return new ServiceResponse<Client>(completed, message, returnedClient);
    }

    public async Task<ServiceResponse<List<Client>>> GetTrainerAcceptedClientsInfo()
    {
        string message = "";

        List<Client> clients = new List<Client>();

        List<TrainerClientRelation> trainerClientRelations;

        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerClientRelationAccepted = trainerClientRelationService.GetTrainerClientRelationAcceptedClients();

        await getTrainerClientRelationAccepted;

        trainerClientRelations = getTrainerClientRelationAccepted.Result.Returned;

        message += getTrainerClientRelationAccepted.Result.Message;

        foreach (TrainerClientRelation tcr in trainerClientRelations)
        {
            Task<ServiceResponse<Client>> getClient = GetClientById(tcr.Client.Id);

            await getClient;

            if (getClient.Result.Completed)
            {
                tcr.Client = getClient.Result.Returned;
                clients.Add(tcr.Client);
            }
            else
            {
                message += getClient.Result.Message;
                break;
            }

        }

        return new ServiceResponse<List<Client>>(getTrainerClientRelationAccepted.Result.Completed, message, clients);
    }
}
