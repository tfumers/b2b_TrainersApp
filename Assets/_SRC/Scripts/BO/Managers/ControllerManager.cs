using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Manager
{
    public RoutineController routineController;

    public CurrentTrainerController currentTrainerController;

    public TrainerController trainerController;

    public ClientController clientController;

    public TrainingController trainingController;

    public ComponentController componentController;

    public override void Initialize()
    {
        List<Controller> controllers = new List<Controller>();

        if (routineController == null)
        {
            routineController = this.gameObject.GetComponentInChildren<RoutineController>(true);
        }
        controllers.Add(routineController);

        if (currentTrainerController == null)
        {
            currentTrainerController = this.gameObject.GetComponentInChildren<CurrentTrainerController>(true);
        }
        controllers.Add(currentTrainerController);

        if (trainerController == null)
        {
            trainerController = this.gameObject.GetComponentInChildren<TrainerController>(true);
        }
        controllers.Add(trainerController);

        if (routineController == null)
        {
            routineController = this.gameObject.GetComponentInChildren<RoutineController>(true);
        }
        controllers.Add(routineController);

        if (clientController == null)
        {
            clientController = this.gameObject.GetComponentInChildren<ClientController>(true);
        }
        controllers.Add(clientController);

        if (trainingController == null)
        {
            trainingController = this.gameObject.GetComponentInChildren<TrainingController>(true);
        }
        controllers.Add(trainingController);

        if (componentController == null)
        {
            componentController = this.gameObject.GetComponentInChildren<ComponentController>(true);
        }
        controllers.Add(componentController);
        /* 
        if(authController == null)
        {
            authController = this.gameObject.GetComponentInChildren<AuthController>(true);
        }
        controllers.Add(authController);

        if (updateClientInfoController == null)
        {
            updateClientInfoController = this.gameObject.GetComponentInChildren<UpdateClientInfoController>(true);
        }
        controllers.Add(updateClientInfoController);

        if (routineController == null)
        {
            routineController = this.gameObject.GetComponentInChildren<RoutineController>(true);
        }
        controllers.Add(routineController);

        if (trainerController == null)
        {
            trainerController = this.gameObject.GetComponentInChildren<TrainerController>(true);
        }
        controllers.Add(trainerController);

        if (clientInfoController == null)
        {
            clientInfoController = this.gameObject.GetComponentInChildren<ClientInfoController>(true);
        }
        controllers.Add(clientInfoController);

        if (componentController == null)
        {
            componentController = this.gameObject.GetComponentInChildren<ComponentController>(true);
        }
        controllers.Add(componentController);*/

        foreach (Controller con in controllers)
        {
            con.Initialize();
        }
    }
}
