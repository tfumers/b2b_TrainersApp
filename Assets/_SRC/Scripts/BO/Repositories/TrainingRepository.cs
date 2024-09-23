using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainingRepository : Repository<Training>, IWebConnectable<Training>
{
    WebConnector webConnector;

    public override Task<bool> Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;
        entities = new List<Training>();

        return Task.FromResult(true);
    }

    public async override Task<RepositoryResponse<Training>> FindById(long id)
    {
        foreach (Training training in entities)
        {
            if (training.Id == id)
            {
                return new RepositoryResponse<Training>("REP: Found.", training);
            }
        }

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TRAINING_BY_ID, id.ToString());

        Task<ConnectorInResponse> getTraining = webConnector.GetConnection(connectorOutMessage);

        await getTraining;

        if (webConnector.HttpStatusAccepted(getTraining.Result.HttpStatus))
        {
            Training obtainedTraining = GetEntityFromResponse(getTraining.Result);
            return new RepositoryResponse<Training>("REP: Found Online.", obtainedTraining);
        }
        else
        {
            return new RepositoryResponse<Training>("REP: Couldn't find Online.", new Training());
        }
    }

    public async Task<RepositoryResponse<bool>> CreateNew(NewTrainingOutDTO outdto)
    {
        ConnectorOutMessage<OutDTO> connectorOutMessage = new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_CREATE_NEW_TRAINING, outdto);

        Task<ConnectorInResponse> createNewTraining = webConnector.PostConnection(connectorOutMessage);

        await createNewTraining;

        if (webConnector.HttpStatusCreated(createNewTraining.Result.HttpStatus))
        {
            return new RepositoryResponse<bool>("REP: Training created.", true);
        }
        else
        {
            return new RepositoryResponse<bool>("REP: Couldn't create.", false);
        }
    }

    public async override Task<RepositoryResponse<List<Training>>> FindAll()
    {
        List<Training> trainings = new List<Training>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ALL_TRAININGS);

        Task<ConnectorInResponse> getAllTrainings = webConnector.GetConnection(connectorOutMessage);

        await getAllTrainings;

        if (webConnector.HttpStatusOk(getAllTrainings.Result.HttpStatus))
        {
            trainings = GetListFromResponse(getAllTrainings.Result);
            return new RepositoryResponse<List<Training>>("REP: Found Online.", trainings);
        }
        else
        {
            return new RepositoryResponse<List<Training>>("REP: Couldn't find Online.", trainings);
        }
    }

    public async Task<RepositoryResponse<List<Training>>> GetByParams(TrainingSearchParamsOutDTO trainingSearchParamsOutDTO)
    {

        List<Training> trainings = new List<Training>();

        ConnectorOutMessage<OutDTO> connectorOutMessage = new ConnectorOutMessage<OutDTO>(Constant.TRAINERS_KEYNAME_TRAINING_BY_PARAMETERS, trainingSearchParamsOutDTO);

        Task<ConnectorInResponse> getTrainingsByParams = webConnector.PostConnection(connectorOutMessage);

        await getTrainingsByParams;

        if (webConnector.HttpStatusOk(getTrainingsByParams.Result.HttpStatus))
        {
            trainings = GetListFromResponse(getTrainingsByParams.Result);
            Debug.Log("Encontrado");
            return new RepositoryResponse<List<Training>>("REP: Found Online.", trainings);
        }
        else
        {
            Debug.Log("No Encontrado");
            return new RepositoryResponse<List<Training>>("REP: Couldn't find Online.", null);
        }
    }

    public async Task<RepositoryResponse<List<Training>>> FindByCategory(long catId)
    {
        List<Training> trainings = new List<Training>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TRAININGS_BY_CATEGORY, catId.ToString());

        Task<ConnectorInResponse> getAllTrainings = webConnector.GetConnection(connectorOutMessage);

        await getAllTrainings;

        if (webConnector.HttpStatusOk(getAllTrainings.Result.HttpStatus))
        {
            trainings = GetListFromResponse(getAllTrainings.Result);
            return new RepositoryResponse<List<Training>>("REP: Found Online.", trainings);
        }
        else
        {
            return new RepositoryResponse<List<Training>>("REP: Couldn't find Online.", trainings);
        }
    }

    public async Task<RepositoryResponse<List<Training>>> FindByDifficulty(long difId)
    {
        List<Training> trainings = new List<Training>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TRAININGS_BY_DIFFICULTY, difId.ToString());

        Task<ConnectorInResponse> getTraining = webConnector.GetConnection(connectorOutMessage);

        await getTraining;

        if (webConnector.HttpStatusOk(getTraining.Result.HttpStatus))
        {
            trainings = GetListFromResponse(getTraining.Result);
            return new RepositoryResponse<List<Training>>("REP: Found Online.", trainings);
        }
        else
        {
            return new RepositoryResponse<List<Training>>("REP: Couldn't find Online.", trainings);
        }
    }

    public List<Training> GetListFromResponse(ConnectorInResponse connectorInResponse) //y si agregamos el try catch fuera de este bloque?
    {
        List<Training> trainings = new List<Training>();

        JSONArray json = new JSONArray();
        try
        {
            json = (JSONArray)JSONObject.Parse(connectorInResponse.Message);
            Debug.Log(connectorInResponse.Message);
        }
        catch (Exception e)
        {
            //Debug.LogError("Training parsing exception " + e);
        }

        //Debug.Log("Conteo de objetos en el array de json: " + json.Count);

        for (int i = 0; i < json.Count; i++)
        {
            Training newTraining = GetEntityFromJSON(json[i].AsObject);

            if (newTraining.Id != 0)
            {
                trainings.Add(newTraining);
                Debug.Log("Training agregado");
            }
            else
            {
                Debug.Log("Training NO agregado");
            }
        }

        //Debug.Log("Conteo de entrenamientos: " + trainings.Count);

        //Debug.Log(jsonArray.AsArray);
        return trainings;
    }

    public Training GetEntityFromResponse(ConnectorInResponse connectorInResponse)
    {
        Training newTraining;

        try
        {
            JSONObject newJson = JSONNode.Parse(connectorInResponse.Message).AsObject;

            newTraining = GetEntityFromJSON(newJson);
            return newTraining;
        }
        catch (NullReferenceException e)
        {
            return new Training();
        }
    }

    public Training GetEntityFromJSON(JSONObject json)
    {
        Training newTraining = new Training();

        newTraining.Id = json["id"];
        newTraining.Difficulty = json["difficulty"];
        newTraining.Name = json["name"];
        newTraining.Description = json["description"];
        newTraining.VideoUrl = json["videoUrl"];
        
        newTraining.ImageUrl = json["imageUrl"];
        
        newTraining.EstTimePerRep = json["estTimePerRep"].AsInt;
        newTraining.EstCaloriesPerRep = json["estCaloriesPerRep"].AsInt;

        if (newTraining.Id != 0)
        {
            SaveInRepository(newTraining);
        }

        return newTraining;
    }

    protected override void SaveInRepository(Training ent)
    {
        if (entities == null)
        {
            entities = new List<Training>();
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
