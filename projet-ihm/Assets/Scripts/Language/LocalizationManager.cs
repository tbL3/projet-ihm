using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour
{
    private const string FILE_PREFIX = "text_";
    private const string FILE_EXTENSION = ".json";
    private string FULL_NAME_TEXT_FILE;
    private string FULL_PATH_TEXT_FILE;
    private string LANGUAGE_CHOOSE = "EN";
    private string LOADED_JSON_TEXT = "";

    public bool _isReady = false;
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
        Debug.Log("start");
        LANGUAGE_CHOOSE = LocaleHelper.GetUserDefaultLangage();
        FULL_NAME_TEXT_FILE = FILE_PREFIX + LANGUAGE_CHOOSE.ToLower() + FILE_EXTENSION;
        FULL_PATH_TEXT_FILE = Path.Combine(Application.streamingAssetsPath, FULL_NAME_TEXT_FILE);
        yield return StartCoroutine(LoadJsonLanguageData());
        _isReady = true;
    }

    IEnumerator LoadJsonLanguageData()
    {
        CheckFileExists();
        yield return new WaitUntil(() => _isFileFound);
        _loadedData = JsonUtility.FromJson<LocalizationData>(LOADED_JSON_TEXT);
        _localizedDictionary = new Dictionary<string, string>(_loadedData.items.Count);
        _loadedData.items.ForEach(item =>
        {
            try
            {
                _localizedDictionary.Add(item.key, item.value);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        });
    }

    private void CheckFileExists()
    {
        if (!File.Exists(FULL_PATH_TEXT_FILE))
        {
            CopyFileFromRessource();
        }
        else
        {
            LoadFileContents();
        }
    }

    private void CopyFileFromRessource()
    {
        TextAsset myFile = Resources.Load(FILE_PREFIX + LANGUAGE_CHOOSE) as TextAsset;
        if(myFile == null)
        {
            Debug.LogError("could not load file");
            return;
        }
        LOADED_JSON_TEXT = myFile.ToString();
        File.WriteAllText(FULL_PATH_TEXT_FILE, LOADED_JSON_TEXT);
        StartCoroutine(WaitCreationFile());
    }

    IEnumerator WaitCreationFile()
    {
        FileInfo myFile = new FileInfo(FULL_NAME_TEXT_FILE);
        float timeOut = 0.0f;
        while (timeOut < 5.0f && !IsFileFinishCreated(myFile))
        {
            timeOut += Time.deltaTime;
            yield return null;
        }
    }

    private bool IsFileFinishCreated(FileInfo file)
    {
        FileStream stream = null;
        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {           
            _isFileFound = true;
            Debug.Log("file created");
            return true;
        }
        finally
        {
            if (stream != null){
                stream.Close();
            }
        }
        _isFileFound = false;
        return false;
    }

    private void LoadFileContents()
    {
        LOADED_JSON_TEXT = File.ReadAllText(FULL_PATH_TEXT_FILE);
        _isFileFound = true;
    }

    public string GetTextForKey(string localizationKey)
    {
        if (_localizedDictionary.ContainsKey(localizationKey))
        {
            return _localizedDictionary[localizationKey];
        }
        else
        {
            return "key not found";
        }
    }
}
