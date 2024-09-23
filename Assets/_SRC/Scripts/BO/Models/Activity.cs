using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activity
{
    long id;

    long orderNumber;

    private Training training;

    private int trainingId;

    private int actTypeId;

    private int typeValue;

    private int elapsedTime;

    private int estimatedTime;

    private bool completed = false;

    public Activity()
    {
        this.actTypeId = 0;
        this.typeValue = 0;
        this.training = new Training();
    }

    public Activity(JSONObject json) : base()
    {
        this.id = json["id"];
        this.trainingId = json["trainingId"];
        this.orderNumber = json["orderNumber"];
        this.actTypeId = json["actTypeId"];
        this.typeValue = json["actTypeValue"];
    }

    public bool CheckActivityToEditStatus()
    {
        return ((this.training.Id != 0) && ((this.actTypeId == Constant.ACTIVITY_TYPE_REPS || (this.actTypeId == Constant.ACTIVITY_TYPE_TIMER)) && (this.typeValue > 0)));
    }

    public int TrainingId { get => trainingId; set => trainingId = value; }
    public int ActTypeId { get => actTypeId; set => actTypeId = value; }
    public int TypeValue { get => typeValue; set => typeValue = value; }
    public Training Training { get => training; set => training = value; }
    public bool Completed { get => completed; 
                            set => completed = value; }
    public int ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
    public long OrderNumber { get => orderNumber; set => orderNumber = value; }
    public long Id { get => id; set => id = value; }
    public int EstimatedTime { get => estimatedTime; set => estimatedTime = value; }

    public void SetTypeValue(string value)
    {
        try
        {
            typeValue = int.Parse(value);
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't parse");
        }
    }

    public void SetOrderNumber(string value)
    {
        try
        {
            orderNumber = int.Parse(value);
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't parse");
        }
    }
}
