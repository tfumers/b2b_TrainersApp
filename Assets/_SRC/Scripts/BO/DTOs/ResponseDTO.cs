using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseDTO
{
    long httpStatus;

    string message;

    public ResponseDTO()
    {
        this.httpStatus = 500;
        this.message = "something happend";
    }

    public ResponseDTO(long httpStatus, string message)
    {
        this.httpStatus = httpStatus;
        this.message = message;
    }

    public long HttpStatus { get => httpStatus; set => httpStatus = value; }
    public string Message { get => message; set => message = value; }
}
