using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AddChemical : MonoBehaviour
{
    [SerializeField] private GameObject liquid, drop;
    [SerializeField] private Material material, dropmat;
    private List<string> chemicals = new List<string>();
    private int temp;
    public void Add(Chemical chemical)
    {
        Liquid(chemical);
        Changes(chemical);
    }
    private void Liquid(Chemical chemical)
    {
        if (GameObject.FindGameObjectWithTag("Liquid") == null)
        {
            Instantiate(liquid, 
                GameObject.FindGameObjectWithTag("Beaker").transform.position + new Vector3(0, 0.5f, 0), 
                Quaternion.identity);
            material.color = chemical.color;
        }
        dropmat.color = chemical.color;
        StartCoroutine(Drops(0));
        StartCoroutine(Drops(0.1f));
        StartCoroutine(Drops(0.2f));
        StartCoroutine(Drops(0.3f));
        StartCoroutine(Drops(0.4f));
    }
    IEnumerator Drops(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(drop, GameObject.FindGameObjectWithTag("Beaker").transform.position + new Vector3(0, 3, 0),
            Quaternion.identity);
    }
    private void Changes(Chemical chemical)
    {
        if (chemical.formula == "Litmus")
            GetComponent<AdvancedChemistrySimulator>().
                indicator = AdvancedChemistrySimulator.IndicatorType.Litmus;
        else if (chemical.formula == "Methyl")
            GetComponent<AdvancedChemistrySimulator>().
                indicator = AdvancedChemistrySimulator.IndicatorType.MethylOrange;
        else if (chemical.formula == "Phenolph")
            GetComponent<AdvancedChemistrySimulator>().
                indicator = AdvancedChemistrySimulator.IndicatorType.Phenolphthalein;
        else if (chemical.formula == "HCl")
                GetComponent<AdvancedChemistrySimulator>().
                    chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().HCl);
        else if (chemical.formula == "HNO3")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().HNO3);
        else if (chemical.formula == "H2SO4")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().H2SO4);
        else if (chemical.formula == "NaOH")
                GetComponent<AdvancedChemistrySimulator>().
                    chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().NaOH);
        else if (chemical.formula == "KOH")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().KOH);
        else if (chemical.formula == "LiOH")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().LiOH);
        else if (chemical.formula == "CH3COOH")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().CH3COOH);
        else if (chemical.formula == "H2CO3")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().H2CO3);
        else if (chemical.formula == "NH4OH")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().NH4OH);
        else if (chemical.formula == "MGOH2")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().MGOH2);
        else if (chemical.formula == "H20")
            GetComponent<AdvancedChemistrySimulator>().
                chemicalsToReact.Add(GetComponent<AdvancedChemistrySimulator>().Water);
        
        string result = GetComponent<AdvancedChemistrySimulator>()
            .React(GetComponent<AdvancedChemistrySimulator>().chemicalsToReact,
                GetComponent<AdvancedChemistrySimulator>().indicator);
            
        Color color = GetComponent<AdvancedChemistrySimulator>()
            .React2(GetComponent<AdvancedChemistrySimulator>().chemicalsToReact,
                GetComponent<AdvancedChemistrySimulator>().indicator);

        temp = 0;
        GetComponent<UI>().Popup(result);
        StartCoroutine(ChangeColor(color));
    }

    IEnumerator ChangeColor(Color color)
    {
        yield return new WaitForSeconds(0.01f);
        material.color = Color.Lerp(material.color, color, 0.1f);
        if (++temp < 10)
            StartCoroutine(ChangeColor(color));
    }
}