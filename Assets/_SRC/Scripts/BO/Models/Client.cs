using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Entity
{
    private long id;

    private long status;

    private string username;

    private string firstname;

    private string lastname;

    private string nationality;

    private DateTime birthDate;

    private int phone;

    private string sex;

    private string icon;

    private int dni;

    private long level;

    private long experience;

    private string avatar;

    private Avatar avatarImage;

    private int height;

    private int starterWeight;

    private bool pregnancy;

    private string trainingOrSportsRecord;

    private string availableTrainingItems;

    private string trainingObjectives;

    private string illnesses;

    private string wounds;

    private string availableTrainingDays;

    public Client()
    {
        this.id = 0;
    }
    /*
    public Client(long id, long status, string username, string firstname, string lastname, string nationality, int phone, string sex, string icon, int dni, long level, long experience, string avatar, int height, int starterWeight, bool pregnancy, string trainingOrSportsRecord, string availableTrainingItems, string trainingObjectives, string illnesses, string availableTrainingDays)
    {
        this.id = id;
        this.status = status;
        this.username = username;
        this.firstname = firstname;
        this.lastname = lastname;
        this.nationality = nationality;
        this.phone = phone;
        this.sex = sex;
        this.icon = icon;
        this.dni = dni;
        this.level = level;
        this.experience = experience;
        this.avatar = avatar;
        this.height = height;
        this.starterWeight = starterWeight;
        this.pregnancy = pregnancy;
        this.trainingOrSportsRecord = trainingOrSportsRecord;
        this.availableTrainingItems = availableTrainingItems;
        this.trainingObjectives = trainingObjectives;
        this.illnesses = illnesses;
        this.availableTrainingDays = availableTrainingDays;
    }
    */
    public Client(SimpleJSON.JSONObject json)
    {
        this.id = json["id"];
        this.status = json["status"];
        this.username = json["username"];
        this.firstname = json["firstname"];
        this.lastname = json["lastname"];
        this.nationality = json["nationality"]; 
        this.phone = json["phone"]; 
        this.sex = json["sex"]; 
        this.icon = json["icon"]; 
        this.dni = json["dni"]; 
        this.level = json["level"]; 
        this.experience = json["experience"]; 
        this.avatar = json["avatar"];
        this.avatarImage = new Avatar();
        this.height = json["height"]; 
        this.starterWeight = json["starterWeight"]; 
        this.pregnancy = json["pregnancy"]; 
        this.trainingOrSportsRecord = json["trainingOrSportsRecord"]; 
        this.availableTrainingItems = json["availableTrainingItems"]; 
        this.trainingObjectives = json["trainingObjectives"]; 
        this.illnesses = json["illnesses"]; 
        this.availableTrainingDays = json["availableTrainingDays"]; 
    }

    public long Id { get => id; set => id = value; }
    public long Status { get => status; set => status = value; }
    public string Username { get => username; set => username = value; }
    public string Firstname { get => firstname; set => firstname = value; }
    public string Lastname { get => lastname; set => lastname = value; }
    public string Nationality { get => nationality; set => nationality = value; }
    public int Phone { get => phone; set => phone = value; }
    public string Sex { get => sex; set => sex = value; }
    public string Icon { get => icon; set => icon = value; }
    public int Dni { get => dni; set => dni = value; }
    public long Level { get => level; set => level = value; }
    public long Experience { get => experience; set => experience = value; }
    public string Avatar { get => avatar; set => avatar = value; }
    public int Height { get => height; set => height = value; }
    public int StarterWeight { get => starterWeight; set => starterWeight = value; }
    public bool Pregnancy { get => pregnancy; set => pregnancy = value; }
    public string TrainingOrSportsRecord { get => trainingOrSportsRecord; set => trainingOrSportsRecord = value; }
    public string AvailableTrainingItems { get => availableTrainingItems; set => availableTrainingItems = value; }
    public string TrainingObjectives { get => trainingObjectives; set => trainingObjectives = value; }
    public string Illnesses { get => illnesses; set => illnesses = value; }
    public string AvailableTrainingDays { get => availableTrainingDays; set => availableTrainingDays = value; }
    public Avatar AvatarImage { get => avatarImage; set => avatarImage = value; }
    public DateTime BirthDate { get => birthDate; set => birthDate = value; }
    public string Wounds { get => wounds; set => wounds = value; }
}
