using UnityEngine;
using System.Collections;

public abstract class Device
{
    public string name;
    private string[] nodes;
    private int[] nodeIndex;
    private double value;

    Device(int n)
    {
        nodes = new string[n];
        nodeIndex = new int[n];
        value = 0.0;
    }
}
