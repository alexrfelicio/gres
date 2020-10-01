using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    [SerializeField] Button ptBr;
    [SerializeField] Button enUs;

    public void SelectedPTBR() {
        var ptImage = ptBr.GetComponent<Image>();
        var tempPtColor = ptImage.color;
        tempPtColor.a = 1f;
        ptImage.color = tempPtColor;

        var enImage = enUs.GetComponent<Image>();
        var tempEnColor = enImage.color;
        tempEnColor.a = 0.5f;
        enImage.color = tempEnColor;
    }

    public void SelectedENUS() {
        var ptImage = ptBr.GetComponent<Image>();
        var tempPtColor = ptImage.color;
        tempPtColor.a = 0.5f;
        ptImage.color = tempPtColor;

        var enImage = enUs.GetComponent<Image>();
        var tempEnColor = enImage.color;
        tempEnColor.a = 1f;
        enImage.color = tempEnColor;
    }
}
