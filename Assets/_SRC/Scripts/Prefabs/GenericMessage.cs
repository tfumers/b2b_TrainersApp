using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericMessage
{
    string title;

    string message;

    string buttonAcceptText;

    string buttonDismissText;

    bool canDismiss;

    UnityEvent onAcceptButtonClick = new UnityEvent();

    UnityEvent onDismissButtonClick = new UnityEvent();

    public GenericMessage(string title, string message, string buttonAceptText = "aceptar", string buttonDismissText = "rechazar", bool canDismiss = false, UnityEvent OnAcceptEvent = null, UnityEvent OnDismissEvent = null)
    {
        this.title = title;
        this.message = message;
        this.buttonAcceptText = buttonAceptText;
        this.buttonDismissText = buttonDismissText;
        this.CanDismiss = canDismiss;
        this.onAcceptButtonClick = OnAcceptEvent;
        this.onDismissButtonClick = OnDismissEvent;
    }

    public string Title { get => title; set => title = value; }
    public string Message { get => message; set => message = value; }
    public string ButtonAcceptText { get => buttonAcceptText; set => buttonAcceptText = value; }
    public string ButtonDismissText { get => buttonDismissText; set => buttonDismissText = value; }
    public bool CanDismiss { get => canDismiss; set => canDismiss = value; }
    public UnityEvent OnAcceptButtonClick { get => onAcceptButtonClick; set => onAcceptButtonClick = value; }
    public UnityEvent OnDismissButtonClick { get => onDismissButtonClick; set => onDismissButtonClick = value; }
}
