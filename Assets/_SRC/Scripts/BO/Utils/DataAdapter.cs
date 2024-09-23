using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using System.Text;

public class DataAdapter
{
    public DataAdapter()
    {

    }


    public static ResponseDTO StringToResponse(string received)
    {
        ResponseDTO newResponse;
        JSONObject newJson = JSONNode.Parse(received).AsObject;

        newResponse = new ResponseDTO(
            newJson["httpStatus"],
            newJson["message"]);

        return newResponse;
    }

    public static Client StringToClient(string received)
    {
        Client newClient;

        try
        {
            JSONObject newJson = JSONNode.Parse(received).AsObject;

            newClient = new Client(newJson);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
            return null;
        }

        return newClient;
    }

    public static List<Routine> ResponseToRoutineList(ResponseDTO received)
    {
        List<Routine> routines = new List<Routine>();
        JSONArray routinesJsonArray = (JSONArray)JSONNode.Parse(received.Message);
        
        for(int i = 0; i < routinesJsonArray.Count; i++)
        {
            Debug.Log(routinesJsonArray[i].AsObject);
            Routine newRoutine = new Routine(routinesJsonArray[i].AsObject);
            routines.Add(newRoutine);
        }

        return routines;
    }

    public static List<Trainer> ResponseToTrainerList(ResponseDTO received)
    {
        List<Trainer> trainers = new List<Trainer>();

        JSONArray json = new JSONArray();
        try
        {
            json = (JSONArray)JSONObject.Parse(received.Message);
        }
        catch(Exception e)
        {
            Debug.LogError("Trainer parsing exception " + e);
        }

        for(int i = 0; i < json.Count; i++)
        {
            trainers.Add(new Trainer(json[i].AsObject));
        }

        //Debug.Log(jsonArray.AsArray);
        return trainers;
    }

    public static List<Training> ResponseToTrainingList(ResponseDTO received)
    {
        List<Training> trainings = new List<Training>();

        JSONArray json = new JSONArray();
        try
        {
            json = (JSONArray)JSONObject.Parse(received.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("Trainer parsing exception " + e);
        }

        for (int i = 0; i < json.Count; i++)
        {
            trainings.Add(new Training(json[i].AsObject));
        }

        //Debug.Log(jsonArray.AsArray);
        return trainings;
    }

    public static List<Training> RoutineResponseToTrainingList(ResponseDTO received)
    {
        List<Training> trainings = new List<Training>();
        JSONArray routinesJsonArray = (JSONArray)JSONNode.Parse(received.Message);
        Debug.Log("message " + received.Message);
        Debug.Log(routinesJsonArray.Count + "asdasd asdas das ");

        JSONArray trainingsJSON = new JSONArray();
        JSONObject routineJSON = new JSONObject();
        try
        {

            for (int i = 0; i < routinesJsonArray.Count; i++)
            {
                routineJSON = routinesJsonArray[i].AsObject;
                Debug.Log(routineJSON + " on RoutineRepsone to training list");
                trainingsJSON = routineJSON["trainingDTOs"].AsArray;
                for (int j = 0; j < trainingsJSON.Count; j++)
                {
                    trainings.Add(new Training(trainingsJSON[j].AsObject));
                    Debug.Log(trainingsJSON[j].AsObject + " pos J:" + j);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Trainer parsing exception " + e);
        }

        //Debug.Log(jsonArray.AsArray);
        return trainings;
    }
}
