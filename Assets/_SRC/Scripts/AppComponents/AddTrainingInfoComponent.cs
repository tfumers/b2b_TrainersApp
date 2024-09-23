using com.TresToGames.TrainersApp.Utils.AppComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddTrainingInfoComponent : AppComponent<AddTrainingInfoComponentConfigurations>
{
    [SerializeField] TMPro.TMP_InputField inputTrainingName;
    [SerializeField] TMPro.TMP_InputField inputTrainingDescription;
    [SerializeField] TMPro.TMP_Dropdown dropdownCategories;
    [SerializeField] TMPro.TMP_Dropdown dropdownDifficulty;
    [SerializeField] TMPro.TMP_InputField inputEstTimePerRep;
    [SerializeField] TMPro.TMP_InputField inputEstCalPerRep;
    [SerializeField] TMPro.TMP_Dropdown dropdownTrainingVideo;
    [SerializeField] TMPro.TMP_Dropdown dropdownTrainingImage;

    [SerializeField] Button createTrainingButton;
    [SerializeField] Button backButton;
    [SerializeField] Button cancelTrainingCreationButton;

    Training trainingToCreate;

    protected override void Prepare(AddTrainingInfoComponentConfigurations model)
    {        
        InitializeTraining(model.TrainingToEdit);
        ConfigureInputTrainingName(inputTrainingName);
        ConfigureInputDescription(inputTrainingDescription);
        ConfigureDropDownCategories(dropdownCategories);
        ConfigureDropDownDifficulty(dropdownDifficulty);
        ConfigureInputEstTimePerRep(inputEstTimePerRep);
        ConfigureInputEstCalPerRep(inputEstCalPerRep);
        if (!ConfigureTrainingVideoDropDown(dropdownTrainingVideo, model.TrainingVideos))
        {
            Debug.LogError("No cargaron training videos");
        }
        if (!LoadTrainingImageDropDown(dropdownTrainingImage, model.TrainingImages))
        {
            Debug.LogError("No cargaron training images");
        }
        ConfigureCreateTrainingButton(createTrainingButton, model.OnCreateTrainingButton,model.OnCannotCreateTraining);
        ConfigureCancelCreationBackButton(backButton, model.OnCancelTrainingCreation);
        ConfigureCancelCreationBackButton(cancelTrainingCreationButton, model.OnCancelTrainingCreation);
    }

    private void InitializeTraining(Training receivedTraining)
    {
        if (receivedTraining == null)
        {
            trainingToCreate = new Training();
        }
        else
        {
            trainingToCreate = receivedTraining;
        }
    }

    private void ConfigureInputTrainingName(TMPro.TMP_InputField inputField)
    {
        inputField.onValueChanged.RemoveAllListeners();

        if (trainingToCreate.Id != 0)
        {
            inputField.text = trainingToCreate.Name;
        }
        else
        {
            inputField.text = "";
        }

        inputField.onValueChanged.AddListener((str) => trainingToCreate.Name = str);
    }

    private void ConfigureInputDescription(TMPro.TMP_InputField inputField)
    {
        inputField.onValueChanged.RemoveAllListeners();

        if (trainingToCreate.Id != 0)
        {
            inputField.text = trainingToCreate.Description;
        }
        else
        {
            inputField.text = "";
        }

        inputField.onValueChanged.AddListener((str) => trainingToCreate.Description = str);
    }

    private void ConfigureDropDownCategories(TMPro.TMP_Dropdown dropdown)
    {
        List<TMPro.TMP_Dropdown.OptionData> optionsToAdd = new List<TMPro.TMP_Dropdown.OptionData>();
        dropdown.ClearOptions();
        dropdown.onValueChanged.RemoveAllListeners();

        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Sin Categoria"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Brazos"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Core"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Piernas"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Cuerpo Completo"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Crossfit"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Calistenia"));

        dropdown.AddOptions(optionsToAdd);
        dropdown.value = 0;

        if (trainingToCreate.Id != 0)
        {
            for(int i = 0; i < dropdown.options.Count; i++)
            {
                if (trainingToCreate.Categories[0].Equals(dropdown.options[i].text))
                {
                    dropdown.value = i;
                    break;
                }
            }
        }

        dropdown.onValueChanged.AddListener((intVal) => trainingToCreate.SetCategory(dropdown.options[intVal].text));
    }

    private void ConfigureDropDownDifficulty(TMPro.TMP_Dropdown dropdown)
    {
        List<TMPro.TMP_Dropdown.OptionData> optionsToAdd = new List<TMPro.TMP_Dropdown.OptionData>();
        dropdown.ClearOptions();
        dropdown.onValueChanged.RemoveAllListeners();

        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Inicial"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Avanzado"));
        optionsToAdd.Add(new TMPro.TMP_Dropdown.OptionData("Difícil"));

        dropdown.AddOptions(optionsToAdd);
        dropdown.value = 0;

        if (trainingToCreate.Id != 0)
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (trainingToCreate.Difficulty.Equals(dropdown.options[i].text))
                {
                    dropdown.value = i;
                    break;
                }
            }
        }

        dropdown.onValueChanged.AddListener((intVal) => trainingToCreate.Difficulty = dropdown.options[intVal].text);
    }

    private void ConfigureInputEstTimePerRep(TMPro.TMP_InputField inputField)
    {
        inputField.onValueChanged.RemoveAllListeners();
        inputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

        if (trainingToCreate.Id != 0)
        {
            inputField.text = trainingToCreate.EstTimePerRep.ToString();
        }
        else
        {
            inputField.text = "";
        }

        inputField.onValueChanged.AddListener((str) => trainingToCreate.EstTimePerRep = int.Parse(str));
    }

    private void ConfigureInputEstCalPerRep(TMPro.TMP_InputField inputField)
    {
        inputField.onValueChanged.RemoveAllListeners();
        inputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

        if (trainingToCreate.Id != 0)
        {
            inputField.text = trainingToCreate.EstCaloriesPerRep.ToString();
        }
        else
        {
            inputField.text = "";
        }

        inputField.onValueChanged.AddListener((str) => trainingToCreate.EstCaloriesPerRep = int.Parse(str));
    }

    private bool ConfigureTrainingVideoDropDown(TMPro.TMP_Dropdown dropdown, List<TrainingVideo> trainingVideos)
    {
        List<TMPro.TMP_Dropdown.OptionData> optionsToAdd = new List<TMPro.TMP_Dropdown.OptionData>();

        dropdown.ClearOptions();
        dropdown.onValueChanged.RemoveAllListeners();

        foreach (TrainingVideo tv in trainingVideos)
        {
            TMPro.TMP_Dropdown.OptionData optionData = new TMPro.TMP_Dropdown.OptionData(tv.AnimatorController.name);
            optionsToAdd.Add(optionData);
        }

        dropdown.AddOptions(optionsToAdd);
        dropdown.value = 0;
        trainingToCreate.VideoUrl = dropdown.options[0].text;

        if (trainingToCreate.Id != 0)
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (trainingToCreate.VideoUrl.Equals(dropdown.options[i].text))
                {
                    dropdown.value = i;
                    break;
                }
            }
        }


        dropdown.onValueChanged.AddListener((intVal) => {
            trainingToCreate.VideoUrl = dropdown.options[intVal].text;
            Debug.Log(trainingToCreate.VideoUrl);
        });


        return true;
    }

    private bool LoadTrainingImageDropDown(TMPro.TMP_Dropdown dropdown, List<TrainingImage> trainingImages)
    {
        List<TMPro.TMP_Dropdown.OptionData> optionsToAdd = new List<TMPro.TMP_Dropdown.OptionData>();

        dropdown.ClearOptions();
        dropdown.onValueChanged.RemoveAllListeners();

        foreach (TrainingImage ti in trainingImages)
        {
            TMPro.TMP_Dropdown.OptionData optionData = new TMPro.TMP_Dropdown.OptionData(ti.Name, ti.Image);
            optionsToAdd.Add(optionData);
        }

        dropdown.AddOptions(optionsToAdd);
        dropdown.value = 0;
        trainingToCreate.ImageUrl = dropdown.options[0].text;

        if (trainingToCreate.Id != 0)
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (trainingToCreate.ImageUrl.Equals(dropdown.options[i].text))
                {
                    dropdown.value = i;
                    break;
                }
            }
        }


        dropdown.onValueChanged.AddListener((intVal) => {
            trainingToCreate.ImageUrl = dropdown.options[intVal].text;
            Debug.Log(trainingToCreate.ImageUrl);
        });

        return true;
    }

    private void ConfigureCreateTrainingButton(Button button, UnityEvent<Training> onCreateTrainingButton, UnityEvent onCannotCreateEvent)
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => {
            if (CheckFieldCompletion()) 
            {
                onCreateTrainingButton.Invoke(trainingToCreate);
            }
            else
            {
                onCannotCreateEvent.Invoke();
            }
        });
    }

    private void ConfigureCancelCreationBackButton(Button button, UnityEvent onCancelTrainingCreation)
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => onCancelTrainingCreation.Invoke());
    }

    private bool CheckFieldCompletion()
    {
        if (trainingToCreate.Name.Length <= 0)
        {
            return false;
        }

        if (trainingToCreate.Description.Length <= 0)
        {
            return false;
        }

        if (trainingToCreate.Difficulty.Length <= 0)
        {
            return false;
        }

        if (trainingToCreate.EstTimePerRep <= 0)
        {
            return false;
        }

        if (trainingToCreate.EstCaloriesPerRep <= 0)
        {
            return false;
        }

        if(trainingToCreate.Categories.Length == 0)
        {
            return false;
        }

        if (trainingToCreate.ImageUrl.Length <= 0)
        {
            return false;
        }

        if (trainingToCreate.VideoUrl.Length <= 0)
        {
            return false;
        }

        return true;
    }
}

public class AddTrainingInfoComponentConfigurations
{
    Training trainingToEdit;

    List<TrainingImage> trainingImages;

    List<TrainingVideo> trainingVideos;

    UnityEvent<Training> onCreateTrainingButton;

    UnityEvent onCancelTrainingCreation;

    UnityEvent onCannotCreateTraining;

    public AddTrainingInfoComponentConfigurations(Training training, List<TrainingImage> trainingImages, List<TrainingVideo> trainingVideos, UnityEvent<Training> OnCreateTrainingButtonPressed, UnityEvent OnCancelTrainingCreatonButtonPressed, UnityEvent OnCannotCreateTraining)
    {
        this.trainingToEdit = training;
        this.trainingImages = trainingImages;
        this.trainingVideos = trainingVideos;
        this.onCreateTrainingButton = OnCreateTrainingButtonPressed;
        this.onCancelTrainingCreation = OnCancelTrainingCreatonButtonPressed;
        this.onCannotCreateTraining = OnCannotCreateTraining;
    }
    public Training TrainingToEdit { get => trainingToEdit;}
    public List<TrainingImage> TrainingImages { get => trainingImages;}
    public List<TrainingVideo> TrainingVideos { get => trainingVideos;}
    public UnityEvent<Training> OnCreateTrainingButton { get => onCreateTrainingButton; set => onCreateTrainingButton = value; }
    public UnityEvent OnCancelTrainingCreation { get => onCancelTrainingCreation; set => onCancelTrainingCreation = value; }
    public UnityEvent OnCannotCreateTraining { get => onCannotCreateTraining; set => onCannotCreateTraining = value; }
}
