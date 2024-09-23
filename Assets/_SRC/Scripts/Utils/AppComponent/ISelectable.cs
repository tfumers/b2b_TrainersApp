using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.Utils.AppComponent
{
    public interface ISelectable
    {
        public void Select();

        public void Deselect();
    }
}