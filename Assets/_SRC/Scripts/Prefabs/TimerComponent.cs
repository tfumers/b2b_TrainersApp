using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerComponent : MonoBehaviour
{
    UnityEvent OnTimerStarted = new UnityEvent();
    UnityEvent OnTimerEnded = new UnityEvent();
    UnityEvent OnTimerPaused = new UnityEvent();

    [SerializeField] TMPro.TextMeshProUGUI txtTimer;

    private bool startedTimer = false;
    private bool pausedTimer = false;
    private bool endedTimer = false;

    int totalElapsedTime = 0;

    public void ConfigureTimer(Activity act, ActivityButton playBtn, UnityEvent<Activity> OnActivityStarted, UnityEvent<Activity> OnActivityEnded)
    {
        totalElapsedTime = 0;
        startedTimer = false;
        pausedTimer = false;
        endedTimer = false;

        OnTimerStarted.RemoveAllListeners();
        OnTimerPaused.RemoveAllListeners();
        OnTimerEnded.RemoveAllListeners();

        bool activityTypeReps = act.ActTypeId == Constant.ACTIVITY_TYPE_REPS;
        int timerValue = activityTypeReps ? 0 : act.TypeValue;

        if (act.Completed)
        {
            playBtn.Button.interactable = false;
            playBtn.SetStopButton();
        }
        else
        {
            playBtn.Button.interactable = true;
            playBtn.SetPlayButton();
        }

        UpdateTimerText(timerValue);

        playBtn.Button.onClick.RemoveAllListeners();

        playBtn.Button.onClick.AddListener(() =>
        {
            if (!endedTimer&&!startedTimer)
            {
                StartCoroutine("StartTimer", timerValue);
            }
            else
            {
                if (activityTypeReps)
                {
                    EndTimer();   
                }
                else
                {
                    PauseTimer();
                }
            }
        });

        OnTimerStarted.AddListener(playBtn.SetPauseButton);
        OnTimerStarted.AddListener(() => OnActivityStarted.Invoke(act));
        OnTimerPaused.AddListener(playBtn.SetPlayButton);
        OnTimerEnded.AddListener(()=> {
            playBtn.SetStopButton();
            playBtn.Button.interactable = false;
            act.Completed = true;
            act.ElapsedTime = totalElapsedTime;
            OnActivityEnded.Invoke(act);
            Debug.Log("Actividad completada");
        });

    }

    IEnumerator StartTimer(int seconds = 0)
    {
        OnTimerStarted.Invoke();
        startedTimer = true;
        float sign = (seconds == 0)? 1:-1;
        float elapsedTime = 0;
        float absoluteTimer = 0;
        do
        {
            if (!pausedTimer)
            {
                elapsedTime += Time.deltaTime * 1;
                absoluteTimer = seconds + (elapsedTime*sign);
                UpdateTimerText((int)absoluteTimer);
            }

            yield return null;
        } while (absoluteTimer > 0 && !endedTimer);

        if (seconds != 0)
        {
            totalElapsedTime = seconds;
            EndTimer();
            //Si el temporizador recibe segundos, es porque está haciendo un conteo
        }
        else
        {
            totalElapsedTime = (int)absoluteTimer;
        }
    }

    private void PauseTimer()
    {
        pausedTimer = !pausedTimer;
        OnTimerPaused.Invoke();
    }

    private void EndTimer()
    {
        endedTimer = true;
        StopCoroutine("StartTime");
        OnTimerEnded.Invoke();
    }

    public void UpdateTimerText(int value)
    {
        string textToShow = (value / 60).ToString() + ":" + (value % 60).ToString();

        txtTimer.text = textToShow;
    }
}
