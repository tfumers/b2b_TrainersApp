using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.TresToGames.TrainersApp.BO.ViewPrefabs;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GenericMessageEmergentView : EmergentView 
{

    ViewManager viewManager;
    NotificationManager notificationManager;

    [SerializeField] TMPro.TextMeshProUGUI txtErrorTitle;
    [SerializeField] TMPro.TextMeshProUGUI txtErrorDisplay;
    [SerializeField] Button btnAccept;
    [SerializeField] TMPro.TextMeshProUGUI txtAcceptButton;
    [SerializeField] Button btnDismiss;
    [SerializeField] TMPro.TextMeshProUGUI txtDismissButton;

    public override Task<bool> InitializeReferences()
    {
        notificationManager = B2BTrainer.Instance.notificationManager;
        viewManager = B2BTrainer.Instance.viewManager;
        return base.InitializeReferences();
    }

    public override Task<bool> LoadView()
    {
        ClearAndPrepareUIElements();

        return base.LoadView();
    }

    private void ClearAndPrepareUIElements()
    {
        btnAccept.onClick.RemoveAllListeners();
        btnDismiss.onClick.RemoveAllListeners();

        GenericMessage genericMessage = NotificationManager.EmergentMessage;
        txtErrorTitle.text = genericMessage.Title;
        txtErrorDisplay.text = genericMessage.Message;
        txtAcceptButton.text = genericMessage.ButtonAcceptText;
        txtDismissButton.text = genericMessage.ButtonDismissText;

        btnDismiss.gameObject.SetActive(genericMessage.CanDismiss);
        btnDismiss.enabled = genericMessage.CanDismiss;

        if (genericMessage.OnAcceptButtonClick!=null)
        {
            btnAccept.onClick.AddListener(() => genericMessage.OnAcceptButtonClick.Invoke());
            btnAccept.onClick.AddListener(() => viewManager.TurnEmergentOff(this));
        }
        else
        {
            btnAccept.onClick.AddListener(() => viewManager.TurnEmergentOff(this));
        }

        if (genericMessage.OnDismissButtonClick != null)
        {
            btnDismiss.onClick.AddListener(() => genericMessage.OnDismissButtonClick.Invoke());
            btnDismiss.onClick.AddListener(() => viewManager.TurnEmergentOff(this));
        }
        else
        {
            btnDismiss.onClick.AddListener(() => viewManager.TurnEmergentOff(this));
        }

    }
}
