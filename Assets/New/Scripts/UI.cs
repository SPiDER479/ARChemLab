using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel, acidsPanel, strongAcidPanel, weakAcidPanel,
        basesPanel, strongBasePanel, weakBasePanel, indicatorPanel, popupPanel;
    [SerializeField] private TextMeshProUGUI popupText;
    private void OnEnable()
    {
        Popup("Tap on a recognized plane to place beaker");
    }
    public void Main()
    {
        mainPanel.SetActive(true);
        acidsPanel.SetActive(false);
        strongAcidPanel.SetActive(false);
        strongBasePanel.SetActive(false);
        weakAcidPanel.SetActive(false);
        weakBasePanel.SetActive(false);
        basesPanel.SetActive(false);
        indicatorPanel.SetActive(false);
    }
    public void Acids()
    {
        mainPanel.SetActive(false);
        acidsPanel.SetActive(true);
    }
    public void Bases()
    {
        mainPanel.SetActive(false);
        basesPanel.SetActive(true);
    }
    public void Indicators()
    {
        mainPanel.SetActive(false);
        indicatorPanel.SetActive(true);
    }
    public void StrongAcids()
    {
        acidsPanel.SetActive(false);
        strongAcidPanel.SetActive(true);
    }
    public void WeakAcids()
    {
        acidsPanel.SetActive(false);
        weakAcidPanel.SetActive(true);
    }
    public void StrongBase()
    {
        basesPanel.SetActive(false);
        strongBasePanel.SetActive(true);
    }
    public void WeakBase()
    {
        basesPanel.SetActive(false);
        weakBasePanel.SetActive(true);
    }
    public void Popup(string text)
    {
        StopCoroutine(StopPopup());
        popupPanel.SetActive(true);
        popupText.text = text;
        StartCoroutine(StopPopup());
    }
    IEnumerator StopPopup()
    {
        yield return new WaitForSeconds(5);
        popupPanel.SetActive(false);
    }
    public void MakeEmpty()
    {
        Destroy(GameObject.FindGameObjectWithTag("Liquid"));
        GetComponent<AdvancedChemistrySimulator>().chemicalsToReact.Clear();
    }
}