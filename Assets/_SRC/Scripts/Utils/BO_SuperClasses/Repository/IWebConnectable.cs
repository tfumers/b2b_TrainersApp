using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public interface IWebConnectable
    {
    }

    public interface IWebConnectable<T> : IWebConnectable
    {

        public T GetEntityFromJSON(JSONObject json);

        public T GetEntityFromResponse(ConnectorInResponse connectorInResponse);

        public List<T> GetListFromResponse(ConnectorInResponse connectorInResponse);
    }

    public interface ILoadEntitiesFromLocal<T>
    {
        public List<T> LoadEntitiesFromLocal();
    }

    public interface ILoadEntityFromFile<T>
    {
        public T LoadEntityFromFile(string directory);
    }
}