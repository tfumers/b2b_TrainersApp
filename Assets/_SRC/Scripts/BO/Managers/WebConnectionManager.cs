using com.TresToGames.TrainersApp.BO_SuperClasses;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebConnectionManager : Manager
{
    public const string BASE_URL = "http://localhost:8080/api/v1.0";
    public const string CURR_TRAINER_URL = "/trainers/current";

    public WebConnector webConnector;

    private Dictionary<string, string> UrlDirectory;

    public override void Initialize()
    {
        if (webConnector == null)
        {
            webConnector.GetComponentInChildren<WebConnector>();
        }
        webConnector.Initialize(this);


        UrlDirectory = new Dictionary<string, string>();
        //Se podrían cargar desde un archivo o una base de datos las url

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_LOGIN, BASE_URL + "/login");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_REGISTER, BASE_URL + "/trainers");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_ALL_CLIENTS, BASE_URL + "/clients");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_CLIENT_BY_ID, BASE_URL + "/clients");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_CREATE_NEW_ROUTINE, BASE_URL + "/routines");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_ROUTINE_BY_ID, BASE_URL + "/routines");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_CURRENT_TRAINER, BASE_URL + CURR_TRAINER_URL);

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_TCRS_BY_STATUS, BASE_URL + CURR_TRAINER_URL + "/trainerClientRelations/status");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_ALL_TCRS, BASE_URL + "");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_TCR_BY_ID, BASE_URL + "");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_CREATE_NEW_TRAINING, BASE_URL + "/trainings");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_TRAINING_BY_ID, BASE_URL + "/trainings");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_ALL_TRAININGS, BASE_URL + "");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_TRAINING_BY_PARAMETERS, BASE_URL + "/trainings/search");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_TRAININGS_BY_CATEGORY, BASE_URL + "");
        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_GET_TRAININGS_BY_DIFFICULTY, BASE_URL + "");

        UrlDirectory.Add(Constant.TRAINERS_KEYNAME_UPDATE_TRAINER_AVATAR, BASE_URL + CURR_TRAINER_URL + "/avatar");

        //Updates
        UrlDirectory.Add("updateSurveyInfo", BASE_URL + "/clients/current/surveyInfo");
        UrlDirectory.Add("updateLoginInfo", BASE_URL + "/clients/current/loginInfo");
        UrlDirectory.Add("updateClientAvatar", BASE_URL + "/clients/current/avatar");
        UrlDirectory.Add("updateCompletedDailyActivities", BASE_URL + "/clients/current/routines/daily");

        //Get
        UrlDirectory.Add("getRoutines", BASE_URL + "/clients/current/routines");
        UrlDirectory.Add("getCurrentClient", BASE_URL + "/clients/current");
        UrlDirectory.Add("getTrainers", BASE_URL + "/clients/current/availableTrainers");

        //Post
        UrlDirectory.Add("postTrainerClientRelation", BASE_URL + "/clients/current/newRelation");
        UrlDirectory.Add("postAndReturnRequiredTrainings", BASE_URL + "/clients/current/trainings");
        //throw new NotImplementedException();
    }

    public string GetUrl(string keyName)
    {
        string refVar = "";
        if (UrlDirectory.TryGetValue(keyName, out refVar))
        {
            
            return UrlDirectory[keyName];
        }
        else
        {
            return UrlDirectory[refVar];
        }
    }

    public async Task<bool> UserLogin(Dictionary<string, string> loginData)
    {
        Task<ResponseDTO> userLogin = this.ConnectionTask(UrlDirectory["login"], DictionaryToForm(loginData));

        await userLogin;

        return CheckHTTPStatusOk(userLogin.Result);
    }

    public async Task<bool> UserRegister(RegisterTrainerDTO registerClientDTO)
    {
        Task<ResponseDTO> userRegister = this.ConnectionTask(UrlDirectory["register"], registerClientDTO.ToForm());

        await userRegister;

        return CheckHTTPStatusOk(userRegister.Result);
    }


    //Updates
    public async Task<bool> UpdateUserSurveyInfo(EditRoutineOutDTO surveyInfoOutDTO)
    {

        Task<ResponseDTO> surveyInfo = this.ConnectionTask(UrlDirectory["updateSurveyInfo"], surveyInfoOutDTO.ToForm());

        await surveyInfo;

        return CheckHTTPStatusOk(surveyInfo.Result);
    }


    public async Task<bool> UpdateClientAvatar(UpdateAvatarOutDTO avatarOutDTO)
    {
        Task<ResponseDTO> postNewClientAvatar = this.ConnectionTask(UrlDirectory["updateClientAvatar"], avatarOutDTO.ToForm());

        await postNewClientAvatar;

        return CheckHTTPStatusOk(postNewClientAvatar.Result);
    }

    public async Task<ResponseDTO> UpdateCompletedDailyActivities(EditRoutineOutDTO completedActivityOutDTO)
    {
        Task<ResponseDTO> updateCompletedDailyActivity = this.ConnectionTask(UrlDirectory["updateCompletedDailyActivities"], completedActivityOutDTO.ToForm());

        await updateCompletedDailyActivity;

        return updateCompletedDailyActivity.Result;
    }

    //Get
    public async Task<ResponseDTO> GetRoutines()
    {
        Task<ResponseDTO> clientRoutines = this.ConnectionTask(UrlDirectory["getRoutines"], null);

        await clientRoutines;

        return clientRoutines.Result;
    }
    public async Task<ResponseDTO> GetCurrentClientInfo()
    {

        Task<ResponseDTO> getCurrentClient = this.ConnectionTask(UrlDirectory["getCurrentClient"], null);

        await getCurrentClient;

        return getCurrentClient.Result;
    }

    public async Task<ResponseDTO> GetTrainers()
    {

        Task<ResponseDTO> getTrainers = this.ConnectionTask(UrlDirectory["getTrainers"], null);

        await getTrainers;

        return getTrainers.Result;
    }

    //Post
    public async Task<bool> PostNewClientTrainerRelation(EditRoutineOutDTO newTrainerClientRelationOutDTO)
    {

        Task<ResponseDTO> postNewClientRelation = this.ConnectionTask(UrlDirectory["postTrainerClientRelation"], newTrainerClientRelationOutDTO.ToForm());

        await postNewClientRelation;

        return CheckHTTPStatusOk(postNewClientRelation.Result);
    }

    public async Task<ResponseDTO> PostAndReturnRequiredTrainings(EditRoutineOutDTO trainingIDsOutDTO)
    {

        Task<ResponseDTO> postAndReturnRequiredTrainings = this.ConnectionTask(UrlDirectory["postAndReturnRequiredTrainings"], trainingIDsOutDTO.ToForm());

        await postAndReturnRequiredTrainings;

        return postAndReturnRequiredTrainings.Result;
    }

    protected async Task<ResponseDTO> ConnectionTask(string url, WWWForm form)
    {
        bool IsDone = false;
        ResponseDTO newResponse = new ResponseDTO();

        Action<ResponseDTO> ConnectionCallback = (returnedResponse) =>
        {
            newResponse = returnedResponse;
            //Debug.Log("desde el callback: " + response);//
            IsDone = true;
        };
        if (form!=null)
        {

            StartCoroutine(PostCoroutine(url, form, ConnectionCallback));
        }
        else
        {
            StartCoroutine(GetCoroutine(url, ConnectionCallback));
        }


        while (!IsDone)
        {
            await Task.Yield();

        }

        //Debug.Log("salio del bucle, retorno una response = " + response.ToString());//
        return newResponse;
    }

    protected IEnumerator PostCoroutine(string url, WWWForm form, Action<ResponseDTO> responseCallback)
    {

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ResponseDTO newResponse;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            newResponse = new ResponseDTO(www.responseCode, "unexpected error");
        }
        else
        {
            newResponse = new ResponseDTO(www.responseCode, www.downloadHandler.text);
        }

        responseCallback(newResponse);
    }

    protected IEnumerator GetCoroutine(string url, Action<ResponseDTO> responseCallback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        ResponseDTO newResponse;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            newResponse = new ResponseDTO(www.responseCode, "unexpected error");
        }
        else
        {
            newResponse = new ResponseDTO(www.responseCode, www.downloadHandler.text);
        }

        responseCallback(newResponse);
    }

    protected WWWForm DictionaryToForm(Dictionary<string, string> data)
    {
        WWWForm form = new WWWForm();
        foreach (var item in data)
        {
            form.AddField(item.Key, item.Value);
        }
        return form;
    }

    private bool CheckHTTPStatusOk(ResponseDTO response)
    {
        if (response.HttpStatus == 200) {
            return true;
        }

        return false;
    }

}

