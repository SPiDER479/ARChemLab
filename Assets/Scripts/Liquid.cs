using System;
using TMPro;
using UnityEngine;
public class Liquid : MonoBehaviour
{
    public Material[] materials;
    public Transform liquid;
    public MeshRenderer material;
    public string type;
    private void Start()
    {
        type = "None";
    }
    public void IncreaseLiquid()
    {
        if (liquid != null)
            liquid.localScale = new Vector3(1, liquid.localScale.y + 0.05f, 1);
    }
    public void ReduceLiquid()
    {
        if (liquid != null)
            liquid.localScale = new Vector3(1, liquid.localScale.y - 0.05f, 1);
    }
    public void SelectChemical(Chemicals chemical)
    {
        if (liquid != null)
        {
            material.material = materials[chemical.colour];
            type = chemical.type;
        }
    }
    public void SelectIndicator(Indicator indicator)
    {
        if (liquid != null)
        {
            switch (type)
            {
                case "Acid":
                    material.material = materials[indicator.acidColor];
                    break;
                case "Base":
                    material.material = materials[indicator.baseColor];
                    break;
            }
        }
    }
}