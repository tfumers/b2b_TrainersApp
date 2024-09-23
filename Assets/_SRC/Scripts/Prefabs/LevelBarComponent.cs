using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarComponent : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txtLVL;
    [SerializeField] TMPro.TextMeshProUGUI txtXP;

    [SerializeField] RectTransform loaderTransform;

    public void PrepareLevelBarComponent(Client client)
    {
        int completeSize = (int)loaderTransform.sizeDelta.x;

        int requiredExperience = ((int)client.Level * (1 / 2) * 100) + (75 * (int)client.Level);

        txtLVL.text = client.Level.ToString();
        txtXP.text = client.Experience.ToString() + "/" + requiredExperience.ToString();



        loaderTransform.sizeDelta = new Vector2((completeSize * client.Experience) / (requiredExperience), loaderTransform.sizeDelta.y);

    }


}
