using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private string name;
    private int index;
    private List<Device> devices;

    private Node(string str)
    {
        name = str;
        index = 0;
        devices = new List<Device>();
    }

    public string ToString()
    {
        string str = name;
        foreach (Device device in devices)
        {
            str += "\t" + device.name;
        }
        return str;
    }
}

