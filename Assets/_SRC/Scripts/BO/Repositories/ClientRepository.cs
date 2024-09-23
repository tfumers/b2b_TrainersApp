using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientRepository : Repository<Client>, IWebConnectable<Client>
{
    WebConnector webConnector;

    bool allObtained = false;

    //considerar agregar un datetime para comparar cuando se busco un repo completo, así evitamos múltiples llamadas al sv.
    //Aún así, ¿cual es la necesidad de disponer tanto en la memoria del equipo? El trainer no la va a usar en todo momento.

    public override Task<bool> Initialize()
    {
        webConnector = B2BTrainer.Instance.webConnectionManager.webConnector;
        entities = new List<Client>();
        return Task.FromResult(true);
    }

    public async override Task<RepositoryResponse<List<Client>>> FindAll()
    {
        List<Client> clients = new List<Client>();

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_ALL_CLIENTS);

        Task<ConnectorInResponse> getAllClients = webConnector.GetConnection(connectorOutMessage);

        await getAllClients;

        if (webConnector.HttpStatusOk(getAllClients.Result.HttpStatus))
        {
            clients = GetListFromResponse(getAllClients.Result);
            return new RepositoryResponse<List<Client>>("REP: Found Online.", clients);
        }
        else
        {
            return new RepositoryResponse<List<Client>>("REP: Couldn't find Online.", clients);
        }
    }

    public async override Task<RepositoryResponse<Client>> FindById(long id)
    {
        Client obtainedClient = new Client();

        foreach(Client cli in entities)
        {
            if (cli.Id == id)
            {
                return new RepositoryResponse<Client>("REP: Found.", cli);
            }
        }

        ConnectorOutMessage connectorOutMessage = new ConnectorOutMessage(Constant.TRAINERS_KEYNAME_GET_CLIENT_BY_ID, id.ToString());

        Task<ConnectorInResponse> getClientTask = webConnector.GetConnection(connectorOutMessage);

        await getClientTask;

        if (webConnector.HttpStatusOk(getClientTask.Result.HttpStatus))
        {
            obtainedClient = GetEntityFromResponse(getClientTask.Result);
            return new RepositoryResponse<Client>("REP: Found Online.", obtainedClient);
        }
        else
        {
            return new RepositoryResponse<Client>("REP: Couldn't find Online.", obtainedClient);
        }
    }


    public Client GetEntityFromResponse(ConnectorInResponse connectorInResponse)
    {
        Client clientFromResponse;

        try
        {
            JSONObject newJson = JSONNode.Parse(connectorInResponse.Message).AsObject;

            clientFromResponse = GetEntityFromJSON(newJson);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            clientFromResponse = new Client();
        }

        return clientFromResponse;
    }

    public List<Client> GetListFromResponse(ConnectorInResponse connectorInResponse)
    {
        List<Client> clients = new List<Client>();

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
            Client newClient = GetEntityFromJSON(json[i].AsObject);

            if (newClient.Id != 0)
            {
                clients.Add(newClient);
            }
        }

        //Debug.Log(jsonArray.AsArray);
        return clients;
    }


    public Client GetEntityFromJSON(JSONObject json)
    {
        Client clientFromJson = new Client();

        //espacio para cada uno de los campos

        clientFromJson.Id = json["id"];
        clientFromJson.Status = json["status"];
        clientFromJson.Username = json["username"];
        clientFromJson.Firstname = json["firstname"];
        clientFromJson.Lastname = json["lastname"];
        clientFromJson.Nationality = json["nationality"];

        try
        {
            clientFromJson.BirthDate = DateTime.ParseExact(json["birthDate"], Constant.DEFAULT_SIMPLE_API_DATE_FORMAT, null);
        }
        catch (Exception e)
        {

        }
        
        clientFromJson.Phone = json["phone"];
        clientFromJson.Sex = json["sex"];
        clientFromJson.Icon = json["icon"];
        clientFromJson.Dni = json["dni"];
        clientFromJson.Level = json["level"];
        clientFromJson.Experience = json["experience"];
        clientFromJson.Avatar = json["avatar"];
        clientFromJson.AvatarImage = new Avatar();
        clientFromJson.Height = json["height"];
        clientFromJson.StarterWeight = json["starterWeight"];
        clientFromJson.Pregnancy = json["pregnancy"];
        clientFromJson.TrainingOrSportsRecord = json["trainingOrSportsRecord"];
        clientFromJson.AvailableTrainingItems = json["availableTrainingItems"];
        clientFromJson.TrainingObjectives = json["trainingObjectives"];
        clientFromJson.Illnesses = json["illnesses"];
        clientFromJson.AvailableTrainingDays = json["availableTrainingDays"];
        clientFromJson.Wounds = json["wounds"];

        if (clientFromJson.Id != 0)
        {
            SaveInRepository(clientFromJson);
        }

        return clientFromJson;
    }

    protected override void SaveInRepository(Client ent)
    {
        if (entities == null)
        {
            entities = new List<Client>();
        }

        foreach(Client cli in entities)
        {
            if(cli.Id == ent.Id)
            {
                return;
            }
        }

        entities.Add(ent);
    }
}
