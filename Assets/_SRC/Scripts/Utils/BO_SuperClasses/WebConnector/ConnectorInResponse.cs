using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public class ConnectorInResponse
    {
        long httpStatus;

        string message;

        public ConnectorInResponse()
        {
            this.httpStatus = 500;
            this.message = "something happend";
        }

        public ConnectorInResponse(long httpStatus, string message)
        {
            this.httpStatus = httpStatus;
            this.message = message;
        }

        public long HttpStatus { get => httpStatus; set => httpStatus = value; }
        public string Message { get => message; set => message = value; }
    }
}