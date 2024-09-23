using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendingNewClientComponent : InteractiveAppComponent<TrainerClientRelation> 
{
    [SerializeField] TMPro.TextMeshProUGUI txtClientName;
    [SerializeField] TMPro.TextMeshProUGUI txtRelCreationDate;
    [SerializeField] AvatarContainerComponent avatarContainer;
    protected override void Prepare(TrainerClientRelation model)
    {
        txtClientName.text = model.Client.Firstname + " " + model.Client.Lastname;

        txtRelCreationDate.text = "Solicitado el día: " + model.CreatedAt.ToString(Constant.DEFAULT_APP_DATE_FORMAT_SIMPLIFIED);

        avatarContainer.LoadComponent(model.Client.AvatarImage);
    }
}
