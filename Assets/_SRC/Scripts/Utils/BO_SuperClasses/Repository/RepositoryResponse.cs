using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public class RepositoryResponse<T>
    {
        string message;

        T returned;

        public RepositoryResponse(string message, T returned)
        {
            this.message = message;
            this.returned = returned;
        }

        public string Message { get => message; set => message = value; }
        public T Returned { get => returned; set => returned = value; }
    }
}