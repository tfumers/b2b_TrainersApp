using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.TresToGames.TrainersApp.Utils.AppComponent
{

    public abstract class AppComponent<T> : MonoBehaviour
    {
        protected T model;

        public T Model { get => model;}

        public void LoadComponent(T model)
        {
            this.model = model;
            Prepare(model);
        }

        protected abstract void Prepare(T model);
    }

    public abstract class InteractiveAppComponent<T> : AppComponent<T>
    {
        [SerializeField] protected Button button;

        public void LoadComponent(T model, UnityEvent<T> OnClickedEvent)
        {
            this.model = model;
            Prepare(model);
            button.onClick.AddListener(() =>
            {
                OnClickedEvent.Invoke(model);
            });
        }

        public virtual void RemoveAllListeners()
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public abstract class InteractiveAppComponent : MonoBehaviour
    {
        [SerializeField] protected Button button;

        public void LoadComponent(UnityEvent OnClickedEvent)
        {
            button.onClick.AddListener(() =>
            {
                OnClickedEvent.Invoke();
            });
        }

        public virtual void RemoveAllListeners()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}