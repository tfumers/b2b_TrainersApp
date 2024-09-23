using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using UnityEngine.Events;

public class LoginMenuView : MenuView
{
    public UnityEvent OnLoginSuccess = new UnityEvent();

    public UnityEvent<string> OnLoginFailed = new UnityEvent<string>();

    CurrentTrainerController currentTrainerController;

    Dictionary<string, string> DictionaryInputLoginInfo = new Dictionary<string, string>();

    string selectedInputKey = "someValue";

    public override Task<bool> InitializeReferences()
    {
        currentTrainerController = B2BTrainer.Instance.controllerManager.currentTrainerController;

        //authController = B2BTrainer.Instance.controllerManager.authController;

        //Acá se podría recurrir a un autoLogin


        return Task.FromResult(true);
    }


    public async void TryLoginUser()
    {
        
        /*
        foreach (var keyValue in DictionaryInputLoginInfo)
        {
            if (!RequiredFormFields.Contains(keyValue.Key))
            {
                DictionaryInputLoginInfo.Remove(keyValue.Key);
            }
        }*/
        
        
        //Task<bool> loginTask = null;// authController.LoginTask(DictionaryInputLoginInfo);
        //esperamos el resultado del task
        Task<bool> loginTask = currentTrainerController.LoginTrainer(DictionaryInputLoginInfo);
        Debug.Log("Task Iniciado");

        await loginTask;
        //acá obtenemos el resultado del task


        
        if(loginTask.Result)
        {
            OnLoginSuccess.Invoke();
        }
        else
        {
            OnLoginFailed.Invoke("");
        }
        
    }

    public void ChangeInputKey(string currentKey)
    {
        selectedInputKey = currentKey;
    }

    public void SaveInputValue(string value)
    {
        if (DictionaryInputLoginInfo == null)
        {
            DictionaryInputLoginInfo = new Dictionary<string, string>();
        }

        if (DictionaryInputLoginInfo.ContainsKey(selectedInputKey))
        {
            DictionaryInputLoginInfo[selectedInputKey] = value;
        }
        else
        {
            try
            {
                DictionaryInputLoginInfo.Add(selectedInputKey, value);
            }
            catch (Exception e)
            {
                Debug.LogError("Excepcion: la key es invalida " + e);
            }
        }
    }
}
