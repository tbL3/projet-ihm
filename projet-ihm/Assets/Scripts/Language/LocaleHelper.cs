using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleHelper : MonoBehaviour
{
    public static string GetUserDefaultLangage()
    {
        SystemLanguage lang = Application.systemLanguage;

        switch (lang)
        {
            case SystemLanguage.French:
                return LocaleApplication.FR;
            default:
                return LocaleApplication.EN;              
        }
    }

}
