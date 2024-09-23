using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAvatarOutDTO : OutDTO
{
    Dictionary<string, string> fields;
    public UpdateAvatarOutDTO(Dictionary<string, string> info) : base(info)
    {
        //fields: "id"


        this.fields = info;
    }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", fields["id"]);
        return form;
    }
}
