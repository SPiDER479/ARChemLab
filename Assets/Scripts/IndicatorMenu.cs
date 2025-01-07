using UnityEngine;
public class IndicatorMenu : MonoBehaviour
{
    [SerializeField] private GameObject selection, acids, bases, indicators;
    public void OpenAcids()
    {
        selection.SetActive(false);
        acids.SetActive(true);
    }
    public void OpenBases()
    {
        selection.SetActive(false);
        bases.SetActive(true);
    }
    public void OpenIndicators()
    {
        selection.SetActive(false);
        indicators.SetActive(true);
    }
    public void OpenSelection()
    {
        acids.SetActive(false);
        bases.SetActive(false);
        indicators.SetActive(false);
        selection.SetActive(true);
    }
}