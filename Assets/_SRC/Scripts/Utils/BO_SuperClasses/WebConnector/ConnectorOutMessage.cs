using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public class ConnectorOutMessage
    {
        string urlKey;

        string extraChar;

        public ConnectorOutMessage(string urlKey, string extraChar = "")
        {
            this.urlKey = urlKey;
            this.extraChar = extraChar;
        }

        public string UrlKey { get => urlKey; set => urlKey = value; }
        public string ExtraChar { get => extraChar; set => extraChar = value; }
    }
    public class ConnectorOutMessage<T> : ConnectorOutMessage where T:OutDTO
    {

        T dto;

        public ConnectorOutMessage(string urlKey, T dto) : base(urlKey)
        {
            this.dto = dto;
        }

        public T DTO { get => dto; set => dto = value; }
    }
}