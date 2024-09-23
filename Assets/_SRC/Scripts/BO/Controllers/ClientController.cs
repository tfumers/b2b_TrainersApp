using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientController : Controller
{
    NotificationManager notificationManager;

    ClientService clientService;


    public override Task<bool> Initialize()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;

        clientService = B2BTrainer.Instance.serviceManager.clientService;

        return Task.FromResult(true);
    }

    public async Task<List<TrainerClientRelation>> GetTrainerAcceptedClients() //STATUS ACCEPTED
    {
        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerAcceptedClients = clientService.GetTrainerAcceptedClients();

        await getTrainerAcceptedClients;

        if (getTrainerAcceptedClients.Result.Completed == false)
        {
            notificationManager.GenericError(getTrainerAcceptedClients.Result.Message);
        }

        return getTrainerAcceptedClients.Result.Returned;
    }

    public async Task<List<Client>> GetTrainerAcceptedClientsInfo() //STATUS ACCEPTED
    {
        Task<ServiceResponse<List<Client>>> getTrainerClients = clientService.GetTrainerAcceptedClientsInfo();

        await getTrainerClients;

        if (getTrainerClients.Result.Completed == false)
        {
            notificationManager.GenericError(getTrainerClients.Result.Message);
        }

        return getTrainerClients.Result.Returned;
    }

    public async Task<List<TrainerClientRelation>> GetTrainerPendingClients()
    {
        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerPendingClients = clientService.GetTrainerPendingClients();

        await getTrainerPendingClients;

        if (getTrainerPendingClients.Result.Completed == false)
        {
            notificationManager.GenericError(getTrainerPendingClients.Result.Message);
        }

        return getTrainerPendingClients.Result.Returned;
    }

    public async Task<List<TrainerClientRelation>> GetTrainerCancelledClients()
    {
        Task<ServiceResponse<List<TrainerClientRelation>>> getTrainerCancelledClients = clientService.GetTrainerCancelledClients();

        await getTrainerCancelledClients;

        if (getTrainerCancelledClients.Result.Completed == false)
        {
            notificationManager.GenericError(getTrainerCancelledClients.Result.Message);
        }

        return getTrainerCancelledClients.Result.Returned;
    }

    public async Task<int> GetTrainerPendingClientsCount()
    {
        Task<ServiceResponse<int>> getPendingClientsCount = clientService.GetTrainerPendingClientsCount();

        await getPendingClientsCount;

        if (getPendingClientsCount.Result.Completed == false)
        {
            notificationManager.GenericError(getPendingClientsCount.Result.Message);
        }

        return getPendingClientsCount.Result.Returned;
    }

}
