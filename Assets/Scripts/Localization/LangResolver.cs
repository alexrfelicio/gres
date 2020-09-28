using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LangResolver : MonoBehaviour {

    public static LangResolver Instance { get; private set; }
    private Dictionary<string, string> lang = new Dictionary<string, string>();
    private SystemLanguage systemLanguage;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetSystemLanguage(SystemLanguage.Portuguese);
        } else {
            Instance.SetSystemLanguage(Instance.GetSystemLanguage());
            Destroy(gameObject);
        }
    }

    public void SetPortuguese() {
        Debug.Log("wtf?");
        Instance.SetSystemLanguage(SystemLanguage.Portuguese);
    }

    public void SetEnglish() {
        Instance.SetSystemLanguage(SystemLanguage.English);
    }

    public void SetSystemLanguage(SystemLanguage lang) {
        systemLanguage = lang;
        ReadProperties();
    }

    public SystemLanguage GetSystemLanguage() {
        return systemLanguage;
    }

    public string GetTextByKey(string key) {
        return lang[key];
    }

    private void ReadProperties() {
        var file = Resources.Load<TextAsset>(systemLanguage.ToString());
        if (file == null) {
            systemLanguage = SystemLanguage.English;
            file = Resources.Load<TextAsset>(systemLanguage.ToString());
        }
        foreach (var line in file.text.Split('\n')) {
            var prop = line.Split('=');
            lang[prop[0]] = prop[1];
        }

        ResolveTexts();
    }

    private void ResolveTexts() {
        var allTexts = Resources.FindObjectsOfTypeAll<LangText>();
        foreach (var langText in allTexts) {
            var text = langText.GetComponent<Text>();
            text.text = Regex.Unescape(lang[langText.identifier]);
        }
    }

}
