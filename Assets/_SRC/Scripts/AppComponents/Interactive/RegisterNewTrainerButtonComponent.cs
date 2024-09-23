using com.TresToGames.TrainersApp.Utils.AppComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RegisterNewTrainerButtonComponent : InteractiveAppComponent<Dictionary<string, string>>
{
    [SerializeField] TMPro.TMP_InputField firstname;
    [SerializeField] TMPro.TMP_InputField lastname;
    [SerializeField] TMPro.TMP_InputField username;
    [SerializeField] TMPro.TMP_InputField description;
    [SerializeField] TMPro.TMP_InputField phone;
    [SerializeField] TMPro.TMP_InputField birthdate;
    [SerializeField] TMPro.TMP_InputField email;
    [SerializeField] TMPro.TMP_InputField password;

    public Button RegisterButton { get => button; }

    protected override void Prepare(Dictionary<string, string> model)
    {
        ClearTextAndPrepareListener(firstname, "firstname");
        ClearTextAndPrepareListener(lastname, "lastname");
        ClearTextAndPrepareListener(username, "username");
        ClearTextAndPrepareListener(description, "description");
        ClearTextAndPrepareListener(phone, "phone");
        ClearTextAndPrepareListener(birthdate, "birthDate");
        ClearTextAndPrepareListener(email, "email");
        ClearTextAndPrepareListener(password, "passTest");
    }

    private void ClearTextAndPrepareListener(TMPro.TMP_InputField inputField, string key)
    {
        inputField.onEndEdit.RemoveAllListeners();

        inputField.text = "";
        SaveInDictionary(key, inputField.text);


        inputField.onEndEdit.AddListener((str) => {
            SaveInDictionary(key, str);
            CheckAllInputFields();
            });
    }

    private void CheckAllInputFields()
    {
        int count = 0;
        count += CheckSingleInputField(firstname);
        count += CheckSingleInputField(lastname);
        count += CheckSingleInputField(username);
        count += CheckSingleInputField(description);
        count += CheckSingleInputField(phone);
        count += CheckSingleInputField(birthdate);
        count += CheckSingleInputField(password);
        count += CheckSingleInputField(email);

        if (count == 8)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

    }

    private int CheckSingleInputField(TMPro.TMP_InputField inputField)
    {
        if (inputField.text.Length > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void SaveInDictionary(string keyName, string value)
    {
        if (model == null)
        {
            model = new Dictionary<string, string>();
        }

        if (model.ContainsKey(keyName))
        {
            model[keyName] = value;
            Debug.Log("saved " + value + " at " + keyName);
        }
        else
        {
            try
            {
                model.Add(keyName, value);
                Debug.Log("saved " + value + " at " + keyName);
            }
            catch (Exception e)
            {
                Debug.LogError("Excepcion: la key es invalida " + e);
            }
        }
    }
}
