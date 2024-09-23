using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoutineRepository : Repository<Routine>, IWebConnectable<Routine>
{
    WebConnector webConnector;

    public override Task<bool> Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;
        entities = new List<Routine>();
        return Task.FromResult(true);
    }

    public async override Task<RepositoryResponse<List<Routine>>> FindAll()
    {
        List<Routine> routines = new List<Routine>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ALL_ROUTINES);

        Task<ConnectorInResponse> getAllRoutines = webConnector.GetConnection(connectorOutMessage);

        await getAllRoutines;

        if (webConnector.HttpStatusOk(getAllRoutines.Result.HttpStatus))
        {
            routines = GetListFromResponse(getAllRoutines.Result);
            return new RepositoryResponse<List<Routine>>("REP: Found Online.", routines);
        }
        else
        {
            return new RepositoryResponse<List<Routine>>("REP: Couldn't find Online.", routines);
        }
    }

    public async override Task<RepositoryResponse<Routine>> FindById(long id)
    {
        Routine obtainedRoutine = new Routine();

        foreach (Routine rou in entities)
        {
            if (rou.Id == id)
            {
                return new RepositoryResponse<Routine>("REP: Found.", rou);
            }
        }

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ROUTINE_BY_ID, id.ToString());

        Task<ConnectorInResponse> getClientTask = webConnector.GetConnection(connectorOutMessage);

        await getClientTask;

        if (webConnector.HttpStatusOk(getClientTask.Result.HttpStatus))
        {
            obtainedRoutine = GetEntityFromResponse(getClientTask.Result);
            return new RepositoryResponse<Routine>("REP: Found Online.", obtainedRoutine);
        }
        else
        {
            return new RepositoryResponse<Routine>("REP: Couldn't find Online.", obtainedRoutine);
        }
    }
    public async Task<RepositoryResponse<bool>> CreateNew(NewRoutineOutDTO outdto)
    {
        ConnectorOutMessage<OutDTO> connectorOutMessage = new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_CREATE_NEW_ROUTINE, outdto);

        Task<ConnectorInResponse> createNewRoutine = webConnector.PostConnection(connectorOutMessage);

        await createNewRoutine;

        if (webConnector.HttpStatusOk(createNewRoutine.Result.HttpStatus))
        {
            return new RepositoryResponse<bool>("REP: Routine created.", true);
        }
        else
        {
            return new RepositoryResponse<bool>("REP: Couldn't create.", false);
        }
    }

    public Routine GetEntityFromResponse(ConnectorInResponse connectorInResponse)
    {
        Routine routineFromResponse;

        try
        {
            JSONObject newJson = JSONNode.Parse(connectorInResponse.Message).AsObject;

            routineFromResponse = GetEntityFromJSON(newJson);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            routineFromResponse = new Routine();
        }

        return routineFromResponse;
    }

    public List<Routine> GetListFromResponse(ConnectorInResponse connectorInResponse)
    {
        List<Routine> routines = new List<Routine>();

        JSONArray json = new JSONArray();
        try
        {
            json = (JSONArray)JSONObject.Parse(connectorInResponse.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("Response parsing exception " + e);
        }

        for (int i = 0; i < json.Count; i++)
        {
            Routine newRoutine = GetEntityFromJSON(json[i].AsObject);

            if (newRoutine.Id != 0)
            {
                routines.Add(newRoutine);
            }
        }

        return routines;
    }

    public Routine GetEntityFromJSON(JSONObject json)
    {
        Routine routineFromJson = new Routine();

        routineFromJson.Id = json["id"];

        JSONArray dailyActivitiesArray = json["dailyActivities"].AsArray;

        for (int i = 0; i < dailyActivitiesArray.Count; i++)
        {
            Daily newDaily = new Daily();
            newDaily.Completed = false;
            newDaily.Id = dailyActivitiesArray[i].AsObject["id"];
            newDaily.OrderNumber = dailyActivitiesArray[i].AsObject["dayNumber"];

            JSONArray activitiesJsonArray = dailyActivitiesArray[i].AsObject["activities"].AsArray;

            try
            {
                newDaily.ProposedDate = DateTime.ParseExact(dailyActivitiesArray[i].AsObject["proposedDate"], Constant.DEFAULT_SIMPLE_API_DATE_FORMAT, null);
            }
            catch (Exception e)
            {
                
            }

            try
            {
                newDaily.CompletionDate = DateTime.ParseExact(dailyActivitiesArray[i].AsObject["completionDate"], Constant.DEFAULT_SIMPLE_API_DATE_FORMAT, null);
            }
            catch (Exception e)
            {
                newDaily.CompletionDate = Constant.DefaultDateTime; //No completado, poniendo una fecha por defecto
            }

            if (newDaily.CompletionDate != Constant.DefaultDateTime)
            {
                newDaily.Completed = true;
            }

            for (int j = 0; j < activitiesJsonArray.Count; j++)
            {
                Activity newActivity = new Activity();

                newActivity.Id = activitiesJsonArray[j].AsObject["id"];
                newActivity.TrainingId = activitiesJsonArray[j].AsObject["trainingId"];
                newActivity.OrderNumber = activitiesJsonArray[j].AsObject["orderNumber"];
                newActivity.ActTypeId = activitiesJsonArray[j].AsObject["actTypeId"];
                newActivity.TypeValue = activitiesJsonArray[j].AsObject["actTypeValue"];

                newActivity.Completed = newDaily.Completed;

                newDaily.AddActivity(newActivity);
            }

            routineFromJson.AddDailyActivity(newDaily);
        }

        //ordenar las daily por id(mayor id, al final)

        routineFromJson.StartDate = routineFromJson.DailyActivities[0].ProposedDate;
        routineFromJson.NumberOfDays = routineFromJson.DailyActivities.Count;

        //espacio para cada uno de los campos

        if (routineFromJson.Id != 0)
        {
            SaveInRepository(routineFromJson);
        }

        return routineFromJson;
    }

    protected override void SaveInRepository(Routine ent)
    {
        if (entities == null)
        {
            entities = new List<Routine>();
        }

        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i].Id == ent.Id)
            {
                entities[i] = ent;
                return;
            }
        }

        entities.Add(ent);
    }
}
