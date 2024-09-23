using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISubmitable<T>
{
    public UnityEvent<T> OnSubmitButtonClick();
}
