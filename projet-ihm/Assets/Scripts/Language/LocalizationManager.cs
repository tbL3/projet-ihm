using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    private const string FILE_PREFIX = "text_";
    private const string FILE_EXTENSION = ".json";
    private string FULL_NAME_TEXT_FILE;
    private string FULL_PATH_TEXT_FILE;
    private string LANGUAGE_CHOOSE = "EN";
    private string LOADED_JSON_TEXT = "";

    private bool _isReady = false;
    private bool _isFileFound = false;
    private bool _isTryChangeLangRunTime = false;
    private Dictionary<string, string> _localizedDictionary;
    private LocalizationData _loadedData;

    private static LocalizationManager LocalizationManagerInstance;

    public static LocalizationManager Instance
    {
        get
        {
            if(LocalizationManagerInstance == null)
            {
                LocalizationManagerInstance = FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;
            }
            return LocalizationManagerInstance;
        }
    }

    IEnumerator Start()
    {
        LANGUAGE_CHOOSE = LocaleHelper.GetUserDefaultLangage();
        FULL_NAME_TEXT_FILE = FILE_PREFIX + LANGUAGE_CHOOSE.ToLower() + FILE_EXTENSION;
        FULL_PATH_TEXT_FILE = Path.Combine(Application.streamingAssetsPath, FULL_PATH_TEXT_FILE);

        yield return StartCoroutine(LoadJsonLanguageData());
        _isReady = true;
    }

    IEnumerator LoadJsonLanguageData()
    {
        yield return null;
    }

}
