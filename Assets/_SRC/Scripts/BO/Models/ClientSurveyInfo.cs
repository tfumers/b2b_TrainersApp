using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSurveyInfo
{
    private string sex;

    private string birthDate;

    private double height;

    private double starterWeight;

    private bool pregnancy;

    private string trainingOrSportsRecord;

    private string availableTrainingItems;

    private string trainingObjectives;

    private string illnesses;

    private string wounds;

    private string availableTrainingDays;

    public ClientSurveyInfo()
    {
        this.sex = "X";
        this.height = 0;
        this.starterWeight = 0;
        this.pregnancy = false;
        this.trainingOrSportsRecord = "";
        this.availableTrainingItems = "";
        this.trainingObjectives = "";
        this.illnesses = "";
        this.availableTrainingDays = "";
    }

    public ClientSurveyInfo(string sex, double height, double starterWeight, bool pregnancy, string trainingOrSportsRecord, string availableTrainingItems, string trainingObjectives, string illnesses, string availableTrainingDays, string birthDate, string wounds)
    {
        this.sex = sex;
        this.birthDate = birthDate;
        this.height = height;
        this.starterWeight = starterWeight;
        this.pregnancy = pregnancy;
        this.trainingOrSportsRecord = trainingOrSportsRecord;
        this.availableTrainingItems = availableTrainingItems;
        this.trainingObjectives = trainingObjectives;
        this.illnesses = illnesses;
        this.wounds = wounds;
        this.availableTrainingDays = availableTrainingDays;
        
    }

    public string Sex { get => sex; set => sex = value; }
    public double Height { get => height; set => height = value; }
    public double StarterWeight { get => starterWeight; set => starterWeight = value; }
    public bool Pregnancy { get => pregnancy; set => pregnancy = value; }
    public string TrainingOrSportsRecord { get => trainingOrSportsRecord; set => trainingOrSportsRecord = value; }
    public string AvailableTrainingItems { get => availableTrainingItems; set => availableTrainingItems = value; }
    public string TrainingObjectives { get => trainingObjectives; set => trainingObjectives = value; }
    public string Illnesses { get => illnesses; set => illnesses = value; }
    public string AvailableTrainingDays { get => availableTrainingDays; set => availableTrainingDays = value; }
    public string BirthDate { get => birthDate; set => birthDate = value; }
    public string Wounds { get => wounds; set => wounds = value; }
}
