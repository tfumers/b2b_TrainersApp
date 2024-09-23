using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Daily
{
    long id;

    private List<Activity> activities = new List<Activity>();

    private bool completed;

    private int orderNumber;

    private DateTime proposedDate;

    private DateTime completionDate = DateTime.Now;

    public Daily()
    {
        this.id = 0;
        completed = false;
        orderNumber = 0;
    }

    public Daily(long id, List<Activity> activities, int dayNumber)
    {
        this.id = id;
        this.activities = activities;
    }

    public Daily(JSONObject json)
    {
        this.id = json["id"];
        //Acá se hizo un cambio exp´licito, para permitir el uso de funciones genéricas
        //Se mantiene el contexto de que es un tipo de orden, solo que cambia el nombre de la variable
        this.orderNumber = json["dayNumber"];
        JSONArray activitiesJsonArray = json["activities"].AsArray;

        try
        {
            proposedDate = DateTime.ParseExact(json["proposedDate"], "yyyy-MM-dd", null);
        }
        catch (Exception e)
        {
            return;
        }

        try
        {
            completionDate = DateTime.ParseExact(json["completionDate"],"yyyy-MM-dd", null);
        }
        catch (Exception e)
        {
            completionDate = Constant.DefaultDateTime ; //No completado, poniendo una fecha por defecto
        }

        if(completionDate != Constant.DefaultDateTime)
        {
            completed = true;
        }

        for (int i = 0; i < activitiesJsonArray.Count; i++)
        {
            Debug.Log(activitiesJsonArray[i].AsObject);
            Activity newActivity= new Activity(activitiesJsonArray[i].AsObject);

            newActivity.Completed = completed;

            /*if (completionDate == DateTime.Today)
            {
                newActivity.Completed = true;
            }*/
            activities.Add(newActivity);
        }
    }

    public long Id { get => id; set => id = value; }
    public List<Activity> Activities { get => activities; set => activities = value; }


    public int DayNumber { get => orderNumber; set => orderNumber = value; }
    public bool Completed { get => completed; set => completed = value; }
    public DateTime ProposedDate { get => proposedDate; set => proposedDate = value; }
    public int OrderNumber { get => orderNumber; set => orderNumber = value; }
    public DateTime CompletionDate { get => completionDate; set => completionDate = value; }
    public void AddActivity(Activity activity)
    {
        if (activities == null)
        {
            activities = new List<Activity>();
        }

        activities.Add(activity);
    }

    public void SetProposedDate(string date)
    {
        try
        {
            proposedDate = DateTime.Parse(date);
            Debug.Log("date saved");
        }
        catch (Exception e)
        {
            Debug.Log("date not saved");
            return;
        }
    }

    //private LocalDate proposedDate;

    //private LocalDate completionDate;
}
