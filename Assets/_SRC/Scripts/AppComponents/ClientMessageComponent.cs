using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMessageComponent : AppComponent<Client>
{
    [SerializeField] TMPro.TextMeshProUGUI txtUsername;
    [SerializeField] TMPro.TextMeshProUGUI txtName;
    [SerializeField] TMPro.TextMeshProUGUI txtNumber;
    [SerializeField] AvatarContainerComponent avatarContainer;

    protected override void Prepare(Client model)
    {
        txtUsername.text = "@" + model.Username;
        txtName.text = "Nombre: " + model.Firstname + " " + model.Lastname;
        txtNumber.text = "Tel: " + model.Phone.ToString();

        avatarContainer.LoadComponent(model.AvatarImage);
    }
}
