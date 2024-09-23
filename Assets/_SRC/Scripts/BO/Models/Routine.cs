using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routine
{
    private long id;

    private int numberOfDays;

    private DateTime startDate;

    //private LocalDate completionDate;

    private List<Daily> dailyActivities = new List<Daily>();

    public Routine()
    {
        this.id = 0;
    }
    public Routine(JSONObject json)
    {
        this.id = json["id"];
        //Ncesitamos en bdd algo que nos diga el orden correcto de las rutinas

        JSONArray dailyActivitiesArray = json["dailyActivities"].AsArray;
        for(int i = 0; i < dailyActivitiesArray.Count; i++)
        {
            Debug.Log(dailyActivitiesArray[i].AsObject);
            Daily newDaily = new Daily(dailyActivitiesArray[i].AsObject);
            dailyActivities.Add(newDaily);
        }

        this.numberOfDays = dailyActivities.Count;

        //this.testString = json["activities"];
    }
    public int NumberOfDays { get => numberOfDays; set => numberOfDays = value; }
    public List<Daily> DailyActivities { get => dailyActivities; set => dailyActivities = value; }

    public long Id { get => id; set => id = value; }
    public DateTime StartDate { get => startDate; set => startDate = value; }

    public void AddDailyActivity(Daily daily)
    {
        if (dailyActivities == null)
        {
            dailyActivities = new List<Daily>();
        }

        dailyActivities.Add(daily);
    }

    public List<Daily> OrderDailyActivities()
    {
        Daily[] dailyArray = new Daily[DailyActivities.Count];
        Debug.Log("Conteo " + DailyActivities.Count);

        foreach (Daily dailyToOrder in DailyActivities)
        {
            if (dailyToOrder.OrderNumber != 0)
            {
                dailyArray[dailyToOrder.OrderNumber - 1] = dailyToOrder;
            }
        }

        DailyActivities = new List<Daily>(dailyArray);

        return DailyActivities;
    }

    public int GetCompletedTrainingDays()
    {
        int count = 0;
        for(int i = 0; i < dailyActivities.Count; i++)
        {
            if (dailyActivities[i].Completed)
            {
                count++;
            }
        }

        return count;
    }

    public bool GetCompletedRoutine()
    {
        int count = 0;
        for (int i = 0; i < dailyActivities.Count; i++)
        {
            if (dailyActivities[i].Completed)
            {
                count++;
            }
        }

        return count == dailyActivities.Count;
    }
}
