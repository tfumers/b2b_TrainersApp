using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoutineController : Controller
{
    NotificationManager notificationManager;

    RoutineService routineService;

    public override Task<bool> Initialize()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;

        routineService = B2BTrainer.Instance.serviceManager.routineService;

        return Task.FromResult(true);
    }

    public async Task<bool> CreateNewRoutine(TrainerClientRelation received)
    {
        Task<ServiceResponse> createNewRoutine = routineService.CreateNewRoutine(new NewRoutineOutDTO(received));

        await createNewRoutine;

        if (createNewRoutine.Result.Completed == false)
        {
            notificationManager.GenericError(createNewRoutine.Result.Message);
        }

        return createNewRoutine.Result.Completed;
    }

    public async Task<bool> EditRoutine(Dictionary<string, string> received)
    {
        Task<ServiceResponse> editRoutine = routineService.EditRoutine(new EditRoutineOutDTO(received));

        await editRoutine;

        if (editRoutine.Result.Completed == false)
        {
            notificationManager.GenericError(editRoutine.Result.Message);
        }

        return editRoutine.Result.Completed;
    }

    public async Task<Routine> GetRoutineById(long id)
    {
        Task<ServiceResponse<Routine>> getRoutineById = routineService.GetRoutineById(id);

        await getRoutineById;

        if (getRoutineById.Result.Completed == false)
        {
            notificationManager.GenericError(getRoutineById.Result.Message);
        }

        return getRoutineById.Result.Returned;
    }

    // CÓDIGO VIEJO
    WebConnectionManager webConnectionManager;

    LocalFilesService localFilesService;

    List<Routine> routines;

    Routine selectedRoutine;

    Daily selectedDaily;

    Activity selectedActivity;

    List<Training> trainings;
    /*
    public void Start()
    {
        webConnectionManager = B2BTrainer.Instance.webConnectionManager;
        notificationManager = B2BTrainer.Instance.notificationManager;
        localFilesService = B2BTrainer.Instance.serviceManager.localFilesService;
        routines = new List<Routine>();
        trainings = new List<Training>();
    }*/

    public async Task<Routine> CreateRoutine()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Routine>> GetRoutinesByTrainer(Trainer trainer)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Routine>> GetAllRoutines()
    {
        throw new NotImplementedException();
    }

    public async Task<Routine> GetRoutine(long id = 0)
    {
        Task<List<Routine>> getRoutines = GetRoutines();
        await getRoutines;
        routines = getRoutines.Result;
        //Acá se podría agregar una lógica condicional. En base a, si hubo una actualización de las rutinas, actualizarlas o no.
        foreach (Routine routine in routines)
        {
            if (routine.Id == id)
            {
                return routine;
            }
        }

        return null;
    }

    public async Task<List<Routine>> GetRoutines() 
    {
        Task<ResponseDTO> getRoutines = webConnectionManager.GetRoutines();
        await getRoutines;

        ResponseDTO response = getRoutines.Result;
        List<Routine> obtainedRoutines = new List<Routine>();
        List<Training> obtainedTrainings = new List<Training>();
        try
        {
            obtainedTrainings = DataAdapter.RoutineResponseToTrainingList(response);
            obtainedRoutines = DataAdapter.ResponseToRoutineList(response);
        }
        catch (Exception e)
        {
            //Debug.Log("error en el try catch de las rutinas " + e.Message);
        }

        foreach(Routine routine in obtainedRoutines)
        {
            if(selectedRoutine == null)
            {
                selectedRoutine = routine;
            }

            if(selectedRoutine.Id < routine.Id)
            {
                selectedRoutine = routine;
            }
        }

        for (int i = 0; i < obtainedTrainings.Count; i++)
        {
            obtainedTrainings[i] = SetTrainingResources(obtainedTrainings[i]);
        }

        trainings = obtainedTrainings;

        foreach(Daily daily in selectedRoutine.DailyActivities)
        {
            foreach(Activity act in daily.Activities)
            {
                act.Training = GetTrainingById(act.TrainingId);
            }
        }

        return obtainedRoutines;
    }

    public async Task<List<Daily>> GetDailyActivities(Routine routine = null)
    {
        Task<List<Routine>> getRoutines = GetRoutines();
        await getRoutines;
        routines = getRoutines.Result;

        return ReturnDailiesByWeek(OrderDailiesByDay(selectedRoutine.DailyActivities));
    }

    internal Task<List<Activity>> GetActivities()
    {
        return Task.FromResult(OrderActivities(selectedDaily.Activities));
    }

    internal Training GetTrainingById(long id)
    {

        foreach(Training tr in trainings)
        {
            if(tr.Id == id)
            {
                return tr;
            }
        }

        throw new Exception();
    }

    public async Task<List<Training>> GetTrainings()
    {
        if (trainings != null)
        {
            string[] trainingIds = ObtainTrainingIdsFromCurrentRoutine();

            Task<ResponseDTO> getTrainingsInfo = webConnectionManager.PostAndReturnRequiredTrainings(new EditRoutineOutDTO(new Dictionary<string, string>()));
            await getTrainingsInfo;
            List<Training> obtainedTrainings = new List<Training>();
            try
            {
                obtainedTrainings = DataAdapter.ResponseToTrainingList(getTrainingsInfo.Result);
                for (int i = 0; i < obtainedTrainings.Count; i++) {
                    obtainedTrainings[i] = SetTrainingResources(obtainedTrainings[i]);
                }
            }
            catch (Exception e)
            {
                //Debug.Log("error en el try catch de los trainigs " + e.Message);
            }

            trainings = obtainedTrainings;
        }


        return trainings;
    }

    private string[] ObtainTrainingIdsFromCurrentRoutine()
    {
        if (selectedRoutine != null)
        {
            List<string> trainingIds = new List<string>();

            foreach (Daily da in selectedRoutine.DailyActivities)
            {
                foreach (Activity act in da.Activities)
                {
                    trainingIds.Add(act.TrainingId.ToString());
                }
            }

            return trainingIds.ToArray();
        }

        return new string[0];
    }

    public void SetSelectedDaily(Daily daily)
    {
        selectedDaily = daily;

        if (!daily.Completed)
        {
            foreach (Activity act in daily.Activities)
            {
                act.Completed = false;
                act.ElapsedTime = 0;
            }
        }

        //Debug.Log("El día propuesto es el " + daily.ProposedDate.ToString());
    }

    public void SetSelectedActivity(Activity activity)
    {
        selectedActivity = activity;
    }

    public async void SaveCompletedActivity(Activity activity)
    {
        if (selectedActivity == activity)
        {
            selectedActivity.Completed = true;
        }

        CheckCompletedActivities();
    }

    private async void CheckCompletedActivities()
    {
        int count = 0;
        foreach (Activity act in selectedDaily.Activities)
        {
            if (act.Completed)
            {
                count++;
            }
        }

        if(selectedDaily.Activities.Count == count)
        {
            Task<ResponseDTO> updateCompletedActivities = null;// webConnectionManager.UpdateCompletedDailyActivities(new CompletedDailyActivitiesOutDTO(selectedDaily, selectedRoutine));
            await updateCompletedActivities;
            if (updateCompletedActivities.Result.HttpStatus != 200)
            {
                notificationManager.GenericError("Error guardando los datos del usuario en la bdd " + updateCompletedActivities.Result.Message);
                //Debug.Log(updateCompletedActivities.Result.HttpStatus + " HTTP STATUS");
            }
            else
            {
                //Debug.Log("Datos guardados con éxito");
            }
            selectedDaily.Completed = true;
        }
    }

    private Training SetTrainingResources(Training training)
    {
        Task<ServiceResponse<TrainingImage>> getTrainingImage = localFilesService.GetTrainignImageByName(training.ImageUrl);
        training.TrainingImage = getTrainingImage.Result.Returned;

        Task <ServiceResponse<TrainingVideo>> getTrainingVideo = localFilesService.GetTrainignVideoByName(training.VideoUrl);
        training.TrainingVideo = getTrainingVideo.Result.Returned;

        return training;
    }


    //En realidad, quien debe tener el método genérico para ordenar entidades es el repositorio, usando uan clase tipo T
    //Se deja acá para análisis posterior
    /*
    private List<OrderedEntity> ListEntitiesByOrder(List<OrderedEntity> orderedEntities)
    {
        OrderedEntity[] arrayOfOrderedEntities = new OrderedEntity[orderedEntities.Count];

        foreach(OrderedEntity oe in orderedEntities)
        {
            if (oe.OrderNumber != 0)
            {
                arrayOfOrderedEntities[oe.OrderNumber] = oe;
            }
        }

        return new List<OrderedEntity>(arrayOfOrderedEntities);
    }*/

    private List<Daily> ReturnDailiesByWeek(List<Daily> dailies)
    {
        Daily[] unorderedArray = new Daily[7];
        Daily[] orderedArray = new Daily[7];
        //Debug.Log("Conteo de actividades diarias " + dailies.Count);


        int counter = 0;
        bool aDayIsNotCompleted = false;
        int posOnTheArray = 0;

        DateTime firstDayDate = DateTime.Today;
        int dayOfTheWeekPosition = 0;

        for(int pos = 0; pos < dailies.Count; pos++)
        {
            unorderedArray[counter] = dailies[pos];
            //Debug.Log("El valor, en el array desordenado, se guardo en la pos " + (counter) + ", siendo el día " + dailies[pos].ProposedDate.DayOfWeek.ToString());

            if (!aDayIsNotCompleted && !dailies[pos].Completed)
            {
                aDayIsNotCompleted = true; // existe un día que no está completado

                firstDayDate = dailies[pos].ProposedDate; //se guarda la fecha de ese día
                dayOfTheWeekPosition = (int)dailies[pos].ProposedDate.DayOfWeek; // se guarda el día de la semana que representa
                orderedArray[dayOfTheWeekPosition - 1] = dailies[pos]; //se guarda en la posición específica en la lista ordenada (-1 para que esté en la pos correcta)
                //Debug.Log("El valor, en el array ordenado, se guardo en la pos " + (dayOfTheWeekPosition - 1) + ", siendo el día " + dailies[pos].ProposedDate.DayOfWeek.ToString());

                posOnTheArray = counter; //se guarda la posición, en el arreglo desordenado, en que se encuentra
            }

            counter++;
            if(counter == 7 || (pos+1==dailies.Count))
            {
                if (aDayIsNotCompleted==true)//si hay un día dentro de la lista que está completo
                {
                    for(int i = 0; i < unorderedArray.Length; i++)
                    {
                        if (unorderedArray[i] != null)
                        {
                            int dateToCompare = unorderedArray[i].ProposedDate.Subtract(firstDayDate).Days;
                            //Debug.Log("La comparación entre " + unorderedArray[i].ProposedDate + " y " + firstDayDate + "como resultado" + dateToCompare);

                            if (dateToCompare > 0)
                            {
                                if (dayOfTheWeekPosition + dateToCompare <= 7)
                                {
                                    orderedArray[dayOfTheWeekPosition + dateToCompare - 1] = unorderedArray[i];
                                    //Debug.Log("El valor, en el array ordenado, se guardo en la pos " + (dayOfTheWeekPosition + dateToCompare) + "");
                                }
                            }

                            if (dateToCompare < 0)//si la fecha a comparar es menor, quiere decir que está antes en el array sin ordenar
                            {
                                dateToCompare = (-dateToCompare);

                                if (dateToCompare < dayOfTheWeekPosition)
                                {
                                    orderedArray[dayOfTheWeekPosition - dateToCompare - 1] = unorderedArray[i];
                                    //Debug.Log("El valor, en el array ordenado, se guardo en la pos " + (dayOfTheWeekPosition - dateToCompare) + "");
                                }
                            }
                        }
                    }
                    counter = posOnTheArray;
                }
                else // si ningún día esta completado, se reinicia la lista
                {
                    counter = 0;
                }
                //se comprueban los siguientes elementos de daitlyToOrder
            }
        }

        DateTime MondayOfThatWeek = firstDayDate;

        if (dayOfTheWeekPosition != 1)
        {
            MondayOfThatWeek = firstDayDate.AddDays(- (dayOfTheWeekPosition-1));
        }

        //Debug.Log("El día " + MondayOfThatWeek.DayOfWeek.ToString() + " es el día " + MondayOfThatWeek);

        for (int i = 0; i < orderedArray.Length; i++)
        {
            if (orderedArray[i] == null)
            {
                orderedArray[i] = new Daily();
                orderedArray[i].ProposedDate = MondayOfThatWeek.AddDays(i);
            }
        }

        return new List<Daily>(orderedArray);
    }

    private List<Daily> OrderDailiesByDay(List<Daily> dailies)
    {
        Daily[] dailyArray = new Daily[dailies.Count];
        Debug.Log("Conteo " + dailies.Count);

        foreach (Daily dailyToOrder in dailies)
        {
            if (dailyToOrder.OrderNumber != 0)
            {
                dailyArray[dailyToOrder.OrderNumber-1] = dailyToOrder;
            }
        }

        return new List<Daily>(dailyArray);
    }

    private List<Activity> OrderActivities(List<Activity> activities)
    {
        Activity[] activitiesArray = new Activity[activities.Count];

        foreach(Activity act in activities)
        {
            if (act.OrderNumber != 0)
            {
                activitiesArray[act.OrderNumber-1] = act;
            }

        }

        return new List<Activity>(activitiesArray);
    }
}
