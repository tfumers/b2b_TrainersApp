using System.Threading.Tasks;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO.ViewPrefabs
{
    public abstract class View : MonoBehaviour
    {
        public bool hasTransition = false;

        public bool initialized = false;

        public bool reloadView = false;

        public async Task<bool> SetOn()
        {

            if (initialized)
            {
                if (reloadView)
                {
                    Task<bool> init = InitializeReferences();
                    await init;
                    if (init.Result == false)
                    {
                        return false;
                    }
                    Task<bool> rldView = LoadView();
                    await rldView;
                    if (rldView.Result == false)
                    {
                        return false;
                    }
                    this.gameObject.SetActive(true);
                    return rldView.Result;
                }
                else
                {
                    this.gameObject.SetActive(true);
                    return true;
                }
            }

            if (!initialized)
            {
                Task<bool> init = InitializeReferences();
                await init;
                if (init.Result == false)
                {
                    return false;
                }
                Task<bool> rldView = LoadView();
                await rldView;
                if (rldView.Result == false)
                {
                    return false;
                }
                initialized = init.Result;
                this.gameObject.SetActive(true);
                return init.Result;
            }
            //if it has transition, waits until it has ended

            return false;
        }

        public void SetOff()
        {
            //if it has transition, waits until it has ended

            this.gameObject.SetActive(false);
        }

        public void PrintDebug(string input)
        {
            Debug.Log(input);
        }

        public virtual Task<bool> InitializeReferences()
        {
            //procesos a inicializar
            
            return Task.FromResult(true);//por defecto, si no tiene que cargar nada, retorna cero
        }

        public virtual Task<bool> LoadView()
        {
            //proceso que se ejecutará al cargar la vista

            return Task.FromResult(true);//por defecto, suponiendo que cargo todo, retorna verdadero
        }
    }
}



