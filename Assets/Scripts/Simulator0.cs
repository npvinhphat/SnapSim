using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using System.Collections;

public class Simulator0 : Simulator
{
    public int NumberOfVariables = 0;
    public int NumberOfPoints = 0;
    public List<string> NameList;
    public List<string> UnitList;
    public List<List<double>> DataList;
    public bool IsSimulating { get; private set; }

    public override void GenerateTextFromCircuit()
    {
        string str;
    }

    public override void GetDataFromText(TextAsset text)
    {
        var newTextAsset = Resources.Load("rc") as TextAsset;
        string str = newTextAsset.text;
        string[] strings = str.Split('\n');
        
        // Start Process
        NumberOfVariables = int.Parse(strings[4].Split(' ')[2]);
        NumberOfPoints = int.Parse(strings[5].Split(' ')[2]);
        DataList = new List<List<double>>();
        for (int i = 0; i < NumberOfVariables; i++)
        {
            NameList.Add(strings[7 + i].Trim().Split('\t')[1]);
            UnitList.Add(strings[7 + i].Trim().Split('\t')[2]);
            List<double> newList = new List<double>();
            DataList.Add(newList);
        }
        int currentLine = 8 + NumberOfVariables;
        for (int j = 0; j < NumberOfPoints; j++)
        {
            DataList[0].Add(Convert.ToDouble(strings[currentLine].Split('\t')[1]));
            for (int i = 1; i < NumberOfVariables; i++)
            {
                DataList[i].Add(Convert.ToDouble(strings[currentLine + i].Trim('\t')));
            }
            currentLine += NumberOfVariables + 1;
        }
    }

    public override void StartSimulation()
    {
        GetDataFromText(null);
        IsSimulating = true;
    }

    public override void StopSimulation()
    {
        IsSimulating = false;
    }
}
