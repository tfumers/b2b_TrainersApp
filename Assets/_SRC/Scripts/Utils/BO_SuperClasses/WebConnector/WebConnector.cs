using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public class WebConnector : MonoBehaviour
    {
        WebConnectionManager webConnectionManager;

        public void Initialize(WebConnectionManager webConnection)
        {
            webConnectionManager = webConnection;
        }

        public async Task<ConnectorInResponse> PostConnection(ConnectorOutMessage<OutDTO> connectorOut)
        {
            bool IsDone = false;
            ConnectorInResponse newResponse = new ConnectorInResponse();

            WWWForm form = connectorOut.DTO.ToForm();

            string url = webConnectionManager.GetUrl(connectorOut.UrlKey);

            Action<ConnectorInResponse> ConnectionCallback = (returnedResponse) =>
            {
                newResponse = returnedResponse;
                //Debug.Log("desde el callback: " + response);//
                IsDone = true;
            };

            StartCoroutine(PostCoroutine(url, form, ConnectionCallback));

            while (!IsDone)
            {
                await Task.Yield();
            }

            //Debug.Log("salio del bucle, retorno una response = " + response.ToString());//
            return newResponse;
        }

        public bool HttpStatusOk(long status)
        {
            if (status == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HttpStatusCreated(long status)
        {
            if (status == 201)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HttpStatusAccepted(long status)
        {
            if (status == 202)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ConnectorInResponse> GetConnection(ConnectorOutMessage connectorOut)
        {
            bool IsDone = false;
            ConnectorInResponse newResponse = new ConnectorInResponse();

            string url = webConnectionManager.GetUrl(connectorOut.UrlKey) + "/" +connectorOut.ExtraChar;

            Action<ConnectorInResponse> ConnectionCallback = (returnedResponse) =>
            {
                newResponse = returnedResponse;
                //Debug.Log("desde el callback: " + response);//
                IsDone = true;
            };

            StartCoroutine(GetCoroutine(url, ConnectionCallback));

            while (!IsDone)
            {
                await Task.Yield();
            }

            //Debug.Log("salio del bucle, retorno una response = " + response.ToString());//
            return newResponse;
        }


        private async Task<ConnectorInResponse> ConnectionTask(ConnectorOutMessage<OutDTO> connectorOut)
        {
            bool IsDone = false;
            ConnectorInResponse newResponse = new ConnectorInResponse();

            WWWForm form = connectorOut.DTO.ToForm();

            string url = connectorOut.UrlKey;

            Action<ConnectorInResponse> ConnectionCallback = (returnedResponse) =>
            {
                newResponse = returnedResponse;
            //Debug.Log("desde el callback: " + response);//
            IsDone = true;
            };
            if (form != null)
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

        private IEnumerator PostCoroutine(string url, WWWForm form, Action<ConnectorInResponse> responseCallback)
        {

            UnityWebRequest www = UnityWebRequest.Post(url, form);

            yield return www.SendWebRequest();

            ConnectorInResponse newResponse;

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                newResponse = new ConnectorInResponse(www.responseCode, "unexpected error");
            }
            else
            {
                newResponse = new ConnectorInResponse(www.responseCode, www.downloadHandler.text);
            }

            responseCallback(newResponse);
        }

        private IEnumerator GetCoroutine(string url, Action<ConnectorInResponse> responseCallback)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            ConnectorInResponse newResponse;

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                newResponse = new ConnectorInResponse(www.responseCode, "unexpected error");
            }
            else
            {
                newResponse = new ConnectorInResponse(www.responseCode, www.downloadHandler.text);
            }

            responseCallback(newResponse);
        }
    }
}