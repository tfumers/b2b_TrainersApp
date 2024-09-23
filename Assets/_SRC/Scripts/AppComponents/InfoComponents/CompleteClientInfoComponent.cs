using com.TresToGames.TrainersApp.Utils.AppComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteClientInfoComponent : AppComponent<TrainerClientRelation>
{
    [SerializeField]AvatarContainerComponent avatarContainer;

    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtAge;
    [SerializeField] TMPro.TextMeshProUGUI txtSex;
    [SerializeField] TMPro.TextMeshProUGUI txtHeight;
    [SerializeField] TMPro.TextMeshProUGUI txtWeight;
    [SerializeField] TMPro.TextMeshProUGUI txtPregnancy;

    [SerializeField] DescriptionComponent trainingObjectives;
    [SerializeField] DescriptionComponent trainingOrSportRecord;
    [SerializeField] DescriptionComponent availableTrainingItems;
    [SerializeField] DescriptionComponent illnesses;
    [SerializeField] DescriptionComponent wounds;
    [SerializeField] DescriptionComponent availableTrainingDays;

    protected override void Prepare(TrainerClientRelation model)
    {
        bool condition = model.Client.Pregnancy;
        string stringCondition = condition ? "Si" : "No";

        avatarContainer.LoadComponent(model.Client.AvatarImage);

        txtName.text = model.Client.Firstname + " " + model.Client.Lastname;
        if(model.Client.BirthDate != null)
        {
            txtAge.gameObject.SetActive(true);
            txtAge.text = "Edad: 23";
        }
        else
        {
            txtAge.gameObject.SetActive(false);
        }

        txtSex.text = "Sexo: " + model.Client.Sex;
        txtHeight.text = "Altura: " + model.Client.Height + " cm.";
        txtWeight.text = "Peso: " + model.Client.StarterWeight + " kg.";
        txtPregnancy.text = "¿Transita un embarazo? " + stringCondition;

        //Objetivos de entrenamiento que se propuso el cliente

        trainingObjectives.LoadComponent(model.Objective);
        trainingOrSportRecord.LoadComponent(model.Client.TrainingOrSportsRecord);
        availableTrainingItems.LoadComponent(model.Client.AvailableTrainingItems);
        illnesses.LoadComponent(model.Client.Illnesses);
        wounds.LoadComponent(model.Client.Wounds);
        availableTrainingDays.LoadComponent(model.Client.AvailableTrainingDays);

    }
}
