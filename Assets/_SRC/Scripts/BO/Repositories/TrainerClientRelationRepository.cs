using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrainerClientRelationRepository : Repository<TrainerClientRelation>, IWebConnectable<TrainerClientRelation>
{
    WebConnector webConnector;

    public override Task<bool> Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;
        return Task.FromResult(true);
    }

    public async override Task<RepositoryResponse<TrainerClientRelation>> FindById(long id)
    {
        TrainerClientRelation trainerClientRelation = new TrainerClientRelation();

        foreach (TrainerClientRelation tcr in entities)
        {
            if (tcr.Id == id)
            {
                return new RepositoryResponse<TrainerClientRelation>("REP: Found.", tcr);
            }
        }

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TCR_BY_ID, id.ToString());

        Task<ConnectorInResponse> getTrainerClientRelation = webConnector.GetConnection(connectorOutMessage);

        await getTrainerClientRelation;

        if (webConnector.HttpStatusOk(getTrainerClientRelation.Result.HttpStatus))
        {
            trainerClientRelation = GetEntityFromResponse(getTrainerClientRelation.Result);
            return new RepositoryResponse<TrainerClientRelation>("REP: Found Online.", trainerClientRelation);
        }
        else
        {
            return new RepositoryResponse<TrainerClientRelation>("REP: Couldn't find Online.", trainerClientRelation);
        }
    }

    public async override Task<RepositoryResponse<List<TrainerClientRelation>>> FindAll()
    {
        List<TrainerClientRelation> trainerClientRelations = new List<TrainerClientRelation>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ALL_TCRS);

        Task<ConnectorInResponse> getTCRs = webConnector.GetConnection(connectorOutMessage);

        await getTCRs;

        if (webConnector.HttpStatusOk(getTCRs.Result.HttpStatus))
        {
            trainerClientRelations = GetListFromResponse(getTCRs.Result);
            return new RepositoryResponse<List<TrainerClientRelation>>("REP: Found Online.", trainerClientRelations);
        }
        else
        {
            return new RepositoryResponse<List<TrainerClientRelation>>("REP: Couldn't find Online.", trainerClientRelations);
        }
    }

    public async Task<RepositoryResponse<List<TrainerClientRelation>>> FindByStatus(long id)
    {
        List<TrainerClientRelation> trainerClientRelations = new List<TrainerClientRelation>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_TCRS_BY_STATUS, id.ToString());

        Task<ConnectorInResponse> getTCRs = webConnector.GetConnection(connectorOutMessage);

        await getTCRs;

        if (webConnector.HttpStatusOk(getTCRs.Result.HttpStatus))
        {
            trainerClientRelations = GetListFromResponse(getTCRs.Result);
            return new RepositoryResponse<List<TrainerClientRelation>>("REP: Found Online.", trainerClientRelations);
        }
        else
        {
            return new RepositoryResponse<List<TrainerClientRelation>>("REP: Couldn't find Online.", trainerClientRelations);
        }
    }

    public List<TrainerClientRelation> GetListFromResponse(ConnectorInResponse connectorInResponse) //y si agregamos el try catch fuera de este bloque?
    {
        List<TrainerClientRelation> trainerClientRelations = new List<TrainerClientRelation>();

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
            TrainerClientRelation trainerClientRelation = GetEntityFromJSON(json[i].AsObject);

            if (trainerClientRelation.Id != 0)
            {
                trainerClientRelations.Add(trainerClientRelation);
            }
        }

        //Debug.Log(jsonArray.AsArray);
        return trainerClientRelations;
    }

    public TrainerClientRelation GetEntityFromResponse(ConnectorInResponse connectorInResponse)
    {
        TrainerClientRelation newTCR;

        try
        {
            JSONObject newJson = JSONNode.Parse(connectorInResponse.Message).AsObject;

            newTCR = GetEntityFromJSON(newJson);
        }
        catch (NullReferenceException e)
        {
            newTCR = new TrainerClientRelation();
        }

        return newTCR;
    }

    public TrainerClientRelation GetEntityFromJSON(JSONObject json)
    {
        Client client = new Client();
        client.Id = json["clientBasicInfoOutDTO"].AsObject["id"];

        Routine routine;

        if (json["routineExtendedInfoOutDTO"] != null)
        {
            routine = new Routine();
            routine.Id = json["routineExtendedInfoOutDTO"].AsObject["id"];
        }
        else
        {
            routine = null;
        }

        DateTime createdAt = new DateTime();

        try
        {
            createdAt = DateTime.ParseExact(json["createdAt"], Constant.DEFAULT_SIMPLE_API_DATE_FORMAT, null);
        }
        catch (Exception e)
        {
        }
        DateTime updatedAt = new DateTime();

        try
        {
            updatedAt = DateTime.ParseExact(json["updatedAt"], Constant.DEFAULT_SIMPLE_API_DATE_FORMAT, null);
        }
        catch (Exception e)
        {
        }

        TrainerClientRelation newTCR = new TrainerClientRelation(json["id"], client, routine, json["status"], json["objective"], json["description"], createdAt, updatedAt);



        if (newTCR.Id != 0)
        {
            SaveInRepository(newTCR);
        }

        return newTCR;
    }

    protected override void SaveInRepository(TrainerClientRelation ent)
    {
        if (entities == null)
        {
            entities = new List<TrainerClientRelation>();
        }

        for(int i = 0; i < entities.Count; i++)
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
