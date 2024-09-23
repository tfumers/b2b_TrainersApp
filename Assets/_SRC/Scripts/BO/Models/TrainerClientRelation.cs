using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerClientRelation
{
    private long id;

    private Trainer trainer;

    private string trainerId;

    private Client client;

    private Routine routine;

    private string status;

    private string objective;

    private string description;

    private DateTime createdAt;

    private DateTime updatedAt;

    public TrainerClientRelation()
    {
    }

    public TrainerClientRelation(long id, Client client, Routine routine, string status, string objective, string description, DateTime createdAt, DateTime updatedAt)
    {
        this.id = id;
        //this.trainer = trainer;
        this.client = client;
        this.routine = routine;
        this.status = status;
        this.objective = objective;
        this.description = description;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }

    public long Id { get => id; set => id = value; }
    public Trainer Trainer { get => trainer; set => trainer = value; }
    public Client Client { get => client; set => client = value; }
    public Routine Routine { get => routine; set => routine = value; }
    public string Status { get => status; set => status = value; }
    public string Objective { get => objective; set => objective = value; }
    public string Description { get => description; set => description = value; }
    public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
}
