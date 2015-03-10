using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Simulator0 : Simulator
{
    public int NumberOfVariables = 0;
    public int NumberOfPoints = 0;
    public List<string> NameList;
    public List<string> UnitList; 
    public List<List<double>> DataList;
    public List<double> MaxList;
    public bool IsSimulating = false;
    public int CurrentStep = 0;
    public double MaximumVoltage = 0;
    public double MaximumCurrent = 0;
    public double SimulationSpeed = 1f;

    private double simulationTime = 0f;

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
            MaxList.Add(0);
            List<double> newList = new List<double>();
            DataList.Add(newList);
        }
        int currentLine = 8 + NumberOfVariables;
        for (int j = 0; j < NumberOfPoints; j++)
        {
            DataList[0].Add(Convert.ToDouble(strings[currentLine].Split('\t')[1]));
            MaxList[0] = Math.Max(MaxList[0], Math.Abs(DataList[0][j]));
            for (int i = 1; i < NumberOfVariables; i++)
            {
                DataList[i].Add(Convert.ToDouble(strings[currentLine + i].Trim('\t')));
                MaxList[i] = Math.Max(MaxList[i], Math.Abs(DataList[i][j]));
            }
            currentLine += NumberOfVariables + 1;
        }

        for (int i = 0; i < NumberOfVariables; i++)
        {
            if (UnitList[i] == "voltage")
            {
                MaximumVoltage = Math.Max(MaximumVoltage, MaxList[i]);
            }
            else
            {
                MaximumCurrent = Math.Max(MaximumCurrent, MaxList[i]);
            }
        }
    }

    public override void GenerateCurrentDots()
    {
        Device[] devices = Device.GetDevices();
        foreach (Device device in devices)
        {
            List<Vector3> pointsList = new List<Vector3>();
            switch (device.DeviceType)
            {
                case Device.TypeEnum.Capacitor:
                    Capacitor tempCapacitor = device as Capacitor;
                    pointsList.Add(tempCapacitor.NegativeNode.ConnectedNode.transform.position);
                    pointsList.Add(tempCapacitor.NegativeNode.transform.position);
                    pointsList.Add(tempCapacitor.PositiveNode.transform.position);
                    pointsList.Add(tempCapacitor.PositiveNode.ConnectedNode.transform.position);
                    //print("There is a capacitor");
                    break;
                case Device.TypeEnum.Ground:
                    break;
                case Device.TypeEnum.Resistor:
                    Resistor tempResistor = device as Resistor;
                    pointsList.Add(tempResistor.NegativeNode.ConnectedNode.transform.position);
                    pointsList.Add(tempResistor.NegativeNode.transform.position);
                    pointsList.Add(tempResistor.PositiveNode.transform.position);
                    pointsList.Add(tempResistor.PositiveNode.ConnectedNode.transform.position);
                    break;
                case Device.TypeEnum.PulseVoltageSource:
                    PulseVoltageSource tempPulseVoltageSource = device as PulseVoltageSource;
                    pointsList.Add(tempPulseVoltageSource.NegativeNode.ConnectedNode.transform.position);
                    pointsList.Add(tempPulseVoltageSource.NegativeNode.transform.position);
                    pointsList.Add(tempPulseVoltageSource.PositiveNode.transform.position);
                    pointsList.Add(tempPulseVoltageSource.PositiveNode.ConnectedNode.transform.position);
                    break;
            }
            for (int i = 0; i < pointsList.Count - 1; i++)
            {
                if (pointsList[i] != pointsList[i + 1])
                {
                    GameObject currentDotGo =
                        (GameObject) Instantiate(Container.CircuitElements.CurrentDot, pointsList[i],
                            Quaternion.identity);
                    CurrentDot currentDot = currentDotGo.GetComponent<CurrentDot>();
                    currentDot.AttachedDevice = device;
                    currentDot.StartPos = pointsList[i];
                    currentDot.EndPos = pointsList[i + 1];
                    print(pointsList[i]);
                }
            }
        }
    }

    public override void StartSimulation()
    {
        GetDataFromText(null);
        
        // SendDataToObjects

        CurrentStep = 0;
        GenerateCurrentDots();
        IsSimulating = true;

        
    }

    public override void StopSimulation()
    {
        IsSimulating = false;
    }

    void Update()
    {
        if (IsSimulating)
        {
            simulationTime += Time.deltaTime;
            if (simulationTime > SimulationSpeed)
            {
                CurrentStep += 1;
                simulationTime = 0f;
            }
        }
    }
}
