using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryManager : Manager
{
    //WEB REPOSITORIES
    public TrainerRepository trainerRepository;

    public ClientRepository clientRepository;

    public TrainingRepository trainingRepository;

    public TrainerClientRelationRepository trainerClientRelationRepository;

    public RoutineRepository routineRepository;

    //LOCAL REPOSITORIES
    public AvatarRepository avatarRepository;

    public TrainingImageRepository trainingImageRepository;

    public TrainingVideoRepository trainingVideoRepository;

    List<Repository> repositories;
    public override void Initialize()
    {
        repositories = new List<Repository>();

        //currentTrainerRepository
        if (trainingRepository == null)
        {

        }
        repositories.Add(trainingRepository);
        //clientRepository
        if (avatarRepository == null)
        {

        }
        repositories.Add(avatarRepository);
        //trainerRepository
        if (trainerRepository == null)
        {

        }
        repositories.Add(trainerRepository);
        //clientRepository
        if (clientRepository == null)
        {

        }
        repositories.Add(clientRepository);
        //routineRepository
        if (routineRepository == null)
        {

        }
        repositories.Add(routineRepository);
        //TrainingRepository
        if (trainingRepository == null)
        {

        }
        repositories.Add(trainingRepository);

        if (trainingImageRepository == null)
        {

        }
        repositories.Add(trainingImageRepository);
        
        if (trainerClientRelationRepository == null)
        {

        }
        repositories.Add(trainerClientRelationRepository);

        if (trainingVideoRepository == null)
        {

        }
        repositories.Add(trainingVideoRepository);

        foreach(Repository repo in repositories)
        {
            repo.Initialize();
        }
    }
}
