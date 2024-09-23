using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{

    public abstract class Controller : MonoBehaviour
    {
        public abstract Task<bool> Initialize();
    }
}