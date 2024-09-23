using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public class ServiceResponse
    {
        bool completed;

        string message;

        public ServiceResponse(bool completed, string message)
        {
            this.completed = completed;
            this.message = message;
        }

        public string Message { get => message; }
        public bool Completed { get => completed; }
    }

    public class ServiceResponse<T>
    {
        bool completed;

        string message;

        T returned;

        public ServiceResponse()
        {

        }

        public ServiceResponse(bool completed, string message, T returned)
        {
            this.completed = completed;
            this.message = message;
            this.returned = returned;
        }

        public string Message { get => message; set => message = value; }
        public bool Completed { get => completed; set => completed = value; }
        public T Returned { get => returned; set => returned = value; }
    }

}