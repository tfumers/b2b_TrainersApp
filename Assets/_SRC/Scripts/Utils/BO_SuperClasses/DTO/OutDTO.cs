using System.Collections.Generic;
using UnityEngine;

namespace com.TresToGames.TrainersApp.BO_SuperClasses
{
    public abstract class OutDTO
    {
        protected Dictionary<string, string> fields;

        public OutDTO(Dictionary<string, string> info)
        {
            this.fields = info;
        }
        public virtual WWWForm ToForm()
        {
            WWWForm newForm = new WWWForm();

            foreach (KeyValuePair<string, string> kvp in fields)
            {
                newForm.AddField(kvp.Key, kvp.Value);
            }

            return newForm;
        }

    }
}