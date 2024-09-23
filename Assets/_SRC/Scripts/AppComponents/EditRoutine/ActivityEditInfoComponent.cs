using com.TresToGames.TrainersApp.Utils.AppComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivityEditInfoComponent : InteractiveAppComponent<Activity>
{
    [SerializeField] SelectedTrainingComponent selectedTrainingComponent;
    [SerializeField] ActivityToEditStatusComponent activityToEditStatusComponent;
    [SerializeField] ActTypeSelectorComponent actTypeSelectorComponent;
    [SerializeField] TMPro.TMP_InputField inputOrderNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtOrderNumber;
    [SerializeField] TMPro.TextMeshProUGUI txtActType;
    [SerializeField] TMPro.TMP_InputField inputActTypeValue;
    [SerializeField] TMPro.TextMeshProUGUI txtActTypeTextComplement;

    UnityEvent OnValueChanged = new UnityEvent();

    protected override void Prepare(Activity model)
    {
        //Limpieza de listeners de los UnityEvents
        inputOrderNumber.onEndEdit.RemoveAllListeners();
        inputActTypeValue.onEndEdit.RemoveAllListeners();
        actTypeSelectorComponent.OnSelectorChanged.RemoveAllListeners();
        OnValueChanged.RemoveAllListeners();

        //Actualizamos los elementos estáticos
        LoadStaticComponents(model);

        //Agregado de funciones a los listeners
        inputOrderNumber.onEndEdit.AddListener((str) => model.SetOrderNumber(str));

        inputActTypeValue.onEndEdit.AddListener((str) => model.SetTypeValue(str));
        inputActTypeValue.onEndEdit.AddListener((str) => OnValueChanged.Invoke());

        actTypeSelectorComponent.OnSelectorChanged.AddListener((sel) => model.ActTypeId = sel);
        actTypeSelectorComponent.OnSelectorChanged.AddListener((sel) => OnValueChanged.Invoke());

        OnValueChanged.AddListener(() => LoadStaticComponents(model));
    }

    private void LoadStaticComponents(Activity model)
    {

        activityToEditStatusComponent.LoadComponent(model.CheckActivityToEditStatus());

        selectedTrainingComponent.LoadComponent(model.Training);

        actTypeSelectorComponent.LoadComponent(model.ActTypeId);

        inputOrderNumber.text = model.OrderNumber.ToString();

        inputActTypeValue.text = model.TypeValue.ToString();

        switch (model.ActTypeId)
        {
            case Constant.ACTIVITY_TYPE_REPS:
                txtActType.text = "Repeticiones";
                txtActTypeTextComplement.text = "reps";
                break;
            case Constant.ACTIVITY_TYPE_TIMER:
                txtActType.text = "Temporizado";
                txtActTypeTextComplement.text = "segundos";
                break;
            default:
                txtActType.text = "Repeticiones/Tiempo (en segundos)";
                txtActTypeTextComplement.text = "reps/segundos";
                break;
        }

    }
}
