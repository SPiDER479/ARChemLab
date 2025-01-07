using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedChemistrySimulator : MonoBehaviour
{
    // Define the types of chemicals
    public enum ChemicalType { StrongAcid, WeakAcid, StrongBase, WeakBase, Water }

    // Define the types of indicators
    public enum IndicatorType { Litmus, MethylOrange, Phenolphthalein, None }

    // Create a struct to store chemical details
    [System.Serializable]
    public struct Chemical
    {
        public string name;          // Chemical name
        public ChemicalType type;    // Type of chemical (Acid/Base/Water)
        public string formula;       // Chemical formula
    }

    // public struct Indicator
    // {
    //     public string name;
    //     public IndicatorType type;
    // }

    // Example chemicals
    public Chemical HCl = new Chemical { name = "Hydrochloric Acid", type = ChemicalType.StrongAcid, formula = "HCl" };
    public Chemical HNO3 = new Chemical { name = "Sulfuric Acid", type = ChemicalType.StrongAcid, formula = "HNO3" };
    public Chemical H2SO4 = new Chemical { name = "Sulfuric Acid", type = ChemicalType.StrongAcid, formula = "H2SO4" };
    
    public Chemical NaOH = new Chemical { name = "Sodium Hydroxide", type = ChemicalType.StrongBase, formula = "NaOH" };
    public Chemical KOH = new Chemical { name = "Sodium Hydroxide", type = ChemicalType.StrongBase, formula = "KOH" };
    public Chemical LiOH = new Chemical { name = "Sodium Hydroxide", type = ChemicalType.StrongBase, formula = "LiOH" };
    
    public Chemical CH3COOH = new Chemical { name = "Acetic Acid", type = ChemicalType.WeakAcid, formula = "CH3COOH" };
    public Chemical H2CO3 = new Chemical { name = "Acetic Acid", type = ChemicalType.WeakAcid, formula = "H2CO3" };
    
    public Chemical NH4OH = new Chemical { name = "Ammonium Hydroxide", type = ChemicalType.WeakBase, formula = "NH4OH" };
    public Chemical MGOH2 = new Chemical { name = "Ammonium Hydroxide", type = ChemicalType.WeakBase, formula = "MGOH2" };
    
    public Chemical Water = new Chemical { name = "Water", type = ChemicalType.Water, formula = "H2O" };
    
    //public Indicator Litmus = new Indicator { name = "Litmus", type = IndicatorType.Litmus };
    
    public List<Chemical> chemicalsToReact = new List<Chemical>();
    public IndicatorType indicator;
    // Method to determine the combined chemical reaction
    public string React(List<Chemical> chemicals, IndicatorType? indicator = null)
    {
        // Variables to keep track of acid/base categories
        int strongAcids = 0, weakAcids = 0, strongBases = 0, weakBases = 0, waterCount = 0;

        string reactionEquation = "";
        Color resultantColor = Color.white;

        // Count how many acids, bases, and water are in the mix
        foreach (var chemical in chemicals)
        {
            if (chemical.type == ChemicalType.StrongAcid) strongAcids++;
            else if (chemical.type == ChemicalType.WeakAcid) weakAcids++;
            else if (chemical.type == ChemicalType.StrongBase) strongBases++;
            else if (chemical.type == ChemicalType.WeakBase) weakBases++;
            else if (chemical.type == ChemicalType.Water) waterCount++;
        }

        // Check for multiple combinations and create a reaction
        if (strongAcids > 0 && strongBases > 0)
        {
            // Strong acid + Strong base -> Neutral
            reactionEquation = "Strong Acid + Strong Base → Salt + H2O";
            resultantColor = Color.clear; // Neutral
        }
        else if (strongAcids > 0 && weakBases > 0)
        {
            // Strong acid + Weak base -> Acidic solution
            reactionEquation = "Strong Acid + Weak Base → Acidic Salt + H2O";
            resultantColor = Color.red; // Acidic
        }
        else if (weakAcids > 0 && strongBases > 0)
        {
            // Weak acid + Strong base -> Basic solution
            reactionEquation = "Weak Acid + Strong Base → Basic Salt + H2O";
            resultantColor = Color.blue; // Basic
        }
        else if (weakAcids > 0 && weakBases > 0)
        {
            // Weak acid + Weak base -> Weakly Basic/Acidic
            reactionEquation = "Weak Acid + Weak Base → Weak Salt + H2O";
            resultantColor = Color.yellow;
        }
        else if ((strongAcids > 0 || weakAcids > 0) && waterCount > 0)
        {
            // Acid + Water -> Dissociation
            reactionEquation = "Acid + Water → H3O⁺ + Anion";
            resultantColor = strongAcids > 0 ? Color.red : Color.magenta; // Strong acids produce red, weak produce pink
        }
        else if ((strongBases > 0 || weakBases > 0) && waterCount > 0)
        {
            // Base + Water -> Dissociation
            reactionEquation = "Base + Water → OH- + Cation";
            resultantColor = strongBases > 0 ? Color.blue : Color.cyan; // Strong bases produce blue, weak produce cyan
        }
        else if (waterCount > 0 && chemicals.Count == waterCount)
        {
            // Only water in the mixture
            reactionEquation = "Only water present.";
            resultantColor = Color.white;
        }
        else
        {
            // No valid reaction
            reactionEquation = "No valid reaction detected.";
            resultantColor = Color.gray;
        }

        // Adjust the color based on the indicator if one is used
        if (indicator != null)
        {
            switch (indicator)
            {
                case IndicatorType.Litmus:
                    //resultantColor = strongAcids > 0 || weakAcids > 0 ? Color.red : (strongBases > 0 || weakBases > 0 ? Color.blue : Color.green);
                    if (strongAcids > strongBases)
                        resultantColor = Color.red;
                    else if (strongBases > strongAcids)
                        resultantColor = Color.blue;
                    else if (weakAcids > weakBases)
                        resultantColor = Color.red;
                    else if (weakBases > weakAcids)
                        resultantColor = Color.blue;
                    else
                        resultantColor = Color.clear;
                    break;

                case IndicatorType.MethylOrange:
                    //resultantColor = strongAcids > 0 || weakAcids > 0 ? Color.red : new Color(1, 0.5f, 0, 1); // Basic solutions turn orange/yellow
                    if (strongAcids > strongBases)
                        resultantColor = Color.red;
                    else if (strongBases > strongAcids)
                        resultantColor = new Color(1, 0.5f, 0, 1);
                    else if (weakAcids > weakBases)
                        resultantColor = Color.red;
                    else if (weakBases > weakAcids)
                        resultantColor = new Color(1, 0.5f, 0, 1);
                    else
                        resultantColor = Color.clear;
                    break;

                case IndicatorType.Phenolphthalein:
                    //resultantColor = strongBases > 0 || weakBases > 0 ? Color.magenta : Color.clear; // Basic turns pink/magenta
                    if (strongAcids > strongBases)
                        resultantColor = Color.clear;
                    else if (strongBases > strongAcids)
                        resultantColor = Color.magenta;
                    else if (weakAcids > weakBases)
                        resultantColor = Color.clear;
                    else if (weakBases > weakAcids)
                        resultantColor = Color.magenta;
                    else
                        resultantColor = Color.clear;
                    break;
            }
        }

        // Display the reaction result
        return "Reaction: " + reactionEquation + "\nResultant Color: " + ColorUtility.ToHtmlStringRGB(resultantColor);
        // Change the color of an object in the scene to show the result
    }
    public Color React2(List<Chemical> chemicals, IndicatorType? indicator = null)
    {
        // Variables to keep track of acid/base categories
        int strongAcids = 0, weakAcids = 0, strongBases = 0, weakBases = 0, waterCount = 0;

        string reactionEquation = "";
        Color resultantColor = Color.white;

        // Count how many acids, bases, and water are in the mix
        foreach (var chemical in chemicals)
        {
            if (chemical.type == ChemicalType.StrongAcid) strongAcids++;
            else if (chemical.type == ChemicalType.WeakAcid) weakAcids++;
            else if (chemical.type == ChemicalType.StrongBase) strongBases++;
            else if (chemical.type == ChemicalType.WeakBase) weakBases++;
            else if (chemical.type == ChemicalType.Water) waterCount++;
        }

        // Check for multiple combinations and create a reaction
        if (strongAcids > 0 && strongBases > 0)
        {
            // Strong acid + Strong base -> Neutral
            reactionEquation = "Strong Acid + Strong Base → Salt + H₂O";
            resultantColor = Color.clear; // Neutral
        }
        else if (strongAcids > 0 && weakBases > 0)
        {
            // Strong acid + Weak base -> Acidic solution
            reactionEquation = "Strong Acid + Weak Base → Acidic Salt + H₂O";
            resultantColor = Color.red; // Acidic
        }
        else if (weakAcids > 0 && strongBases > 0)
        {
            // Weak acid + Strong base -> Basic solution
            reactionEquation = "Weak Acid + Strong Base → Basic Salt + H₂O";
            resultantColor = Color.blue; // Basic
        }
        else if (weakAcids > 0 && weakBases > 0)
        {
            // Weak acid + Weak base -> Weakly Basic/Acidic
            reactionEquation = "Weak Acid + Weak Base → Weak Salt + H₂O";
            resultantColor = Color.yellow;
        }
        else if ((strongAcids > 0 || weakAcids > 0) && waterCount > 0)
        {
            // Acid + Water -> Dissociation
            reactionEquation = "Acid + Water → H₃O⁺ + Anion";
            resultantColor = strongAcids > 0 ? Color.red : Color.magenta; // Strong acids produce red, weak produce pink
        }
        else if ((strongBases > 0 || weakBases > 0) && waterCount > 0)
        {
            // Base + Water -> Dissociation
            reactionEquation = "Base + Water → OH⁻ + Cation";
            resultantColor = strongBases > 0 ? Color.blue : Color.cyan; // Strong bases produce blue, weak produce cyan
        }
        else if (waterCount > 0 && chemicals.Count == waterCount)
        {
            // Only water in the mixture
            reactionEquation = "Only water present.";
            resultantColor = Color.white;
        }
        else
        {
            // No valid reaction
            reactionEquation = "No valid reaction detected.";
            resultantColor = Color.gray;
        }

        // Adjust the color based on the indicator if one is used
        if (indicator != null)
        {
            switch (indicator)
            {
                case IndicatorType.Litmus:
                    //resultantColor = strongAcids > 0 || weakAcids > 0 ? Color.red : (strongBases > 0 || weakBases > 0 ? Color.blue : Color.green);
                    if (strongAcids > strongBases)
                        resultantColor = Color.red;
                    else if (strongBases > strongAcids)
                        resultantColor = Color.blue;
                    else if (weakAcids > weakBases)
                        resultantColor = Color.red;
                    else if (weakBases > weakAcids)
                        resultantColor = Color.blue;
                    else
                        resultantColor = Color.clear;
                    break;

                case IndicatorType.MethylOrange:
                    //resultantColor = strongAcids > 0 || weakAcids > 0 ? Color.red : new Color(1, 0.5f, 0, 1); // Basic solutions turn orange/yellow
                    if (strongAcids > strongBases)
                        resultantColor = Color.red;
                    else if (strongBases > strongAcids)
                        resultantColor = new Color(1, 0.5f, 0, 1);
                    else if (weakAcids > weakBases)
                        resultantColor = Color.red;
                    else if (weakBases > weakAcids)
                        resultantColor = new Color(1, 0.5f, 0, 1);
                    else
                        resultantColor = Color.clear;
                    break;

                case IndicatorType.Phenolphthalein:
                    //resultantColor = strongBases > 0 || weakBases > 0 ? Color.magenta : Color.clear; // Basic turns pink/magenta
                    if (strongAcids > strongBases)
                        resultantColor = Color.clear;
                    else if (strongBases > strongAcids)
                        resultantColor = Color.magenta;
                    else if (weakAcids > weakBases)
                        resultantColor = Color.clear;
                    else if (weakBases > weakAcids)
                        resultantColor = Color.magenta;
                    else
                        resultantColor = Color.clear;
                    break;
            }
        }

        // Display the reaction result
        
        // Change the color of an object in the scene to show the result
        return resultantColor;
    }
}