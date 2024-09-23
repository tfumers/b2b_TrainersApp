using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// El component controller se encarga, como su nombre indica, de la creación de componentes en las distintas interfaces.
/// </summary>
public class ComponentController : Controller
{
    NotificationManager notificationManager;

    CurrentTrainerController currentTrainerController;

    ClientController clientController;

    TrainerController trainerController;

    RoutineController routineController;

    TrainingController trainingController;

    LocalFilesService localFilesService;


    // Lista de componentes

    public ClientMessageComponent clientMessageComponentPrefab;

    public AcceptedClientComponent acceptedClientComponentPrefab;

    public PendingNewClientComponent pendingNewClientComponentPrefab;

    public DailyComponent dailyComponentPrefab;

    public ActivityComponent activityComponentPrefab;

    public ClickableAvatarComponent clickableAvatarComponentPrefab;

    //Componentes viejos

    public TrainerInfoComponent trainerInfoComponentPrefab;

    public EditDailyComponent editDailyComponentPrefab;

    public EditActivityComponent editActivityComponentPrefab;

    public TrainingToSelectInActivityEditComponent trainingToSelectInActivityEditComponentPrefab;

    public CommonTrainingViewComponent commonTrainingViewComponentPrefab;

    public List<ClickableAvatarComponent> loadedClickableAvatars = new List<ClickableAvatarComponent>();

    public override Task<bool> Initialize()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;

        currentTrainerController = B2BTrainer.Instance.controllerManager.currentTrainerController;

        trainerController = B2BTrainer.Instance.controllerManager.trainerController;
        routineController = B2BTrainer.Instance.controllerManager.routineController;
        clientController = B2BTrainer.Instance.controllerManager.clientController;
        trainingController = B2BTrainer.Instance.controllerManager.trainingController;
        localFilesService = B2BTrainer.Instance.serviceManager.localFilesService;

        return Task.FromResult(true);
    }

    public async Task<List<ClientMessageComponent>> CreateClientMessageComponents(Transform scrollViewTransform)
    {
        List<ClientMessageComponent> clientMessageComponents = new List<ClientMessageComponent>();

        if (clientMessageComponentPrefab != null)
        {

            Task<List<Client>> getClientsInfo = clientController.GetTrainerAcceptedClientsInfo();

            await getClientsInfo;

            foreach (Client cli in getClientsInfo.Result)
            {
                ClientMessageComponent cmc = Instantiate(clientMessageComponentPrefab, scrollViewTransform);
                cmc.LoadComponent(cli);
                clientMessageComponents.Add(cmc);
            }

        }

        return clientMessageComponents;
    }
    public async Task<List<AcceptedClientComponent>> CreateCurrentClientsComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<AcceptedClientComponent> aceptedClientComponents = new List<AcceptedClientComponent>();

        if (acceptedClientComponentPrefab != null)
        {

            Task<List<TrainerClientRelation>> getTrainerClientRelationsInfo = clientController.GetTrainerAcceptedClients();

            await getTrainerClientRelationsInfo;

            foreach (TrainerClientRelation tcr in getTrainerClientRelationsInfo.Result)
            {
                tcr.Routine.OrderDailyActivities();

                UnityEvent<TrainerClientRelation> unityEvent = new UnityEvent<TrainerClientRelation>();

                unityEvent.AddListener((tcr) => currentTrainerController.SetSelectedTrainerClientRelation(tcr));
                unityEvent.AddListener((tcr) => currentTrainerController.SetSelectedRoutine(tcr.Routine));
                unityEvent.AddListener((tcr) => onClickedComponentEvent.Invoke());

                AcceptedClientComponent acc = Instantiate(acceptedClientComponentPrefab, scrollViewTransform);

                acc.LoadComponent(tcr, unityEvent);

                aceptedClientComponents.Add(acc);
            }

        }

        return aceptedClientComponents;
    }

    public async Task<List<PendingNewClientComponent>> CreatePendingClientsComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<PendingNewClientComponent> pendingClientComponents = new List<PendingNewClientComponent>();

        if (pendingNewClientComponentPrefab != null)
        {

            Task<List<TrainerClientRelation>> getTCRPendingClients = clientController.GetTrainerPendingClients();

            await getTCRPendingClients;

            foreach (TrainerClientRelation tcr in getTCRPendingClients.Result)
            {
                UnityEvent<TrainerClientRelation> unityEvent = new UnityEvent<TrainerClientRelation>();

                unityEvent.AddListener((tcr) => currentTrainerController.SetSelectedTrainerClientRelation(tcr));
                unityEvent.AddListener((tcr) => onClickedComponentEvent.Invoke());

                PendingNewClientComponent pncc = Instantiate(pendingNewClientComponentPrefab, scrollViewTransform);

                pncc.LoadComponent(tcr, unityEvent);

                pendingClientComponents.Add(pncc);
            }

        }

        return pendingClientComponents;
    }

    public async Task<PendingClientsCountComponent> ConfigurePendingClientsCountComponent(PendingClientsCountComponent component)
    {
        PendingClientsCountComponent pendingClientsCountComponent = component;

        Task<int> getPendingClientsCount = clientController.GetTrainerPendingClientsCount();

        await getPendingClientsCount;

        pendingClientsCountComponent.LoadComponent(getPendingClientsCount.Result);

        return pendingClientsCountComponent;
    }

    public Task<List<DailyComponent>> CreateDailyComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<DailyComponent> dailyComponents = new List<DailyComponent>();

        if (dailyComponentPrefab != null)
        {

            List<Daily> dailiesFromCurrentRoutine = currentTrainerController.GetSelectedRoutine().DailyActivities;

            foreach (Daily daily in dailiesFromCurrentRoutine)
            {
                UnityEvent<Daily> unityEvent = new UnityEvent<Daily>();

                unityEvent.AddListener((daily) => currentTrainerController.SetSelectedDaily(daily));
                unityEvent.AddListener((daily) => onClickedComponentEvent.Invoke());

                DailyComponent dailyComponent = Instantiate(dailyComponentPrefab, scrollViewTransform);

                dailyComponent.LoadComponent(daily, unityEvent);

                dailyComponents.Add(dailyComponent);
            }

        }

        return Task.FromResult(dailyComponents);
    }

    public Task<List<ActivityComponent>> CreateActivityComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<ActivityComponent> activityComponents = new List<ActivityComponent>();

        if (activityComponentPrefab != null)
        {

            List<Activity> activitiesFromCurrentDaily = currentTrainerController.GetSelectedDaily().Activities;

            foreach (Activity act in activitiesFromCurrentDaily)
            {
                //Acá entra un método para traer, desde el trianing controller, el entrenamiento actualizado
                //act.Training acá se setea el training

                UnityEvent<Activity> unityEvent = new UnityEvent<Activity>();

                unityEvent.AddListener((act) => currentTrainerController.SetSelectedActivity(act));
                unityEvent.AddListener((act) => onClickedComponentEvent.Invoke());

                ActivityComponent activityComponent = Instantiate(activityComponentPrefab, scrollViewTransform);

                activityComponent.LoadComponent(act, unityEvent);

                activityComponents.Add(activityComponent);
            }

        }

        return Task.FromResult(activityComponents);
    }

    //Metodos antiguos app
    /*
    public Avatar GetClientAvatar()
    {
        string avatarString = "0";
        //este método se podría agregar al objeto client, así obtenemos la ref directa al avatar del usuario
        /*if (clientInfoController.CurrentClient != null)
        {
            avatarString = clientInfoController.CurrentClient.Avatar;
        }

        long avatarId = 0;
        try
        {
            avatarId = long.Parse(avatarString);
        }
        catch (Exception e)
        {
            Debug.LogError("Parsing exception " + e.Message);
        }


        List<Avatar> avatars = localFilesService.GetA();
        foreach(Avatar av in avatars)
        {
            if(avatarId == av.Id)
            {
                return av;
            }
        }

        Debug.Log("El avatar del cliente no existe. Por defecto, será el primero en la lista");
        return avatars[0];
    }*/

    public async Task<List<ClickableAvatarComponent>> CreateClickableAvatarComponents(Transform scrollViewTransform, UnityEvent<Avatar> onClickedComponentEvent)
    {
        Task<ServiceResponse<List<Avatar>>> getAvatars = localFilesService.GetAllAvatars();
        List<Avatar> avatars = getAvatars.Result.Returned;

        Task<Trainer> getCurrentTrainerAvatar = currentTrainerController.GetCurrentTrainerInfo();

        await getCurrentTrainerAvatar;

        Avatar currentSelectedAvatar = getCurrentTrainerAvatar.Result.AvatarImage;

        List<ClickableAvatarComponent> clickableAvatarComponents = new List<ClickableAvatarComponent>();

        foreach(Avatar av in avatars)
        {
            ClickableAvatarComponent cac = Instantiate(clickableAvatarComponentPrefab, scrollViewTransform);

            UnityEvent<Avatar> unityEvent = new UnityEvent<Avatar>();

            //luego probar como funciona
            unityEvent.AddListener((av) => currentTrainerController.SetSelectedAvatarToUpdate(av));

            onClickedComponentEvent.AddListener((av) => cac.CheckSelectedAvatar(currentTrainerController.GetSelectedAvatarToUpdate()));

            unityEvent.AddListener((av) => onClickedComponentEvent.Invoke(av));

            cac.LoadComponent(av, unityEvent);

            cac.CheckSelectedAvatar(currentSelectedAvatar);

            clickableAvatarComponents.Add(cac);
        }

        return clickableAvatarComponents;
    }

    public Task<List<ClickableAvatarComponent>> CreateSelectableAvatarComponent(Transform contentTransform, UnityEvent<Avatar> buttonSelect)
    {
        if (loadedClickableAvatars == null)
        {
            loadedClickableAvatars = new List<ClickableAvatarComponent>();
        }

        if (clickableAvatarComponentPrefab != null)
        {

            Task<ServiceResponse<List<Avatar>>> getAvatars = localFilesService.GetAllAvatars();
            List<Avatar> avatars = getAvatars.Result.Returned;

            Debug.Log("lista de avatars: " + avatars.Count);

            foreach (Avatar avatar in avatars)
            {
                ClickableAvatarComponent cac = Instantiate(clickableAvatarComponentPrefab, contentTransform);
                cac.LoadComponent(avatar, buttonSelect);

                loadedClickableAvatars.Add(cac);

                Debug.Log("Avatar id " + avatar.Id);
            }
        }
        else
        {
            notificationManager.GenericError("No hay una instancia de prefab de clickable avatar component");
        }

        return Task.FromResult(loadedClickableAvatars);
    }

    internal Task<List<EditDailyComponent>> CreateEditDailyComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<EditDailyComponent> editDailyComponents = new List<EditDailyComponent>();

        if (editDailyComponentPrefab != null)
        {

            List<Daily> dailiesFromRoutineToEdit = currentTrainerController.GetRoutineToEdit().DailyActivities;

            foreach (Daily daily in dailiesFromRoutineToEdit)
            {
                //Acá entra un método para traer, desde el trianing controller, el entrenamiento actualizado
                //act.Training acá se setea el training

                UnityEvent<Daily> unityEvent = new UnityEvent<Daily>();

                unityEvent.AddListener((daily) => currentTrainerController.SetDailyToEdit(daily));
                unityEvent.AddListener((daily) => onClickedComponentEvent.Invoke());

                EditDailyComponent editDailyComponent = Instantiate(editDailyComponentPrefab, scrollViewTransform);

                editDailyComponent.LoadComponent(daily, unityEvent);

                editDailyComponents.Add(editDailyComponent);
            }

        }

        return Task.FromResult(editDailyComponents);
    }

    public Task<List<EditActivityComponent>> CreateEditActivityComponents(Transform scrollViewTransform, UnityEvent onClickedComponentEvent)
    {
        List<EditActivityComponent> editActivityComponents = new List<EditActivityComponent>();

        if (editActivityComponentPrefab != null)
        {

            List<Activity> activitiesFromDailyToEdit = currentTrainerController.GetDailyToEdit().Activities;

            foreach (Activity act in activitiesFromDailyToEdit)
            {
                //Acá entra un método para traer, desde el trianing controller, el entrenamiento actualizado
                //act.Training acá se setea el training

                UnityEvent<Activity> unityEvent = new UnityEvent<Activity>();

                unityEvent.AddListener((act) => currentTrainerController.SetActivityToEdit(act));
                unityEvent.AddListener((act) => onClickedComponentEvent.Invoke());

                EditActivityComponent editActivityComponent = Instantiate(editActivityComponentPrefab, scrollViewTransform);

                editActivityComponent.LoadComponent(act, unityEvent);

                editActivityComponents.Add(editActivityComponent);
            }

        }

        return Task.FromResult(editActivityComponents);
    }

    public Task<List<TrainingToSelectInActivityEditComponent>> CreateTrainingToSelectInActivityEditComponent(List<Training> trainings, Transform scrollViewTransform, UnityEvent<Training> onClickedComponentEvent)
    {
        List<TrainingToSelectInActivityEditComponent> trainingsToSelectInActivityEdit = new List<TrainingToSelectInActivityEditComponent>();

        if (editActivityComponentPrefab != null)
        {
            foreach (Training training in trainings)
            {
                TrainingToSelectInActivityEditComponent trainingToSelect = Instantiate(trainingToSelectInActivityEditComponentPrefab, scrollViewTransform);

                trainingToSelect.LoadComponent(training, onClickedComponentEvent);

                trainingsToSelectInActivityEdit.Add(trainingToSelect);
            }

        }

        return Task.FromResult(trainingsToSelectInActivityEdit);
    }

    public async Task<List<TrainingImage>> GetAllTrainingImages()
    {
        Task<ServiceResponse<List<TrainingImage>>> getTrainingImages = localFilesService.GetAllTrainignImages();

        await getTrainingImages;

        if (getTrainingImages.Result.Completed)
        {
            return getTrainingImages.Result.Returned;
        }
        else
        {
            notificationManager.GenericError(getTrainingImages.Result.Message);
            return getTrainingImages.Result.Returned;
        }
    }

    public async Task<List<TrainingVideo>> GetAllTrainingVideos()
    {
        Task<ServiceResponse<List<TrainingVideo>>> getTrainingVideos = localFilesService.GetAllTrainingVideos();

        await getTrainingVideos;

        if (getTrainingVideos.Result.Completed)
        {
            return getTrainingVideos.Result.Returned;
        }
        else
        {
            notificationManager.GenericError(getTrainingVideos.Result.Message);
            return getTrainingVideos.Result.Returned;
        }
    }

    public async Task<List<CommonTrainingViewComponent>> CreateCommonTrainingViewComponents(Transform scrollViewTransform, string textToSearch, string paramToSearch, UnityEvent onClickedComponentEvent)
    {
        List<CommonTrainingViewComponent> trainingComponents = new List<CommonTrainingViewComponent>();

        if (commonTrainingViewComponentPrefab != null)
        {
            Task<List<Training>> getTrainings = trainingController.GetTrainingsBySearchParams(textToSearch, paramToSearch);

            await getTrainings;

            foreach (Training tr in getTrainings.Result)
            {
                //Acá entra un método para traer, desde el trianing controller, el entrenamiento actualizado
                //act.Training acá se setea el training

                UnityEvent<Training> unityEvent = new UnityEvent<Training>();

                unityEvent.AddListener((tr) => currentTrainerController.SetSelectedTraining(tr));
                unityEvent.AddListener((tr) => onClickedComponentEvent.Invoke());

                CommonTrainingViewComponent trComponent = Instantiate(commonTrainingViewComponentPrefab, scrollViewTransform);

                trComponent.LoadComponent(tr, unityEvent);

                trainingComponents.Add(trComponent);
            }
        }

        return trainingComponents;
    }

    /*
    public async Task<bool> CreateTrainersInfoComponent(Transform targetTransform, UnityEvent<Trainer> buttonUnityEvent)
    {
        if (trainerInfoComponentPrefab!=null)
        {
            List<Trainer> trainers;

            Task<List<Trainer>> getTrainers = trainerController.GetTrainers();
            await getTrainers;
            trainers = getTrainers.Result;

            Debug.Log(trainers.Count);

            foreach (Trainer trainer in trainers)
            {
                TrainerInfoComponent tic = Instantiate(trainerInfoComponentPrefab, targetTransform);
                tic.LoadComponent(trainer, buttonUnityEvent);
                Debug.Log("trainer info en TIC, tID = " + trainer.Id);
            }


            if (trainers.Count <= 0)
            {
                //Si no hay ningun entrenador, debería inicializar lo mismo, pero bueno
                return false;
            }
            else
            {
                //debe haber entrenadores
                return true;
            }

        }
        else
        {
            notificationManager.GenericError("No hay una instancia del Trianer Info Component Prefab");
            return false;
        }
    }

    public async Task<List<DailyComponent>> CreateDailyActivityComponents(Transform scrollViewTransform, UnityEvent<Daily> OnClickedEvent)
    {
        Task<List<Daily>> getDailyActivities = routineController.GetDailyActivities();
        await getDailyActivities;
        List<DailyComponent> dailyComponents = new List<DailyComponent>();

        foreach(Daily da in getDailyActivities.Result)
        {
            DailyComponent dac = Instantiate(dailyComponentPrefab, scrollViewTransform);
            dac.LoadComponent(da, OnClickedEvent);
            dailyComponents.Add(dac);
        }

        return dailyComponents;
    }

    public async Task<List<ActivityComponent>> CreateActivityComponents(Transform scrollViewTransform, UnityEvent<Activity> OnClickedEvent)
    {
        Task<List<Activity>> getActivities = routineController.GetActivities();
        await getActivities;
        List<ActivityComponent> activityComponents = new List<ActivityComponent>();

        foreach (Activity act in getActivities.Result)
        {
            ActivityComponent actComp = Instantiate(activityComponentPrefab, scrollViewTransform);
            actComp.LoadComponent(act, OnClickedEvent);
            activityComponents.Add(actComp);
        }

        return activityComponents;
    }

    public Task<Activity> CurrentActivityInfo()
    {
        Activity activity = routineController.SelectedActivity;
        return Task.FromResult(activity);
    }*/
}
