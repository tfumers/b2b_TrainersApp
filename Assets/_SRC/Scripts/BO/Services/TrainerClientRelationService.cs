using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerClientRelationService : Service
{
    TrainerClientRelationRepository trainerClientRelationRepository;

    public override void Initialize()
    {
        trainerClientRelationRepository = B2BTrainer.Instance.repositoryManager.trainerClientRelationRepository;
    }

    internal async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerClientRelationAcceptedClients()
    {
        Task<RepositoryResponse<List<TrainerClientRelation>>> getTrainerClientRelations = trainerClientRelationRepository.FindByStatus(Constant.TRAINER_CLIENT_RELATION_STATUS_ACCEPTED);
        await getTrainerClientRelations;

        List<TrainerClientRelation> trainerClientRelations = getTrainerClientRelations.Result.Returned;

        string message = getTrainerClientRelations.Result.Message + "SER: Obtained " + trainerClientRelations.Count + " entities.";

        return new ServiceResponse<List<TrainerClientRelation>>(true, message, trainerClientRelations);
    }

    internal async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerClientRelationPendingClients()
    {
        Task<RepositoryResponse<List<TrainerClientRelation>>> getTrainerClientRelations = trainerClientRelationRepository.FindByStatus(Constant.TRAINER_CLIENT_RELATION_STATUS_PENDING);
        await getTrainerClientRelations;

        List<TrainerClientRelation> trainerClientRelations = getTrainerClientRelations.Result.Returned;

        string message = getTrainerClientRelations.Result.Message + "SER: Obtained " + trainerClientRelations.Count + " entities.";

        return new ServiceResponse<List<TrainerClientRelation>>(true, message, trainerClientRelations);
    }

    internal async Task<ServiceResponse<List<TrainerClientRelation>>> GetTrainerClientRelationCancelledClients()
    {
        Task<RepositoryResponse<List<TrainerClientRelation>>> getTrainerClientRelations = trainerClientRelationRepository.FindByStatus(Constant.TRAINER_CLIENT_RELATION_STATUS_CANCELLED);
        await getTrainerClientRelations;

        List<TrainerClientRelation> trainerClientRelations = getTrainerClientRelations.Result.Returned;

        string message = getTrainerClientRelations.Result.Message + "SER: Obtained " + trainerClientRelations.Count + " entities.";

        return new ServiceResponse<List<TrainerClientRelation>>(true, message, trainerClientRelations);
    }
}
