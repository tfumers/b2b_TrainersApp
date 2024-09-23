using com.TresToGames.TrainersApp.BO_SuperClasses;
using com.TresToGames.TrainersApp.Utils.AppComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ComponentManager : Manager, 
    ICreateComponentsInManager<ClientMessageComponent>, 
    ICreateClickableComponentsInManager<AcceptedClientComponent>, 
    ICreateClickableComponentsInManager<PendingNewClientComponent>,
    ICreateClickableComponentsInManager<DailyComponent>,
    ICreateClickableComponentsInManager<ActivityComponent>,
    IConfigureInteractiveComponentInManager<CreateRoutineButtonComponent>,
    ICreateClickableComponentWithModelInManager<ClickableAvatarComponent, Avatar>,
    IConfigureComponentInManager<PendingClientsCountComponent>,
    IConfigureInteractiveComponentInManager<TrainerClientRelationInfoComponent>,
    IConfigureInteractiveComponentInManager<CurrentActivityInfoComponent>,
    IConfigureInteractiveComponentInManager<ChangeTrainerAvatarComponent>,
    IConfigureComponentInManager<CurrentTrainingInfoComponent>,
    IConfigureInteractiveComponentInManager<SubmitSelectedAvatarComponent>,
    IConfigureComponentInManager<CompleteClientInfoComponent>,
    ICreateClickableComponentsInManager<EditDailyComponent>,
    ICreateClickableComponentsInManager<EditActivityComponent>,
    IConfigureInteractiveComponentInManager<DeleteRoutineEditComponent>,
    IConfigureInteractiveComponentInManager<EndRoutineEditComponent>,
    IConfigureInteractiveComponentInManager<DeleteDailyActivityEditComponent>,
    IConfigureInteractiveComponentInManager<DeleteActivityEditComponent>,
    IConfigureInteractiveComponentInManager<BackToSelectPendingClient>,
    IConfigureComponentInManager<DailyToEditInfoComponent>,
    IConfigureInteractiveComponentInManager<BackToEditRoutineComponent>,
    IConfigureInteractiveComponentInManager<ActivityEditInfoComponent>,
    IConfigureInteractiveComponentInManager<BackToEditDailyActivity>,
    IConfigureComponentInManager<TrainingSelectorComponent>,
    IConfigureInteractiveComponentInManager<RegisterNewTrainerButtonComponent>,
    IConfigureComponentInManager<CommonTrainingSelector>
{

    CurrentTrainerController currentTrainerController;

    RoutineController routineController;

    TrainingController trainingController;

    ComponentController componentController;

    NotificationManager notificationManager;
    public override void Initialize()
    {
        currentTrainerController = B2BTrainer.Instance.controllerManager.currentTrainerController;
        routineController = B2BTrainer.Instance.controllerManager.routineController;
        trainingController = B2BTrainer.Instance.controllerManager.trainingController;
        componentController = B2BTrainer.Instance.controllerManager.componentController;
        notificationManager = B2BTrainer.Instance.notificationManager;
    }

    public async Task<List<ClientMessageComponent>> CreateListOfComponents(List<ClientMessageComponent> listOfAppComponents, Transform scrollViewTransform)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<ClientMessageComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach(ClientMessageComponent cmc in listOfAppComponents)
            {
                Destroy(cmc.gameObject);
            }
        }

        Task<List<ClientMessageComponent>> createClientMessageComponents = componentController.CreateClientMessageComponents(scrollViewTransform);

        await createClientMessageComponents;

        return createClientMessageComponents.Result;
    }

    public async Task<List<AcceptedClientComponent>> CreateListOfComponents(List<AcceptedClientComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<AcceptedClientComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (AcceptedClientComponent cmc in listOfAppComponents)
            {
                Destroy(cmc.gameObject);
            }
        }

        Task<List<AcceptedClientComponent>> createCurrentClientsComponents = componentController.CreateCurrentClientsComponents(scrollViewTransform, onClickedComponentEvent);

        await createCurrentClientsComponents;

        return createCurrentClientsComponents.Result;
    }

    public async Task<List<PendingNewClientComponent>> CreateListOfComponents(List<PendingNewClientComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<PendingNewClientComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (PendingNewClientComponent pncc in listOfAppComponents)
            {
                Destroy(pncc.gameObject);
            }
        }

        Task<List<PendingNewClientComponent>> createPendingNewClientsComponents = componentController.CreatePendingClientsComponents(scrollViewTransform, onClickedComponentEvent);

        await createPendingNewClientsComponents;

        return createPendingNewClientsComponents.Result;
    }

    public async Task<List<DailyComponent>> CreateListOfComponents(List<DailyComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<DailyComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (DailyComponent dc in listOfAppComponents)
            {
                Destroy(dc.gameObject);
            }
        }

        Task<List<DailyComponent>> createDailyComponents = componentController.CreateDailyComponents(scrollViewTransform, onClickedComponentEvent);

        await createDailyComponents;

        return createDailyComponents.Result;

    }

    public async Task<List<ActivityComponent>> CreateListOfComponents(List<ActivityComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<ActivityComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (ActivityComponent component in listOfAppComponents)
            {
                Destroy(component.gameObject);
            }
        }

        Task<List<ActivityComponent>> createActivityComponents = componentController.CreateActivityComponents(scrollViewTransform, onClickedComponentEvent);

        await createActivityComponents;

        return createActivityComponents.Result;
    }

    public async Task<PendingClientsCountComponent> ConfigureComponent(PendingClientsCountComponent component)
    {
        Task<PendingClientsCountComponent> configurePendingClientsCountComponent = componentController.ConfigurePendingClientsCountComponent(component);

        await configurePendingClientsCountComponent;

        PendingClientsCountComponent obtainedComponent = configurePendingClientsCountComponent.Result;

        if (obtainedComponent.Model >= 0)
        {
            obtainedComponent.gameObject.SetActive(true);
        }
        else
        {
            obtainedComponent.gameObject.SetActive(false);
        }

        return obtainedComponent;
    }

    //Estos objetos ya están creados. No es necesario crearlos desde el component controller, pero sí podemos acceder a los métodos del client controller

    public Task<TrainerClientRelationInfoComponent> ConfigureComponent(TrainerClientRelationInfoComponent component, UnityEvent onMoreInfoButtonClicked)
    {
        component.RemoveAllListeners();

        TrainerClientRelation tcr = currentTrainerController.GetSelectedTrainerClientRelation();

        UnityEvent<TrainerClientRelation> unityEvent = new UnityEvent<TrainerClientRelation>();

        unityEvent.AddListener((tcr) => currentTrainerController.SetSelectedClient(tcr.Client));
        unityEvent.AddListener((tcr) => onMoreInfoButtonClicked.Invoke());

        component.LoadComponent(tcr, unityEvent);

        return Task.FromResult(component);
    }

    public Task<CurrentActivityInfoComponent> ConfigureComponent(CurrentActivityInfoComponent component, UnityEvent onClickedEvent)
    {
        Activity currentActivity = currentTrainerController.GetSelectedActivity();

        UnityEvent<Activity> unityEvent = new UnityEvent<Activity>();

        unityEvent.AddListener((act) => currentTrainerController.SetSelectedTraining(act.Training));
        unityEvent.AddListener((act) => onClickedEvent.Invoke());

        component.LoadComponent(currentActivity, unityEvent);

        return Task.FromResult(component);
    }

    public Task<CurrentTrainingInfoComponent> ConfigureComponent(CurrentTrainingInfoComponent component)
    {
        Training currentTraining = currentTrainerController.GetSelectedTraining();

        component.LoadComponent(currentTraining);

        return Task.FromResult(component);
    }

    public async Task<ChangeTrainerAvatarComponent> ConfigureComponent(ChangeTrainerAvatarComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        Task<Trainer> getTrainerInfo = currentTrainerController.GetCurrentTrainerInfo();

        await getTrainerInfo;

        Trainer trainer = getTrainerInfo.Result;

        UnityEvent<Avatar> unityEvent = new UnityEvent<Avatar>();

        unityEvent.AddListener((tcr) => onClickedEvent.Invoke());

        component.LoadComponent(trainer.AvatarImage, unityEvent);

        return component;
    }

    public async Task<List<ClickableAvatarComponent>> CreateListOfComponents(List<ClickableAvatarComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent<Avatar> onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<ClickableAvatarComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (ClickableAvatarComponent cac in listOfAppComponents)
            {
                Destroy(cac.gameObject);
            }
        }

        Task<List<ClickableAvatarComponent>> createClickabelAvatarComponents = componentController.CreateClickableAvatarComponents(scrollViewTransform, onClickedComponentEvent);

        await createClickabelAvatarComponents;

        return createClickabelAvatarComponents.Result;
    }

    public async Task<SubmitSelectedAvatarComponent> ConfigureComponent(SubmitSelectedAvatarComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent<Avatar> unityEvent = new UnityEvent<Avatar>();

        unityEvent.AddListener(async (av) => {
            Dictionary<string, string> avatarToUpdate = new Dictionary<string, string>();
            avatarToUpdate.Add("id", currentTrainerController.GetSelectedAvatarToUpdate().Id.ToString());
            await currentTrainerController.UpdateTrainerAvatar(avatarToUpdate);
            onClickedEvent.Invoke();
        });

        component.LoadComponent(currentTrainerController.GetSelectedAvatarToUpdate(), unityEvent);

        return component;
    }

    public Task<CompleteClientInfoComponent> ConfigureComponent(CompleteClientInfoComponent component)
    {
        TrainerClientRelation currentTrainerClientRelation = currentTrainerController.GetSelectedTrainerClientRelation();

        component.LoadComponent(currentTrainerClientRelation);

        return Task.FromResult(component);
    }

    public Task<CreateRoutineButtonComponent> ConfigureComponent(CreateRoutineButtonComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(() => currentTrainerController.SetRoutineToEdit());
        unityEvent.AddListener(() => onClickedEvent.Invoke());

        component.LoadComponent(unityEvent);

        return Task.FromResult(component);
    }

    //Edit routine components

    public async Task<List<EditDailyComponent>> CreateListOfComponents(List<EditDailyComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<EditDailyComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (EditDailyComponent edc in listOfAppComponents)
            {
                Destroy(edc.gameObject);
            }
        }

        Task<List<EditDailyComponent>> createEditDailyComponents = componentController.CreateEditDailyComponents(scrollViewTransform, onClickedComponentEvent);

        await createEditDailyComponents;

        return createEditDailyComponents.Result;
    }

    public Task<AddDailyComponent> ConfigureComponent(AddDailyComponent component, Transform scrollViewTransform, UnityEvent onClickedEvent)
    {
        string name;

        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(() => currentTrainerController.AddDailyActivityToEdit());
        unityEvent.AddListener(() => onClickedEvent.Invoke());

        AddDailyComponent adcnew = AddDailyComponent.Instantiate(component, scrollViewTransform);

        name = component.name;
        Destroy(component.gameObject);
        adcnew.name = name;

        adcnew.LoadComponent(unityEvent);



        return Task.FromResult(adcnew);
    }

    public Task<DeleteRoutineEditComponent> ConfigureComponent(DeleteRoutineEditComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();


        UnityEvent deleteRoutineProcess = new UnityEvent();

        deleteRoutineProcess.AddListener(() => currentTrainerController.DeleteRoutineToEdit());
        deleteRoutineProcess.AddListener(() => onClickedEvent.Invoke());

        UnityEvent notificationEvent = new UnityEvent();

        notificationEvent.AddListener(() => notificationManager.DeleteConfirmation("Rutina", deleteRoutineProcess));

        component.LoadComponent(notificationEvent);

        return Task.FromResult(component);
    }

    public Task<BackToSelectPendingClient> ConfigureComponent(BackToSelectPendingClient component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent deleteRoutineProcess = new UnityEvent();

        deleteRoutineProcess.AddListener(() => currentTrainerController.DeleteRoutineToEdit());
        deleteRoutineProcess.AddListener(() => onClickedEvent.Invoke());

        UnityEvent notificationEvent = new UnityEvent();

        notificationEvent.AddListener(() => notificationManager.DeleteConfirmation("Rutina", deleteRoutineProcess));

        component.LoadComponent(notificationEvent);

        return Task.FromResult(component);
    }

    //Edit Daily view

    public async Task<List<EditActivityComponent>> CreateListOfComponents(List<EditActivityComponent> listOfAppComponents, Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<EditActivityComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (EditActivityComponent eac in listOfAppComponents)
            {
                Destroy(eac.gameObject);
            }
        }

        Task<List<EditActivityComponent>> createEditDailyComponents = componentController.CreateEditActivityComponents(scrollViewTransform, onClickedComponentEvent);

        await createEditDailyComponents;

        return createEditDailyComponents.Result;
    }

    public Task<AddActivityComponent> ConfigureComponent(AddActivityComponent component, Transform scrollViewTransform, UnityEvent onClickedEvent)
    {
        string name;

        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(() => currentTrainerController.AddActivityToEdit());
        unityEvent.AddListener(() => onClickedEvent.Invoke());

        AddActivityComponent newAAC = AddDailyComponent.Instantiate(component, scrollViewTransform);

        name = component.name;
        Destroy(component.gameObject);
        newAAC.name = name;

        newAAC.LoadComponent(unityEvent);

        return Task.FromResult(newAAC);
    }

    public Task<DeleteDailyActivityEditComponent> ConfigureComponent(DeleteDailyActivityEditComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent deleteDailyProcess = new UnityEvent();

        deleteDailyProcess.AddListener(() => currentTrainerController.DeleteCurrentDailyToEdit());
        deleteDailyProcess.AddListener(() => onClickedEvent.Invoke());

        UnityEvent notificationEvent = new UnityEvent();
        
        notificationEvent.AddListener(() => notificationManager.DeleteConfirmation("Actividad Diaria", deleteDailyProcess));

        component.LoadComponent(notificationEvent);

        return Task.FromResult(component);
    }

    public Task<BackToEditRoutineComponent> ConfigureComponent(BackToEditRoutineComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(() => currentTrainerController.SaveDailyToEdit());
        
        unityEvent.AddListener(() => onClickedEvent.Invoke());

        component.LoadComponent(unityEvent);

        return Task.FromResult(component);
    }

    public Task<DailyToEditInfoComponent> ConfigureComponent(DailyToEditInfoComponent component)
    {
    

        component.LoadComponent(currentTrainerController.GetDailyToEdit());

        return Task.FromResult(component);
    }

    //Edit Activity
    public Task<ActivityEditInfoComponent> ConfigureComponent(ActivityEditInfoComponent component, UnityEvent OnSearchTrainingToAdd)
    {
        UnityEvent<List<Training>> OnSearchTrainingsButtonPressed = new UnityEvent<List<Training>>();

        component.RemoveAllListeners();

        UnityEvent<Activity> unityEvent = new UnityEvent<Activity>();

        unityEvent.AddListener((act) => OnSearchTrainingToAdd.Invoke());

        component.LoadComponent(currentTrainerController.GetActivityToEdit(), unityEvent);

        return Task.FromResult(component);
    }

    public Task<DeleteActivityEditComponent> ConfigureComponent(DeleteActivityEditComponent component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent deleteActivityProcess = new UnityEvent();

        deleteActivityProcess.AddListener(() => currentTrainerController.DeleteCurrentActivityToEdit());
        deleteActivityProcess.AddListener(() => onClickedEvent.Invoke());

        UnityEvent notificationEvent = new UnityEvent();

        notificationEvent.AddListener(() => notificationManager.DeleteConfirmation("Actividad", deleteActivityProcess));

        component.LoadComponent(notificationEvent);

        return Task.FromResult(component);
    }

    public Task<BackToEditDailyActivity> ConfigureComponent(BackToEditDailyActivity component, UnityEvent onClickedEvent)
    {
        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        //unityEvent.AddListener(() => );//Guardar el progreso;
        unityEvent.AddListener(() => currentTrainerController.SaveActivityToEdit());
        unityEvent.AddListener(() => onClickedEvent.Invoke());

        component.LoadComponent(unityEvent);

        return Task.FromResult(component);
    }

    public Task<EndRoutineEditComponent> ConfigureComponent(EndRoutineEditComponent component, UnityEvent OnSuccessEvents)
    {
        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(async () => {
            Task<bool> createRoutine = routineController.CreateNewRoutine(currentTrainerController.GetTrainerClientRelationToEndEditRoutine());
            await createRoutine;
            if (createRoutine.Result)
            {
                notificationManager.Success(OnSuccessEvents);
            }
        });
        //unityEvent.AddListener(() => );//Guardar el progreso;
        //unityEvent.AddListener(() => onClickedEvent.Invoke());

        component.LoadComponent(unityEvent);

        return Task.FromResult(component);
    }

    public Task<TrainingSelectorComponent> ConfigureComponent(TrainingSelectorComponent component)
    {
        component.RemoveAllListeners();

        UnityEvent unityEvent = new UnityEvent();

        unityEvent.AddListener(async () =>
        {
            component.SetButtonsInteractable(false);

            component.OnTrainingSelected.AddListener((training) => currentTrainerController.SaveSelectedTrainingInActivityToEdit(training));

            component.OnTrainingSelected.AddListener((training) => 
            {
                foreach (TrainingToSelectInActivityEditComponent tr in component.Trainings)
                {
                    tr.CheckSelected(training);
                } 
            });

            Task<List<Training>> getTrainingProcess = trainingController.GetTrainingsBySearchParams(component.GetTextToSearch(), component.GetParamToSearch());

            await getTrainingProcess;

            Task<List<TrainingToSelectInActivityEditComponent>> configureSelectorComponents = componentController.CreateTrainingToSelectInActivityEditComponent(getTrainingProcess.Result, component.scrollViewTransform, component.OnTrainingSelected);

            await configureSelectorComponents;

            component.ClearAndAddTrainings(configureSelectorComponents.Result);

            component.SetButtonsInteractable(true);
        });

        UnityEvent<Training> newTrainingEvent = new UnityEvent<Training>();
        newTrainingEvent.AddListener((tr) => unityEvent.Invoke());

        component.LoadComponent(new Training(), newTrainingEvent);

        component.ClearAndAddTrainings();

        return Task.FromResult(component);
    }

    public async Task<AddTrainingInfoComponent> ConfigureComponent(AddTrainingInfoComponent component, UnityEvent onCancelTrainingCreationButtonPressed, UnityEvent receivedOnCreatedTrainingButtonPressed)
    {
        Task<Training> getTrainingToEditTask = currentTrainerController.GetTrainingToEdit();

        await getTrainingToEditTask;

        Task<List<TrainingImage>> getTrainingImages = componentController.GetAllTrainingImages();

        await getTrainingImages;

        Task<List<TrainingVideo>> getAllTrainingVideos = componentController.GetAllTrainingVideos();

        await getAllTrainingVideos;

        UnityEvent onCancelPressed = new UnityEvent();

        //onCancelPressed.AddListener(() => currentTrainerController.CancelTrainingToEdit()) listener para eliminar el currentTrainingToEdit
        onCancelPressed.AddListener(() => onCancelTrainingCreationButtonPressed.Invoke());

        UnityEvent onCancelTrainingCreationOrEdit = new UnityEvent();
        onCancelTrainingCreationOrEdit.AddListener(() =>
        {
            notificationManager.DeleteConfirmation("Entrenamiento", onCancelTrainingCreationButtonPressed);
        });


        UnityEvent<Training> OnCreateTrainingButtonPressed = new UnityEvent<Training>();
        OnCreateTrainingButtonPressed.AddListener(async (tr) =>
        {
            Task<bool> taskTrainingCreation = trainingController.CreateNewTraining(tr);

            await taskTrainingCreation;


            if (taskTrainingCreation.Result)
            {
                notificationManager.Success(receivedOnCreatedTrainingButtonPressed);
                //receivedOnCreatedTrainingButtonPressed.Invoke();
            }
            else
            {
                notificationManager.Failed("", new UnityEvent());
            }
        });

        UnityEvent onCannotCreateTrainingButtonPressed = new UnityEvent();

        onCannotCreateTrainingButtonPressed.AddListener(() =>
        {
            notificationManager.GenericError("No se pudo crear el entrenamiento. Faltan campos de completar");
        });

        component.LoadComponent(new AddTrainingInfoComponentConfigurations(getTrainingToEditTask.Result, getTrainingImages.Result, getAllTrainingVideos.Result, OnCreateTrainingButtonPressed, onCancelTrainingCreationOrEdit, onCannotCreateTrainingButtonPressed));

        return component;
    }

    public async Task<RegisterNewTrainerButtonComponent> ConfigureComponent(RegisterNewTrainerButtonComponent component, UnityEvent OnRegisterSuccess)
    {
        component.RemoveAllListeners();

        UnityEvent<Dictionary<string, string>> onRegisterButtonPressed = new UnityEvent<Dictionary<string, string>>();

        onRegisterButtonPressed.AddListener(async (dictionary) =>
        {
            component.RegisterButton.interactable = false;

            Task<bool> registerTask = currentTrainerController.RegisterTrainer(dictionary);// authController.RegisterTask();
                                                                                                            //esperamos el resultado del task
            await registerTask;

            Debug.Log("Estado del registro " + registerTask.Result);

            if (registerTask.Result)
            {
                notificationManager.Success(OnRegisterSuccess);
                component.RegisterButton.interactable = true;
                //            OnRegisterSuccess.Invoke();
            }
            else
            {
                notificationManager.RegisterFailed("Registro falló");
                component.RegisterButton.interactable = true;
            }
            
        });

        component.LoadComponent(new Dictionary<string, string>(), onRegisterButtonPressed);

        return component;
    }

    public Task<CommonTrainingSelector> ConfigureComponent(CommonTrainingSelector component)
    {
        UnityEvent<string, string> unityEvent = new UnityEvent<string, string>();

        component.LoadComponent(unityEvent);

        return Task.FromResult(component);
    }

    internal async Task<List<CommonTrainingViewComponent>> CreateListOfComponents(List<CommonTrainingViewComponent> listOfAppComponents, Transform scrollViewContentTransform, string toSearch, string paramText, UnityEvent onTrainingButtonPressed)
    {
        if (listOfAppComponents == null)
        {
            listOfAppComponents = new List<CommonTrainingViewComponent>();
        }

        if (listOfAppComponents.Count > 0)
        {
            foreach (CommonTrainingViewComponent ctvc in listOfAppComponents)
            {
                ctvc.RemoveAllListeners();
                Destroy(ctvc.gameObject);
            }

            listOfAppComponents.Clear();
        }

        Task<List<CommonTrainingViewComponent>> createCommonTrainingComponents = componentController.CreateCommonTrainingViewComponents(scrollViewContentTransform, toSearch, paramText, onTrainingButtonPressed);

        await createCommonTrainingComponents;

        listOfAppComponents = createCommonTrainingComponents.Result;

        return listOfAppComponents;
    }
}
