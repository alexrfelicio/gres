using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [SerializeField] Slider slider;

    private int currentLanguage;

    private void Awake() {
        slider.value = GamePersist.Instance.volume;
        if (GamePersist.Instance.language == 0) {
            FindObjectOfType<ButtonManager>().SelectedPTBR();
        } else {
            FindObjectOfType<ButtonManager>().SelectedENUS();
        }
        SetCurrentLanguage(GamePersist.Instance.language);
    }

    public void DefaultValues() {
        slider.value = 1f;
        currentLanguage = 0;
        FindObjectOfType<ButtonManager>().SelectedPTBR();
        LangResolver.Instance.SetSystemLanguage(SystemLanguage.Portuguese);
    }

    public void SetCurrentLanguage(int language) {
        currentLanguage = language;
    }

    public float GetCurrentVolume() {
        return slider.value;
    }

    public int GetCurrentLanguage() {
        return currentLanguage;
    }

}
