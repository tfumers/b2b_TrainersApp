using com.TresToGames.TrainersApp.BO_SuperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotificationManager : Manager
{
    ViewManager viewManager;

    static GenericMessage emergentMessage;
    public static GenericMessage EmergentMessage { get => emergentMessage; set => emergentMessage = value; }

    public override void Initialize()
    {
        viewManager = B2BTrainer.Instance.viewManager;

    }

    public void Success(UnityEvent onAcceptButton)
    {
        string message = "¡La operación se completó con éxito!";

        onAcceptButton.AddListener(() => TurnNotificationEmergentOff());

        EmergentMessage = new GenericMessage("¡Exito!", message, "Aceptar", "Cancelar", false, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void Failed(string opMessage, UnityEvent onAcceptButton)
    {
        string message = "La operación se completó no se completo. Vuelva a intentarlo." + opMessage;

        onAcceptButton.AddListener(() => TurnNotificationEmergentOff());

        EmergentMessage = new GenericMessage("¡Algo salio mal!", message, "Aceptar", "Cancelar", false, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void DeleteConfirmation(string text, UnityEvent onAcceptButton)
    {
        string message = "Está por cancelar la edición de su "+ text +". Los cambios realizados se perderán. ¿Estás seguro que deseas hacerlo?";
        EmergentMessage = new GenericMessage("¡Atención!", message, "Eliminar", "Cancelar", true, onAcceptButton, null);
        TurnNotificationEmergentOn();
    }

    public void LoginFailed(string message)
    {
        EmergentMessage = new GenericMessage("Login Failed", message);
        TurnNotificationEmergentOn();
    }

    public void RegisterFailed(string message)
    {
        EmergentMessage = new GenericMessage("Register Failed", message);
        TurnNotificationEmergentOn();
    }

    public void UserDontHaveStatus(int status)
    {
        string message = "No puedes realizar esa acción: ";

        switch (status)
        {
            case Constant.CLIENT_STATUS_NO_SURVEY:
                message += "Antes necesitas completar la encuesta.";
                break;
            case Constant.CLIENT_STATUS_NO_ROUTINE:
                message += "Primero necesitas elegir un personal trainer.";
                break;
            case Constant.CLIENT_STATUS_WAITING:
                message += "Necesitas esperar tu rutina.";
                break;
            case Constant.CLIENT_STATUS_BANNED:
                message += "No puedes usar la aplicación hasta nuevo aviso.";
                break;
            default:
            case 0:
                    break;
        }

        EmergentMessage = new GenericMessage("Atención:", "");
        TurnNotificationEmergentOn();
    }

    public void NotClientLoggin()
    {
        EmergentMessage = new GenericMessage("Error", "");
        TurnNotificationEmergentOn();
    }

    public void GenericNotification(string arg)
    {
        EmergentMessage = new GenericMessage("¡Atención!", arg);
        TurnNotificationEmergentOn();
    }

    public void GenericError(string arg)
    {
        EmergentMessage = new GenericMessage("Error", arg);
        TurnNotificationEmergentOn();
    }

    private void NewErrorEmergent(string arg)
    {
        Debug.Log("Desde el error manager: " + arg);
    }

    private void TurnNotificationEmergentOff()
    {
        Debug.Log("Emergent called");
        viewManager.TurnEmergentOff(viewManager.notifactionEmergentView);
    }

    private void TurnNotificationEmergentOn()
    {
        Debug.Log("Emergent called");
        viewManager.TurnEmergentOn(viewManager.notifactionEmergentView);
    }
}
