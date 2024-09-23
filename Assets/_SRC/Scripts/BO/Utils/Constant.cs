using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const int CLIENT_STATUS_NO_SURVEY = 1;
    public const int CLIENT_STATUS_NO_ROUTINE = 2;
    public const int CLIENT_STATUS_WAITING = 3;
    public const int CLIENT_STATUS_HAS_ROUTINE = 4;
    public const int CLIENT_STATUS_BANNED = 5;

    public const int ACTIVITY_TYPE_REPS = 1;
    public const int ACTIVITY_TYPE_TIMER = 2;
    public static readonly DateTime DefaultDateTime = DateTime.ParseExact("1998-10-11", "yyyy-mm-dd", null);

    public const string DEFAULT_SIMPLE_API_DATE_FORMAT = "yyyy-MM-dd";
    public const string DEFAULT_COMPLETED_API_DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss";

    public const string DEFAULT_APP_DATE_FORMAT_DAY_OF_THE_WEEK = "dddd";

    public const string DEFAULT_APP_DATE_FORMAT_DAY_N_MONTH = "dddd, dd MMM";

    public const string DEFAULT_APP_DATE_FORMAT_SIMPLIFIED = "dd-MM-yyyy";

    public const string DEFAULT_APP_DATE_FORMAT_COMPLETED = "dddd, dd MMMM yyyy";

    public const int TRAINER_CLIENT_RELATION_STATUS_PENDING = 1;
    public const int TRAINER_CLIENT_RELATION_STATUS_ACCEPTED = 2;
    public const int TRAINER_CLIENT_RELATION_STATUS_CANCELLED = 3;

    public const string TRAINERS_KEYNAME_LOGIN = "login";
    public const string TRAINERS_KEYNAME_REGISTER = "register";

    public const string TRAINERS_KEYNAME_CURRENT_TRAINER = "currentTrainer";

    public const string TRAINERS_KEYNAME_GET_TRAINER_BY_ID = "getTrainerById";
    public const string TRAINERS_KEYNAME_GET_ALL_TRAINERS = "getAllTrainers";

    public const string TRAINERS_KEYNAME_GET_CLIENT_BY_ID = "getClientById";
    public const string TRAINERS_KEYNAME_GET_ALL_CLIENTS = "getAllClients";

    public const string TRAINERS_KEYNAME_CREATE_NEW_ROUTINE = "newRoutine";

    public const string TRAINERS_KEYNAME_GET_ROUTINE_BY_ID = "getRoutineById";
    public const string TRAINERS_KEYNAME_GET_ALL_ROUTINES = "getAllRoutines";

    public const string TRAINERS_KEYNAME_CREATE_NEW_TRAINING = "newTraining";

    public const string TRAINERS_KEYNAME_GET_TRAINING_BY_ID = "getTrainingById";
    public const string TRAINERS_KEYNAME_GET_ALL_TRAININGS = "getAllTrainings";

    public const string TRAINERS_KEYNAME_TRAINING_BY_PARAMETERS = "getTrainingByParameters";

    public const string TRAINERS_KEYNAME_GET_TRAININGS_BY_CATEGORY = "getTrainingsByCategory";
    public const string TRAINERS_KEYNAME_GET_TRAININGS_BY_DIFFICULTY = "getTrainingsByDificulty";

    public const string TRAINERS_KEYNAME_GET_TCR_BY_ID = "getTrainerClientRelationById";
    public const string TRAINERS_KEYNAME_GET_ALL_TCRS = "getAllTrainerClientRelations";
    public const string TRAINERS_KEYNAME_GET_TCRS_BY_STATUS = "getTrainerClientRelationsByStatus";

    public const string TRAINERS_KEYNAME_UPDATE_TRAINER_INFO = "updateTrainerInfo";
    public const string TRAINERS_KEYNAME_UPDATE_TRAINER_AVATAR = "updateTrainerAvatar";
    /*public readonly string TRAINERSAPP_KEY_REGISTER = "";
    public readonly string TRAINERSAPP_KEY_REGISTER = "";
    public readonly string TRAINERSAPP_KEY_REGISTER = "";
    public readonly string TRAINERSAPP_KEY_REGISTER = "";*/
}
