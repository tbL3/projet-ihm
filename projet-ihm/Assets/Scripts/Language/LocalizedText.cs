using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string _localizationKey;
    TMP_Text _textMeshProComponent;
    IEnumerator Start()
    {
        while (!LocalizationManager.Instance._isReady)
        {
            yield return null;
        }
        AttributionText();
    }

    public void AttributionText()
    {
        if(_textMeshProComponent == null)
        {
            _textMeshProComponent = gameObject.GetComponent<TMP_Text>();
        }
        try
        {
            _textMeshProComponent.text = LocalizationManager.Instance.GetTextForKey(_localizationKey);

        }catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

}
