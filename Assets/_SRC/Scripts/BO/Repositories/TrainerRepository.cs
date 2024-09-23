using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerRepository : Repository<Trainer>, IWebConnectable<Trainer>
{
    WebConnector webConnector;

    AvatarRepository avatarRepository;

    public override Task<bool> Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;

        avatarRepository = B2BTrainer.Instance.repositoryManager.avatarRepository;

        return Task.FromResult(true);
    }

    public async Task<RepositoryResponse<Trainer>> FindCurrent()
    {
        Trainer obtainedTrainer = new Trainer();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_CURRENT_TRAINER);

        Task<ConnectorInResponse> getTrainer = webConnector.GetConnection(connectorOutMessage);

        await getTrainer;

        if (webConnector.HttpStatusOk(getTrainer.Result.HttpStatus))
        {
            obtainedTrainer = GetEntityFromResponse(getTrainer.Result);
            return new RepositoryResponse<Trainer>("REP: Found Online.", obtainedTrainer);
        }
        else
        {
            return new RepositoryResponse<Trainer>("REP: Couldn't find Online.", obtainedTrainer);
        }
    }

    public async override Task<RepositoryResponse<Trainer>> FindById(long id)
    {
        Trainer obtainedTrainer = new Trainer();

        foreach (Trainer trainer in entities)
        {
            if (trainer.Id == id)
            {
                return new RepositoryResponse<Trainer>("REP: Found.", trainer);
            }
        }

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TRAINER_BY_ID, id.ToString());

        Task<ConnectorInResponse> getTrainer = webConnector.GetConnection(connectorOutMessage);

        await getTrainer;

        if (webConnector.HttpStatusOk(getTrainer.Result.HttpStatus))
        {
            obtainedTrainer = GetEntityFromResponse(getTrainer.Result);
            return new RepositoryResponse<Trainer>("REP: Found Online.", obtainedTrainer);
        }
        else
        {
            return new RepositoryResponse<Trainer>("REP: Couldn't find Online.", obtainedTrainer);
        }
    }

    public async override Task<RepositoryResponse<List<Trainer>>> FindAll()
    {
        List<Trainer> trainers = new List<Trainer>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ALL_TRAINERS);

        Task<ConnectorInResponse> getAllTrainers = webConnector.GetConnection(connectorOutMessage);

        await getAllTrainers;

        if (webConnector.HttpStatusOk(getAllTrainers.Result.HttpStatus))
        {
            trainers = GetListFromResponse(getAllTrainers.Result);
            return new RepositoryResponse<List<Trainer>>("REP: Found Online.", trainers);
        }
        else
        {
            return new RepositoryResponse<List<Trainer>>("REP: Couldn't find Online.", trainers);
        }
    }

    public List<Trainer> GetListFromResponse(ConnectorInResponse connectorInResponse) //y si agregamos el try catch fuera de este bloque?
    {
        List<Trainer> trainers = new List<Trainer>();

        JSONArray json = new JSONArray();
        try
        {
            json = (JSONArray)JSONObject.Parse(connectorInResponse.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("Training parsing exception " + e);
        }

        for (int i = 0; i < json.Count; i++)
        {
            Trainer newTrainer = GetEntityFromJSON(json[i].AsObject);

            if (newTrainer.Id != 0)
            {
                trainers.Add(newTrainer);
            }
        }

        //Debug.Log(jsonArray.AsArray);
        return trainers;
    }

    public Trainer GetEntityFromResponse(ConnectorInResponse connectorInResponse)
    {
        Trainer newTrainer;

        try
        {
            JSONObject newJson = JSONNode.Parse(connectorInResponse.Message).AsObject;

            newTrainer = GetEntityFromJSON(newJson);

            SaveInRepository(newTrainer);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            newTrainer = new Trainer();
        }

        return newTrainer;
    }

    public Trainer GetEntityFromJSON(JSONObject json)
    {

        Trainer newTrainer = new Trainer(json["id"], json["firstname"], json["lastname"], json["username"], json["avatar"], json["description"]);

        if (newTrainer.Id != 0)
        {
            SaveInRepository(newTrainer);
        }

        return newTrainer;
    }

    protected override void SaveInRepository(Trainer ent)
    {
        if (entities == null)
        {
            entities = new List<Trainer>();
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
