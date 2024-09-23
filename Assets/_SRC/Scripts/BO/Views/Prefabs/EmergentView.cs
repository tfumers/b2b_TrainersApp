using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO.ViewPrefabs
{ 
    public class EmergentView : View
    {
        public void ChangeSortingOrder(int emergentOrder = 0)
        {
            GetComponent<Canvas>().sortingOrder = emergentOrder;
        }
    }
}