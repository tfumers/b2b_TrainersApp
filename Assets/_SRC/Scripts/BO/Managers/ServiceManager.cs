using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ServiceManager : Manager
{
    public CurrentTrainerService currentTrainerService;

    public TrainerClientRelationService trainerClientRelationService;

    public ClientService clientService;

    public RoutineService routineService;

    public TrainerService trainerService;

    public TrainingService trainingService;

    public LocalFilesService localFilesService;

    List<Service> services;

    public override void Initialize()
    {
        services = new List<Service>();

        if (currentTrainerService == null)
        {
            currentTrainerService = this.GetComponent<CurrentTrainerService>();
        }
        services.Add(currentTrainerService);

        if (trainerClientRelationService == null)
        {
            trainerClientRelationService = this.GetComponent<TrainerClientRelationService>();
        }
        services.Add(trainerClientRelationService);

        if (clientService == null)
        {
            clientService = this.GetComponent<ClientService>();
        }
        services.Add(clientService);

        if (routineService == null)
        {
            routineService = this.GetComponent<RoutineService>();
        }
        services.Add(routineService);

        if (trainerService == null)
        {
            trainerService = this.GetComponent<TrainerService>();
        }
        services.Add(trainerService);

        if (trainingService == null)
        {
            trainingService = this.GetComponent<TrainingService>();
        }
        services.Add(trainingService);

        if (localFilesService == null)
        {
            localFilesService = this.GetComponent<LocalFilesService>();
        }
        services.Add(localFilesService);

        foreach (Service s in services)
        {
            s.Initialize();
        }
    }
}
